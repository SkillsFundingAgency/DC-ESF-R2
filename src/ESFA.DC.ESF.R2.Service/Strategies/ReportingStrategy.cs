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

        public ReportingStrategy(IReportingController reportingController)
        {
            _reportingController = reportingController;
        }

        public int Order => 2;

        public bool IsMatch(string taskName)
        {
            return taskName == Constants.ReportingTask;
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
