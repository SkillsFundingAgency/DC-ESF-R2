using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDReferenceTypeAL : BaseValidationRule, IFieldDefinitionValidator
    {
        private const int FieldLength = 20;

        public FDReferenceTypeAL(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "FD_ReferenceType_AL";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return string.IsNullOrEmpty(model.ReferenceType?.Trim()) || model.ReferenceType.Length <= FieldLength;
        }
    }
}
