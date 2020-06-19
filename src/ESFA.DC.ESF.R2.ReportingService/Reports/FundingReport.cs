using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Constants;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports;
using ESFA.DC.ESF.R2.ReportingService.Mappers;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.FileService.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.Reports
{
    public class FundingReport : AbstractReportBuilder, IModelReport
    {
        private readonly IFileService _storage;
        private readonly IReferenceDataService _referenceDataService;

        public FundingReport(
            IDateTimeProvider dateTimeProvider,
            IValueProvider valueProvider,
            IFileService storage,
            IReferenceDataService referenceDataService)
            : base(dateTimeProvider, valueProvider, Constants.TaskGenerateFundingReport)
        {
            ReportFileName = "ESF (Round 2) Supplementary Data Funding Report";

            _storage = storage;
            _referenceDataService = referenceDataService;
        }

        public async Task GenerateReport(
            IEsfJobContext esfJobContext,
            SourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            ZipArchive archive,
            CancellationToken cancellationToken)
        {
            string csv = GetCsv(wrapper);

            ReportFileName = $"{sourceFile.ConRefNumber} " + ReportFileName;
            string externalFileName = GetExternalFilename(esfJobContext.UkPrn.ToString(), esfJobContext.JobId, sourceFile.SuppliedDate ?? DateTime.MinValue);
            string fileName = GetFilename(esfJobContext.UkPrn.ToString(), esfJobContext.JobId, sourceFile.SuppliedDate ?? DateTime.MinValue);

            using (var stream = await _storage.OpenWriteStreamAsync(
                $"{externalFileName}.csv",
                esfJobContext.BlobContainerName,
                cancellationToken))
            {
                using (var writer = new StreamWriter(stream, new UTF8Encoding(false, true)))
                {
                    writer.Write(csv);
                }
            }

            await WriteZipEntry(archive, $"{fileName}.csv", csv);
        }

        private string GetCsv(SupplementaryDataWrapper wrapper)
        {
            var fundingModels = new List<FundingReportModel>();

            foreach (var model in wrapper.SupplementaryDataModels)
            {
                if (wrapper.ValidErrorModels.Any(vm =>
                    vm.ConRefNumber.CaseInsensitiveEquals(model.ConRefNumber)
                    && vm.DeliverableCode.CaseInsensitiveEquals(model.DeliverableCode)
                    && vm.CostType.CaseInsensitiveEquals(model.CostType)
                    && vm.CalendarYear.CaseInsensitiveEquals(model.CalendarYear.ToString())
                    && vm.CalendarMonth.CaseInsensitiveEquals(model.CalendarMonth.ToString())
                    && vm.ReferenceType.CaseInsensitiveEquals(model.ReferenceType)
                    && vm.Reference.CaseInsensitiveEquals(model.Reference)))
                {
                    continue;
                }

                var fundModel = new FundingReportModel
                {
                    ConRefNumber = model.ConRefNumber,
                    DeliverableCode = model.DeliverableCode,
                    CostType = model.CostType,
                    CalendarYear = model.CalendarYear,
                    CalendarMonth = model.CalendarMonth,
                    Reference = model.Reference,
                    ReferenceType = model.ReferenceType,
                    ULN = model.ULN,
                    ProviderSpecifiedReference = model.ProviderSpecifiedReference,
                    LearnAimRef = model.LearnAimRef,
                    SupplementaryDataPanelDate = model.SupplementaryDataPanelDate?.ToString("dd/MM/yyyy"),
                    Value = model.Value
                };

                if (ESFConstants.UnitCostDeliverableCodes.Contains(model.DeliverableCode))
                {
                    var unitCost = _referenceDataService.GetDeliverableUnitCosts(model.ConRefNumber, new List<string> { model.DeliverableCode })
                                        .FirstOrDefault(uc => uc.DeliverableCode.CaseInsensitiveEquals(model.DeliverableCode)
                                                              && uc.ConRefNum.CaseInsensitiveEquals(model.ConRefNumber))
                                        ?.UnitCost ?? 0M;
                    unitCost = model.CostType?.CaseInsensitiveEquals(ESFConstants.UnitCostDeductionCostType)
                               ?? false ? unitCost * -1 : unitCost;

                    fundModel.Value = unitCost;
                }

                fundingModels.Add(fundModel);
            }

            using (var ms = new MemoryStream())
            {
                var utF8Encoding = new UTF8Encoding(false, true);
                using (TextWriter textWriter = new StreamWriter(ms, utF8Encoding))
                {
                    using (var csvWriter = new CsvWriter(textWriter))
                    {
                        WriteCsvRecords<FundingReportMapper, FundingReportModel>(csvWriter, fundingModels);
                        csvWriter.Flush();
                        textWriter.Flush();
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }
    }
}