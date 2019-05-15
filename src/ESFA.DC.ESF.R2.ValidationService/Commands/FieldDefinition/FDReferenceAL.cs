using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDReferenceAL : BaseValidationRule, IFieldDefinitionValidator
    {
        private const int FieldLength = 100;

        public FDReferenceAL(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "FD_Reference_AL";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return string.IsNullOrEmpty(model.Reference?.Trim()) || model.Reference.Length <= FieldLength;
        }
    }
}
