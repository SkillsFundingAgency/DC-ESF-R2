using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Cells;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.Config;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Interfaces.Strategies;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Generation;
using ESFA.DC.ESF.R2.Models.Reports;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.Models.Styling;
using ESFA.DC.ESF.R2.ReportingService.Mappers;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.ReportingService.Reports.FundingSummary
{
    public sealed class FundingSummaryReport : AbstractReportBuilder, IModelReport
    {
        private readonly IFileService _storage;
        private readonly IList<IRowHelper> _rowHelpers;
        private readonly IILRService _ilrService;
        private readonly IReferenceDataService _referenceDataService;
        private readonly IExcelStyleProvider _excelStyleProvider;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IVersionInfo _versionInfo;
        private readonly ISupplementaryDataService _supplementaryDataService;

        private readonly ModelProperty[] _cachedModelProperties;
        private readonly FundingSummaryMapper _fundingSummaryMapper;

        private string[] _cachedHeaders;
        private CellStyle[] _cellStyles;
        private int _reportWidth = 1;

        public FundingSummaryReport(
            IDateTimeProvider dateTimeProvider,
            IValueProvider valueProvider,
            IFileService storage,
            IILRService ilrService,
            ISupplementaryDataService supplementaryDataService,
            IList<IRowHelper> rowHelpers,
            IReferenceDataService referenceDataService,
            IExcelStyleProvider excelStyleProvider,
            IVersionInfo versionInfo)
            : base(dateTimeProvider, valueProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            _storage = storage;
            _rowHelpers = rowHelpers;
            _referenceDataService = referenceDataService;
            _excelStyleProvider = excelStyleProvider;
            _versionInfo = versionInfo;
            _supplementaryDataService = supplementaryDataService;
            _ilrService = ilrService;

            ReportFileName = "ESF Round 2 Funding Summary Report";
            _fundingSummaryMapper = new FundingSummaryMapper();
            _cachedModelProperties = _fundingSummaryMapper
                .MemberMaps
                .OrderBy(x => x.Data.Index)
                .Select(x => new ModelProperty(x.Data.Names.Names.ToArray(), (PropertyInfo)x.Data.Member))
                .ToArray();
        }

        public async Task GenerateReport(
            JobContextModel jobContextModel,
            SourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            ZipArchive archive,
            CancellationToken cancellationToken)
        {
            var ukPrn = Convert.ToInt32(jobContextModel.UkPrn);

            var collectionYear = Convert.ToInt32($"20{jobContextModel.CollectionYear.ToString().Substring(0, 2)}");

            var sourceFiles = await _supplementaryDataService.GetImportFiles(jobContextModel.UkPrn.ToString(), cancellationToken);
            var supplementaryData =
                await _supplementaryDataService.GetSupplementaryData(collectionYear, sourceFiles, cancellationToken);

            var ilrYearlyFileData = (await _ilrService.GetIlrFileDetails(ukPrn, cancellationToken)).ToList();
            var fm70YearlyData = (await _ilrService.GetYearlyIlrData(collectionYear, ukPrn, cancellationToken)).ToList();

            FundingSummaryHeaderModel fundingSummaryHeaderModel =
                PopulateReportHeader(sourceFile, ilrYearlyFileData, ukPrn, cancellationToken);

            var workbook = new Workbook();
            workbook.Worksheets.Clear();

            foreach (var file in sourceFiles)
            {
                var fundingSummaryModels = PopulateReportData(collectionYear, fm70YearlyData, supplementaryData[file.SourceFileId]).ToList();

                ReplaceConRefNumInTitle(fundingSummaryModels, file);

                FundingSummaryFooterModel fundingSummaryFooterModel = PopulateReportFooter(cancellationToken);

                FundingSummaryModel rowOfData = fundingSummaryModels.FirstOrDefault(x => x.DeliverableCode == "ST01" && x.YearlyValues.Any());
                var yearAndDataLengthModels = new List<YearAndDataLengthModel>();
                if (rowOfData != null)
                {
                    int valCount = rowOfData.YearlyValues.Sum(x => x.Values.Count);
                    _reportWidth = valCount + rowOfData.Totals.Count + 2;
                    foreach (FundingSummaryReportYearlyValueModel fundingSummaryReportYearlyValueModel in
                        rowOfData.YearlyValues)
                    {
                        yearAndDataLengthModels.Add(new YearAndDataLengthModel(
                            fundingSummaryReportYearlyValueModel.FundingYear,
                            fundingSummaryReportYearlyValueModel.Values.Count));
                    }
                }

                _cachedHeaders = GetHeaderEntries(collectionYear, yearAndDataLengthModels);
                _cellStyles = _excelStyleProvider.GetFundingSummaryStyles(workbook);

                Worksheet sheet = workbook.Worksheets.Add(file.ConRefNumber);
                sheet.Cells.StandardWidth = 19;
                sheet.Cells.Columns[0].Width = 63.93;
                sheet.IsGridlinesVisible = false;

                AddImageToReport(sheet);

                workbook = GetWorkbookReport(workbook, sheet, fundingSummaryHeaderModel, fundingSummaryModels, fundingSummaryFooterModel);
            }

            string externalFileName = GetExternalFilename(ukPrn.ToString(), jobContextModel.JobId, sourceFile?.SuppliedDate ?? DateTime.MinValue);
            string fileName = GetFilename(ukPrn.ToString(), jobContextModel.JobId, sourceFile?.SuppliedDate ?? DateTime.MinValue);

            using (var ms = new MemoryStream())
            {
                if (workbook.Worksheets.Any())
                {
                    workbook.Save(ms, SaveFormat.Xlsx);
                    ms.Seek(0, SeekOrigin.Begin);

                    using (var stream = await _storage.OpenWriteStreamAsync(
                        $"{externalFileName}.xlsx",
                        jobContextModel.BlobContainerName,
                        cancellationToken))
                    {
                        await ms.CopyToAsync(stream, 81920, cancellationToken);
                    }

                    ms.Seek(0, SeekOrigin.Begin);
                    await WriteZipEntry(archive, $"{fileName}.xlsx", ms, cancellationToken);
                }
            }
        }

        private void AddImageToReport(Worksheet worksheet)
        {
            var assembly = Assembly.GetExecutingAssembly();
            string euFlag = assembly.GetManifestResourceNames().Single(str => str.EndsWith("ESF.png"));
            using (Stream stream = assembly.GetManifestResourceStream(euFlag))
            {
                worksheet.Pictures.Add(3, _reportWidth - 2, stream);
            }
        }

        private FundingSummaryHeaderModel PopulateReportHeader(
            SourceFileModel sourceFile,
            IEnumerable<ILRFileDetails> fileData,
            int ukPrn,
            CancellationToken cancellationToken)
        {
            var ukPrnRow =
                new List<string> { ukPrn.ToString(), null, null };
            var contractReferenceNumberRow =
                new List<string> { sourceFile?.ConRefNumber, null, null, "ILR File :" };
            var supplementaryDataFileRow =
                new List<string> { sourceFile?.FileName?.Contains("/") ?? false ? sourceFile.FileName.Substring(sourceFile.FileName.IndexOf("/", StringComparison.Ordinal) + 1) : sourceFile?.FileName, null, null, "Last ILR File Update :" };
            var lastSupplementaryDataFileUpdateRow =
                new List<string> { sourceFile?.SuppliedDate?.ToString("dd/MM/yyyy hh:mm:ss"), null, null, "File Preparation Date :" };

            foreach (var model in fileData)
            {
                var preparationDate = FileNameHelper.GetPreparedDateFromILRFileName(model.FileName);
                var secondYear = FileNameHelper.GetSecondYearFromReportYear(model.Year);

                ukPrnRow.Add(null);
                ukPrnRow.Add($"{model.Year}/{secondYear}");
                contractReferenceNumberRow.Add(model.FileName.Substring(model.FileName.Contains("/") ? model.FileName.IndexOf("/", StringComparison.Ordinal) + 1 : 0));
                contractReferenceNumberRow.Add(null);
                supplementaryDataFileRow.Add(model.LastSubmission?.ToString("dd/MM/yyyy hh:mm:ss"));
                supplementaryDataFileRow.Add(null);
                lastSupplementaryDataFileUpdateRow.Add(preparationDate);
                lastSupplementaryDataFileUpdateRow.Add(null);
            }

            var header = new FundingSummaryHeaderModel
            {
                ProviderName = _referenceDataService.GetProviderName(ukPrn, cancellationToken),
                Ukprn = ukPrnRow.ToArray(),
                ContractReferenceNumber = contractReferenceNumberRow.ToArray(),
                SupplementaryDataFile = supplementaryDataFileRow.ToArray(),
                LastSupplementaryDataFileUpdate = lastSupplementaryDataFileUpdateRow.ToArray()
            };

            return header;
        }

        private FundingSummaryFooterModel PopulateReportFooter(CancellationToken cancellationToken)
        {
            var dateTimeNowUtc = _dateTimeProvider.GetNowUtc();
            var dateTimeNowUk = _dateTimeProvider.ConvertUtcToUk(dateTimeNowUtc);

            return new FundingSummaryFooterModel
            {
                ReportGeneratedAt = "Report generated at " + dateTimeNowUk.ToString("HH:mm:ss") + " on " + dateTimeNowUk.ToString("dd/MM/yyyy"),
                LarsData = _referenceDataService.GetLarsVersion(cancellationToken),
                OrganisationData = _referenceDataService.GetOrganisationVersion(cancellationToken),
                PostcodeData = _referenceDataService.GetPostcodeVersion(cancellationToken),
                ApplicationVersion = _versionInfo.ServiceReleaseVersion
            };
        }

        private IEnumerable<FundingSummaryModel> PopulateReportData(
            int endYear,
            IEnumerable<FM70PeriodisedValuesYearly> fm70YearlyData,
            IEnumerable<SupplementaryDataYearlyModel> data)
        {
            List<FM70PeriodisedValuesYearly> fm70YearlyDataList = fm70YearlyData.ToList();
            List<SupplementaryDataYearlyModel> dataList = data.ToList();

            var fundingSummaryModels = new List<FundingSummaryModel>();
            foreach (var fundingReportRow in ReportDataTemplate.FundingModelRowDefinitions)
            {
                foreach (var rowHelper in _rowHelpers)
                {
                    if (!rowHelper.IsMatch(fundingReportRow.RowType))
                    {
                        continue;
                    }

                    rowHelper.Execute(endYear, fundingSummaryModels, fundingReportRow, dataList, fm70YearlyDataList);
                    break;
                }
            }

            return fundingSummaryModels;
        }

        private Workbook GetWorkbookReport(
            Workbook workbook,
            Worksheet sheet,
            FundingSummaryHeaderModel fundingSummaryHeaderModel,
            IEnumerable<FundingSummaryModel> fundingSummaryModels,
            FundingSummaryFooterModel fundingSummaryFooterModel)
        {
            WriteExcelRecords(sheet, new FundingSummaryHeaderMapper(), new List<FundingSummaryHeaderModel> { fundingSummaryHeaderModel }, _cellStyles[7], _cellStyles[7], true);
            foreach (var fundingSummaryModel in fundingSummaryModels)
            {
                if (string.IsNullOrEmpty(fundingSummaryModel.Title))
                {
                    WriteBlankRow(sheet);
                    continue;
                }

                CellStyle excelHeaderStyle = _excelStyleProvider.GetCellStyle(_cellStyles, fundingSummaryModel.ExcelHeaderStyle);

                if (fundingSummaryModel.HeaderType == HeaderType.TitleOnly)
                {
                    WriteTitleRecord(sheet, fundingSummaryModel.Title, excelHeaderStyle, _reportWidth);
                    continue;
                }

                if (fundingSummaryModel.HeaderType == HeaderType.All)
                {
                    _fundingSummaryMapper.MemberMaps.Single(x => x.Data.Index == 0).Name(fundingSummaryModel.Title);
                    _cachedHeaders[0] = fundingSummaryModel.Title;
                    WriteRecordsFromArray(sheet, _fundingSummaryMapper, _cachedHeaders, excelHeaderStyle);
                    continue;
                }

                CellStyle excelRecordStyle = _excelStyleProvider.GetCellStyle(_cellStyles, fundingSummaryModel.ExcelRecordStyle);

                WriteExcelRecordsFromModelProperty(sheet, _fundingSummaryMapper, _cachedModelProperties, fundingSummaryModel, excelRecordStyle);
            }

            WriteExcelRecords(sheet, new FundingSummaryFooterMapper(), new List<FundingSummaryFooterModel> { fundingSummaryFooterModel }, _cellStyles[7], _cellStyles[7], true);

            return workbook;
        }

        private string[] GetHeaderEntries(int endYear, List<YearAndDataLengthModel> yearAndDataLengthModels)
        {
            var values = new List<string>
            {
                string.Empty // placeholder for title
            };
            foreach (var cachedModelProperty in _cachedModelProperties)
            {
                if (typeof(IEnumerable).IsAssignableFrom(cachedModelProperty.MethodInfo.PropertyType))
                {
                    BuildYears(endYear, values, cachedModelProperty.Names, yearAndDataLengthModels);
                    BuildTotals(endYear, values, cachedModelProperty.Names, yearAndDataLengthModels);
                }
                else
                {
                    values.Add(cachedModelProperty.Names[0]);
                }
            }

            return values.ToArray();
        }

        private void BuildTotals(
            int endYear,
            List<string> values,
            string[] names,
            List<YearAndDataLengthModel> yearAndDataLengthModels)
        {
            if (names.Length < 7)
            {
                return;
            }

            for (int i = Constants.StartYear; i <= endYear; i++)
            {
                YearAndDataLengthModel yearAndDataLengthModel = yearAndDataLengthModels.SingleOrDefault(x => x.Year == i);
                if (yearAndDataLengthModel == null)
                {
                    continue;
                }

                var year = i == Constants.StartYear ? i + 1 : i;
                var startMonthIndex = i == Constants.StartYear ? 8 : 0;
                var length = 12 - startMonthIndex;
                var yearlyNames = new string[length];
                Array.Copy(names, startMonthIndex, yearlyNames, 0, length);

                int counter = 0;
                foreach (string name in yearlyNames)
                {
                    if (counter > yearAndDataLengthModel.DataLength)
                    {
                        break;
                    }

                    if (!name.Contains("{Y}"))
                    {
                        return;
                    }

                    if (i != 2015 && counter == 5)
                    {
                        year++;
                    }

                    values.Add(name.Replace("{Y}", year.ToString()));
                    counter++;
                }
            }
        }

        private void BuildYears(
            int endYear,
            List<string> values,
            string[] names,
            List<YearAndDataLengthModel> yearAndDataLengthModels)
        {
            for (var i = Constants.StartYear; i <= endYear; i++)
            {
                YearAndDataLengthModel yearAndDataLengthModel = yearAndDataLengthModels.SingleOrDefault(x => x.Year == i);
                if (yearAndDataLengthModel == null)
                {
                    continue;
                }

                int counter = 0;
                foreach (string name in names)
                {
                    if (counter > yearAndDataLengthModel.DataLength)
                    {
                        break;
                    }

                    if (!name.Contains("{SP}"))
                    {
                        return;
                    }

                    values.Add(name.Replace("{SP}", i.ToString()).Replace("{SY}", (i + 1).ToString().Substring(2)));
                    counter++;
                }
            }
        }

        private void ReplaceConRefNumInTitle(IEnumerable<FundingSummaryModel> fundingSummaryModels, SourceFileModel sourceFile)
        {
            foreach (var fundingSummaryModel in fundingSummaryModels)
            {
                if (string.IsNullOrEmpty(fundingSummaryModel.Title) || !fundingSummaryModel.Title.Contains(Constants.ContractReferenceNumberTag))
                {
                    continue;
                }

                fundingSummaryModel.Title = fundingSummaryModel.Title.Replace(Constants.ContractReferenceNumberTag, sourceFile.ConRefNumber);
            }
        }
    }
}