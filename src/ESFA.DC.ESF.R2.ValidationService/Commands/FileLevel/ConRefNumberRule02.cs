using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FileLevel
{
    public class ConRefNumberRule02 : BaseValidationRule, IFileLevelValidator
    {
        public ConRefNumberRule02(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "ConRefNumber_02";

        public bool IsWarning => false;

        public bool RejectFile => true;

        public bool IsValid(SourceFileModel sourceFileModel, SupplementaryDataLooseModel model)
        {
            if (string.IsNullOrWhiteSpace(model.ConRefNumber))
            {
                return true;
            }

            var numericString = model.ConRefNumber.Replace(ESFConstants.ConRefNumberPrefix, string.Empty);

            if (!int.TryParse(numericString, out var contractNumber))
            {
                return true;
            }

            return contractNumber >= ESFConstants.ESFRound2StartConRefNumber;
        }
    }
}