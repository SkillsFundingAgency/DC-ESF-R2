﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.CsvService.Interface;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Reports.AimAndDeliverable;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ESF.R2.ReportingService.Abstract;
using ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Abstract;
using ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Interface;
using ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Model;
using ESFA.DC.ESF.R2.ReportingService.Constants;

namespace ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable
{
    public class AimAndDeliverableReport : AbstractCsvReportService<AimAndDeliverableReportRow, AbstractAimAndDeliverableMapper>, IModelReport
    {
        private readonly IAimAndDeliverableModelBuilder _aimAndDeliverableModelBuilder;
        private readonly IAimAndDeliverableDataProvider _aimAndDeliverableDataProvider;
        private readonly AbstractAimAndDeliverableMapper _aimAndDeliverableMapper;

        public AimAndDeliverableReport(
            IDateTimeProvider dateTimeProvider,
            ICsvFileService csvFileService,
            IAimAndDeliverableModelBuilder aimAndDeliverableModelBuilder,
            IAimAndDeliverableDataProvider aimAndDeliverableDataProvider,
            AbstractAimAndDeliverableMapper aimAndDeliverableMapper)
            : base(dateTimeProvider, csvFileService, ReportTaskConstants.TaskGenerateEsfAimAndDeliverableReport)
        {
            _aimAndDeliverableModelBuilder = aimAndDeliverableModelBuilder;
            _aimAndDeliverableDataProvider = aimAndDeliverableDataProvider;
            _aimAndDeliverableMapper = aimAndDeliverableMapper;
            ReportFileName = ReportNameConstants.AimsAndDeliverableReport;
        }

        public async Task<string> GenerateReport(IEsfJobContext esfJobContext, ISourceFileModel sourceFile, SupplementaryDataWrapper wrapper, CancellationToken cancellationToken)
        {
            var externalFileName = GetExternalFilename(esfJobContext.UkPrn, esfJobContext.JobId, sourceFile?.SuppliedDate ?? DateTime.MinValue, ReportNameConstants.CsvExtension);

            var learningDeliveries = _aimAndDeliverableDataProvider.GetLearningDeliveriesAsync(esfJobContext.UkPrn, cancellationToken);
            var dpOutcomes = _aimAndDeliverableDataProvider.GetDpOutcomesAsync(esfJobContext.UkPrn, cancellationToken);
            var deliverablePeriods = _aimAndDeliverableDataProvider.GetDeliverablePeriodsAsync(esfJobContext.UkPrn, cancellationToken);
            var esfDpOutcomes = _aimAndDeliverableDataProvider.GetEsfDpOutcomesAsync(esfJobContext.UkPrn, cancellationToken);
            var fcsDeliverableCodeMappings = _aimAndDeliverableDataProvider.GetFcsDeliverableCodeMappingsAsync(cancellationToken);

            await Task.WhenAll(learningDeliveries, dpOutcomes, deliverablePeriods, esfDpOutcomes, fcsDeliverableCodeMappings);

            var learnAimRefs = new HashSet<string>(learningDeliveries.Result.Select(l => l.LearnAimRef), StringComparer.OrdinalIgnoreCase);
            var larsLearningDeliveries = await _aimAndDeliverableDataProvider.GetLarsLearningDeliveriesAsync(learnAimRefs, cancellationToken);

            var reportModels = _aimAndDeliverableModelBuilder.Build(esfJobContext, learningDeliveries.Result, dpOutcomes.Result, deliverablePeriods.Result, esfDpOutcomes.Result, larsLearningDeliveries, fcsDeliverableCodeMappings.Result);

            await WriteCsv(esfJobContext, externalFileName, reportModels, cancellationToken, _aimAndDeliverableMapper);

            return externalFileName;
        }
    }
}
