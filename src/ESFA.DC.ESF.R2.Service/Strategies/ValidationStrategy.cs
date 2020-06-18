using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces;
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

        public async Task Execute(
            IEsfJobContext esfJobContext,
            SourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            CancellationToken cancellationToken)
        {
            await _controller.ValidateData(wrapper, sourceFile, cancellationToken);
        }
    }
}
