using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;
using ESFA.DC.ESF.R2.ValidationService.Helpers;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDValueAL : BaseValidationRule, IFieldDefinitionValidator
    {
        private const int IntegerPartLength = 6;

        private const int PrecisionLength = 2;

        public FDValueAL(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.FD_Value_AL;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return string.IsNullOrEmpty(model.Value?.Trim())
                      || (decimal.TryParse(model.Value, out var value) &&
                   DecimalHelper.CheckDecimalLengthAndPrecision(value, IntegerPartLength, PrecisionLength));
        }
    }
}
