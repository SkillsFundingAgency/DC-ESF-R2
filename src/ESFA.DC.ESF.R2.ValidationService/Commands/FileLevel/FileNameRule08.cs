using System.Linq;
using System.Threading;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FileLevel
{
    public class FileNameRule08 : BaseValidationRule, IFileLevelValidator
    {
        private readonly IEsfRepository _esfRepository;

        public FileNameRule08(
            IValidationErrorMessageService errorMessageService,
            IEsfRepository esfRepository)
            : base(errorMessageService)
        {
            _esfRepository = esfRepository;
        }

        public bool RejectFile => true;

        public override string ErrorName => "Filename_08";

        public bool IsWarning => false;

        public bool IsValid(SourceFileModel sourceFileModel, SupplementaryDataLooseModel model)
        {
            var previousFiles = _esfRepository.AllPreviousFilesForValidation(sourceFileModel.UKPRN, sourceFileModel.ConRefNumber, CancellationToken.None).Result;

            return previousFiles?.All(f => f.FilePreparationDate <= sourceFileModel.PreparationDate) ?? true;
        }
    }
}
