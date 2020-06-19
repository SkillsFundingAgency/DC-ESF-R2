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
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Config;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Interfaces.Strategies;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Generation;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ESF.R2.Models.Reports;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.Models.Styling;
using ESFA.DC.ESF.R2.ReportingService.Mappers;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.DataService.Models;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.ReportingService.Reports.FundingSummary
{
    public sealed class FundingSummaryReport : AbstractReportBuilder, IModelReport
    {
        private const string NotApplicable = "Not Applicable";
        private readonly IFileService _storage;
        private readonly IList<IRowHelper> _rowHelpers;
        private readonly IILRService _ilrService;
        private readonly IReferenceDataService _referenceDataService;
        private readonly IExcelStyleProvider _excelStyleProvider;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IVersionInfo _versionInfo;
        private readonly ILogger _logger;
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
            IVersionInfo versionInfo,
            ILogger logger)
            : base(dateTimeProvider, valueProvider, Constants.TaskGenerateFundingSummaryReport)
        {
            _dateTimeProvider = dateTimeProvider;
            _storage = storage;
            _rowHelpers = rowHelpers;
            _referenceDataService = referenceDataService;
            _excelStyleProvider = excelStyleProvider;
            _versionInfo = versionInfo;
            _logger = logger;
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
            IEsfJobContext esfJobContext,
            ISourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            ZipArchive archive,
            CancellationToken cancellationToken)
        {
            var ukPrn = esfJobContext.UkPrn;

            var conRefNumbers = await _referenceDataService.GetContractAllocationsForUkprn(ukPrn, cancellationToken);

            if (!conRefNumbers.Any())
            {
                conRefNumbers = new List<string> { NotApplicable };
            }

            var collectionYear = Convert.ToInt32($"20{esfJobContext.CollectionYear.ToString().Substring(0, 2)}");

            var sourceFiles = await _supplementaryDataService.GetImportFiles(esfJobContext.UkPrn.ToString(), cancellationToken);

            _logger.LogDebug($"{sourceFiles.Count} esf files found for ukprn {ukPrn} and collection year 20{esfJobContext.CollectionYear.ToString().Substring(0, 2)}.");

            var supplementaryData =
                await _supplementaryDataService.GetSupplementaryData(collectionYear, sourceFiles, cancellationToken);

            var ilrYearlyFileData = (await _ilrService.GetIlrFileDetails(ukPrn, collectionYear, cancellationToken)).ToList();
            var fm70YearlyData = (await _ilrService.GetYearlyIlrData(ukPrn, esfJobContext.CollectionName, collectionYear, esfJobContext.ReturnPeriod, cancellationToken)).ToList();

            var workbook = new Workbook();
            workbook.Worksheets.Clear();

            foreach (var conRefNumber in conRefNumbers)
            {
                var file = sourceFiles.FirstOrDefault(sf => sf.ConRefNumber.CaseInsensitiveEquals(conRefNumber));

                FundingSummaryHeaderModel fundingSummaryHeaderModel =
                    PopulateReportHeader(file, ilrYearlyFileData, ukPrn, conRefNumber, cancellationToken);

                var fm70YearlyDataForConRef = new List<FM70PeriodisedValuesYearly>();
                var supplementaryDataYearlyModels = new List<SupplementaryDataYearlyModel>();
                supplementaryData.TryGetValue(conRefNumber, out var suppData);

                foreach (var fm70Data in fm70YearlyData)
                {
                    var periodisedValuesPerConRef = fm70Data.Fm70PeriodisedValues.Where(x => conRefNumber.CaseInsensitiveEquals(x.ConRefNumber)).ToList();
                    fm70YearlyDataForConRef.Add(new FM70PeriodisedValuesYearly()
                    {
                        Fm70PeriodisedValues = periodisedValuesPerConRef,
                        FundingYear = fm70Data.FundingYear
                    });

                    supplementaryDataYearlyModels.Add(new SupplementaryDataYearlyModel
                    {
                        FundingYear = fm70Data.FundingYear,
                        SupplementaryData = suppData?.FirstOrDefault(x => x.FundingYear == fm70Data.FundingYear)?.SupplementaryData ?? new List<SupplementaryDataModel>()
                    });
                }

                var fundingSummaryModels = PopulateReportData(collectionYear, fm70YearlyDataForConRef, supplementaryDataYearlyModels).ToList();

                ReplaceConRefNumInTitle(fundingSummaryModels, conRefNumber);

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

                Worksheet sheet = workbook.Worksheets.Add(conRefNumber);
                sheet.Cells.StandardWidth = 19;
                sheet.Cells.Columns[0].Width = 63.93;
                sheet.IsGridlinesVisible = false;

                AddImageToReport(sheet);

                workbook = GetWorkbookReport(workbook, sheet, fundingSummaryHeaderModel, fundingSummaryModels, fundingSummaryFooterModel);
            }

            string externalFileName = GetExternalFilename(ukPrn.ToString(), esfJobContext.JobId, sourceFile?.SuppliedDate ?? DateTime.MinValue);
            string fileName = GetFilename(ukPrn.ToString(), esfJobContext.JobId, sourceFile?.SuppliedDate ?? DateTime.MinValue);

            using (var ms = new MemoryStream())
            {
                if (!workbook.Worksheets.Any())
                {
                    workbook.Worksheets.Add(SheetType.Worksheet);
                }

                workbook.Save(ms, SaveFormat.Xlsx);

                ms.Seek(0, SeekOrigin.Begin);

                using (var stream = await _storage.OpenWriteStreamAsync(
                    $"{externalFileName}.xlsx",
                    esfJobContext.BlobContainerName,
                    cancellationToken))
                {
                    await ms.CopyToAsync(stream, 81920, cancellationToken);
                }

                ms.Seek(0, SeekOrigin.Begin);
                await WriteZipEntry(archive, $"{fileName}.xlsx", ms, cancellationToken);
            }
        }

        private void AddImageToReport(Worksheet worksheet)
        {
            WriteBlankRow(worksheet, 1); // Add blank Row to position the image on top
            worksheet.Cells.SetRowHeight(0, 27); // Adjust the image row height

            var assembly = Assembly.GetExecutingAssembly();
            string euFlag = assembly.GetManifestResourceNames().Single(str => str.EndsWith("ESF.png"));
            using (Stream stream = assembly.GetManifestResourceStream(euFlag))
            {
                worksheet.Pictures.Add(0, _reportWidth - 2, stream);
                worksheet.Pictures[0].Top = 2;
            }
        }

        private FundingSummaryHeaderModel PopulateReportHeader(
            SourceFileModel sourceFile,
            IEnumerable<ILRFileDetails> fileData,
            int ukPrn,
            string conRefNumber,
            CancellationToken cancellationToken)
        {
            var ukPrnRow =
                new List<string> { ukPrn.ToString(), null, null };
            var contractReferenceNumberRow =
                new List<string> { conRefNumber, null, null, "ILR File :" };
            var supplementaryDataFileRow =
                new List<string> { sourceFile?.FileName?.Contains("/") ?? false ? sourceFile.FileName.Substring(sourceFile.FileName.IndexOf("/", StringComparison.Ordinal) + 1) : sourceFile?.FileName, null, null, "Last ILR File Update :" };
            var lastSupplementaryDataFileUpdateRow =
                new List<string> { sourceFile?.SuppliedDate?.ToString("dd/MM/yyyy hh:mm:ss"), null, null, "File Preparation Date :" };
            var securityClassificationRow =
                new List<string> { "OFFICIAL-SENSITIVE", null, null, null };

            foreach (var model in fileData)
            {
                var preparationDate = GetPreparedDateFromILRFileName(model.FileName);
                var secondYear = GetSecondYearFromReportYear(model.Year);

                ukPrnRow.Add(null);
                ukPrnRow.Add($"{model.Year}/{secondYear}");
                contractReferenceNumberRow.Add(model.FileName?.Substring(model.FileName.Contains("/") ? model.FileName.IndexOf("/", StringComparison.Ordinal) + 1 : 0));
                contractReferenceNumberRow.Add(null);
                supplementaryDataFileRow.Add(preparationDate);
                supplementaryDataFileRow.Add(null);
                lastSupplementaryDataFileUpdateRow.Add(model.FilePreparationDate?.ToString("dd/MM/yyyy hh:mm:ss"));
                lastSupplementaryDataFileUpdateRow.Add(null);

                if (model.Equals(fileData.Last()))
                {
                    continue;
                }

                securityClassificationRow.Add("(most recent closed collection for year)");
                securityClassificationRow.Add(null);
            }

            var header = new FundingSummaryHeaderModel
            {
                ProviderName = _referenceDataService.GetProviderName(ukPrn, cancellationToken),
                Ukprn = ukPrnRow.ToArray(),
                ContractReferenceNumber = contractReferenceNumberRow.ToArray(),
                SupplementaryDataFile = supplementaryDataFileRow.ToArray(),
                LastSupplementaryDataFileUpdate = lastSupplementaryDataFileUpdateRow.ToArray(),
                SecurityClassification = securityClassificationRow.ToArray()
            };

            return header;
        }

        private FundingSummaryFooterModel PopulateReportFooter(CancellationToken cancellationToken)
        {
            var dateTimeNowUtc = _dateTimeProvider.GetNowUtc();
            var dateTimeNowUk = _dateTimeProvider.ConvertUtcToUk(dateTimeNowUtc);

            return new FundingSummaryFooterModel
            {
                ReportGeneratedAt = dateTimeNowUk.ToString("HH:mm:ss") + " on " + dateTimeNowUk.ToString("dd/MM/yyyy"),
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
            List<SupplementaryDataYearlyModel> dataList = data?.ToList() ?? new List<SupplementaryDataYearlyModel>();

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
            {
                WriteExcelRecords(sheet, new FundingSummaryHeaderMapper(), new List<FundingSummaryHeaderModel> { fundingSummaryHeaderModel }, _cellStyles[7], _cellStyles[7], true);
                var firstFutureColumn = GetFirstFutureColumn();
                int lastOperatedRow;
                // number of columns minus number of static columns (3)
                var endColumn = _cachedHeaders.Count() - 3;
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
                        // Align data to the Right
                        excelHeaderStyle.Style.HorizontalAlignment = TextAlignmentType.Right;
                        excelHeaderStyle.StyleFlag.HorizontalAlignment = true;

                        _fundingSummaryMapper.MemberMaps.Single(x => x.Data.Index == 0).Name(fundingSummaryModel.Title);
                        _cachedHeaders[0] = fundingSummaryModel.Title;

                        // this line is the month/year header
                        WriteRecordsFromArray(sheet, _fundingSummaryMapper, _cachedHeaders, excelHeaderStyle);
                        lastOperatedRow = GetCurrentRow(sheet) - 1;
                        ItaliciseFutureData(sheet, firstFutureColumn, lastOperatedRow, endColumn);
                        continue;
                    }

                    CellStyle excelRecordStyle = _excelStyleProvider.GetCellStyle(_cellStyles, fundingSummaryModel.ExcelRecordStyle);

                    // Align data to the Right
                    excelRecordStyle.Style.HorizontalAlignment = TextAlignmentType.Right;
                    excelRecordStyle.StyleFlag.HorizontalAlignment = true;

                    // this line is subtotals below the month/ year header
                    WriteExcelRecordsFromModelProperty(sheet, _fundingSummaryMapper, _cachedModelProperties, fundingSummaryModel, excelRecordStyle);
                    lastOperatedRow = GetCurrentRow(sheet) - 1;
                    ItaliciseFutureData(sheet, firstFutureColumn, lastOperatedRow, endColumn);
                }

                for (int i = 0; i < workbook.Worksheets.Count; i++)
                {
                    AlignWorkSheetColumnData(workbook.Worksheets[i], 0, TextAlignmentType.Left);
                }

                WriteExcelRecords(sheet, new FundingSummaryFooterMapper(), new List<FundingSummaryFooterModel> { fundingSummaryFooterModel }, _cellStyles[7], _cellStyles[7], true);

                return workbook;
            }
        }

        /// <summary>
        ///   Align column data to [Right] or [Left].
        /// </summary>
        /// <param name="worksheet"> worksheet number to use.</param>
        /// <param name="columnNumber">column number to apply alignment.</param>
        /// <param name="textAlignmentType">type of alignment [Left] [Right].</param>
        private void AlignWorkSheetColumnData(Worksheet worksheet, int columnNumber, TextAlignmentType textAlignmentType)
        {
            Cells cells = worksheet.Cells;

            // Get current style & adjust alignment
            Style style = cells.Columns[columnNumber].Style;
            style.HorizontalAlignment = textAlignmentType;
            cells.Columns[columnNumber].ApplyStyle(style, new StyleFlag { HorizontalAlignment = true });
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
                    var b = cachedModelProperty.Names;
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

        private void ReplaceConRefNumInTitle(IEnumerable<FundingSummaryModel> fundingSummaryModels, string conRefNumber)
        {
            foreach (var fundingSummaryModel in fundingSummaryModels)
            {
                if (string.IsNullOrEmpty(fundingSummaryModel.Title) || !fundingSummaryModel.Title.Contains(Constants.ContractReferenceNumberTag))
                {
                    continue;
                }

                fundingSummaryModel.Title = fundingSummaryModel.Title.Replace(Constants.ContractReferenceNumberTag, conRefNumber);
            }
        }

        private int GetFirstFutureColumn()
        {
            // using implicit logic to pair months with their respective columns
            var reportStartDate = new DateTime(Constants.StartYear + 1, 4, 1);
            var curentDate = _dateTimeProvider.GetNowUtc();

            var firstFutureColumn = (((curentDate.Year - reportStartDate.Year) * 12) + (curentDate.Month - reportStartDate.Month)) + 2; // calculate months difference

            return firstFutureColumn;
        }

        private void ItaliciseFutureData(Worksheet sheet, int firstColumn, int lastOperatedRow, int lastColumn)
        {
            int totalColumns;
            var italicCellStyle = _excelStyleProvider.GetCellStyle(_cellStyles, 9);
            if ((lastColumn - firstColumn) < 0)
            {
              totalColumns = 1;
            }
            else
            {
                totalColumns = lastColumn - firstColumn;
            }

            // current row is incremeneted on leaving the write function, so decrement to update style.
            if (firstColumn > 0)
            {
                sheet.Cells.CreateRange(lastOperatedRow, firstColumn, 1, totalColumns).ApplyStyle(italicCellStyle.Style, italicCellStyle.StyleFlag);
            }
        }

        private string GetSecondYearFromReportYear(int year)
        {
            return year.ToString().Length > 3 ?
                (Convert.ToInt32(year.ToString().Substring(year.ToString().Length - 2)) + 1).ToString() :
                string.Empty;
        }

        private string GetPreparedDateFromILRFileName(string fileName)
        {
            if (fileName == null)
            {
                return null;
            }

            var fileNameParts = fileName.Split('-');
            if (fileNameParts.Length < 6 || fileNameParts[3].Length != 8 || fileNameParts[4].Length != 6)
            {
                return string.Empty;
            }

            var dateString = $"{fileNameParts[3].Substring(0, 4)}/{fileNameParts[3].Substring(4, 2)}/{fileNameParts[3].Substring(6, 2)} " +
                             $"{fileNameParts[4].Substring(0, 2)}:{fileNameParts[4].Substring(2, 2)}:{fileNameParts[4].Substring(4, 2)}";
            return Convert.ToDateTime(dateString).ToString("dd/MM/yyyy hh:mm:ss");
        }
    }
}