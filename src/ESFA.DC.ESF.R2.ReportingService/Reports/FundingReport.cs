using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.CsvService.Interface;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Constants;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ESF.R2.Models.Reports;
using ESFA.DC.ESF.R2.ReportingService.Abstract;
using ESFA.DC.ESF.R2.ReportingService.Constants;
using ESFA.DC.ESF.R2.ReportingService.Mappers;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.FileService.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.Reports
{
    public class FundingReport : AbstractCsvReportService<FundingReportModel, FundingReportMapper>, IModelReport
    {
        private const string _reportExtension = ".csv";

        private readonly IReferenceDataService _referenceDataService;

        public FundingReport(
            IDateTimeProvider dateTimeProvider,
            IValueProvider valueProvider,
            IFileService fileService,
            ICsvFileService csvFileService,
            IReferenceDataService referenceDataService)
            : base(dateTimeProvider, valueProvider, fileService, csvFileService, ReportTaskConstants.TaskGenerateFundingReport)
        {
            ReportFileName = ReportNameConstants.FundingReport;

            _referenceDataService = referenceDataService;
        }

        public async Task<string> GenerateReport(
            IEsfJobContext esfJobContext,
            ISourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            CancellationToken cancellationToken)
        {
            var reportModels = GetModels(wrapper);

            ReportFileName = $"{sourceFile.ConRefNumber} " + ReportFileName;
            string externalFileName = GetExternalFilename(esfJobContext.UkPrn, esfJobContext.JobId, sourceFile.SuppliedDate ?? DateTime.MinValue, _reportExtension);

            await WriteCsv(esfJobContext, externalFileName, reportModels, cancellationToken);

            return externalFileName;
        }

        private ICollection<FundingReportModel> GetModels(SupplementaryDataWrapper wrapper)
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

            return fundingModels;
        }
    }
}