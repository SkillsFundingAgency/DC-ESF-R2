using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ConRefNumberRule02
    {
        public string ErrorMessage => "The ConRefNumber is not a valid ConRefNumber for ESF Round 2.";

        public string ErrorName => "ConRefNumber_02";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
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