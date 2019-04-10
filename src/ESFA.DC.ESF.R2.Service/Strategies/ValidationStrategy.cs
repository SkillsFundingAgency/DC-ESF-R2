using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Interfaces.Strategies;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Service.Strategies
{
    public class ValidationStrategy : ITaskStrategy
    {
        private readonly IValidationController _controller;

        public ValidationStrategy(IValidationController controller)
        {
            _controller = controller;
        }

        public int Order => 1;

        public bool IsMatch(string taskName)
        {
            return taskName == Constants.ValidationTask;
        }

        public Task Execute(
            JobContextModel jobContextModel,
            SourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            CancellationToken cancellationToken)
        {
            _controller.ValidateData(wrapper, sourceFile, cancellationToken);
            return Task.CompletedTask;
        }
    }
}
