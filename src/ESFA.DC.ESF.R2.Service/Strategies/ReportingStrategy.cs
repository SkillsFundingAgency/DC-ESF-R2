using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Interfaces.Strategies;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Service.Strategies
{
    public class ReportingStrategy : ITaskStrategy
    {
        private readonly IReportingController _reportingController;

        private string[] _reportingTasks =
        {
            Constants.TaskGenerateEsfAimAndDeliverableReport,
            Constants.TaskGenerateFundingSummaryReport,
            Constants.TaskGenerateFundingReport
        };

        public ReportingStrategy(IReportingController reportingController)
        {
            _reportingController = reportingController;
        }

        public int Order => 3;

        public bool IsMatch(string taskName)
        {
            return _reportingTasks.Contains(taskName, StringComparer.OrdinalIgnoreCase);
        }

        public async Task Execute(
            JobContextModel jobContextModel,
            SourceFileModel sourceFile,
            SupplementaryDataWrapper supplementaryDataWrapper,
            CancellationToken cancellationToken)
        {
            await _reportingController.ProduceReports(jobContextModel, supplementaryDataWrapper, sourceFile, cancellationToken);
        }
    }
}
