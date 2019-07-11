using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FileLevel
{
    public class ConRefNumberRule01 : BaseValidationRule, IFileLevelValidator
    {
        public ConRefNumberRule01(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "ConRefNumber_01";

        public bool IsWarning => false;

        public bool RejectFile => true;

        public async Task<bool> IsValid(SourceFileModel sourceFileModel, SupplementaryDataLooseModel model)
        {
            string[] filenameParts = FileNameHelper.SplitFileName(sourceFileModel.FileName);

            return filenameParts[2] == model.ConRefNumber;
        }
    }
}
