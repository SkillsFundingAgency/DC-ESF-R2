using System.Linq;
using System.Threading;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FileLevel
{
    public class FileNameRule08 : IFileLevelValidator
    {
        private readonly IEsfRepository _esfRepository;

        public FileNameRule08(IEsfRepository esfRepository)
        {
            _esfRepository = esfRepository;
        }

        public string ErrorMessage => "The date/time of the file is not greater than a previous transmission with the same ConRefNumber and UKPRN.";

        public bool RejectFile => true;

        public string ErrorName => "Filename_08";

        public bool IsWarning => false;

        public bool Execute(SourceFileModel sourceFileModel, SupplementaryDataLooseModel model)
        {
            var previousFiles = _esfRepository.AllPreviousFilesForValidation(sourceFileModel.UKPRN, sourceFileModel.ConRefNumber, CancellationToken.None).Result;

            return previousFiles.All(f => f.FilePreparationDate <= sourceFileModel.PreparationDate);
        }
    }
}
