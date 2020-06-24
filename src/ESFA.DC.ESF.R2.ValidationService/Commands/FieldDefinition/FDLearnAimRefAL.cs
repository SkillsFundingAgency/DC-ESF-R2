using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDLearnAimRefAL : BaseValidationRule, IFieldDefinitionValidator
    {
        private const int FieldLength = 8;

        public FDLearnAimRefAL(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.FD_LearnAimRef_AL;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return string.IsNullOrWhiteSpace(model.LearnAimRef) || model.LearnAimRef.Length <= FieldLength;
        }
    }
}
