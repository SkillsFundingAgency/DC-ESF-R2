using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDULNDT : BaseValidationRule, IFieldDefinitionValidator
    {
        private const long Min = 1000000000;
        private const long Max = 9999999999;

        public FDULNDT(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.FD_ULN_DT;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return string.IsNullOrEmpty(model.ULN) ||
                   (long.TryParse(model.ULN, out var uln) && uln >= Min && uln <= Max);
        }
    }
}
