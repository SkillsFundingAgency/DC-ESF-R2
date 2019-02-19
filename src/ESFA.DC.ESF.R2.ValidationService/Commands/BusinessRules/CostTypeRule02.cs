using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CostTypeRule02 : IBusinessRuleValidator
    {
        private readonly List<string> _SDCodes = new List<string>
        {
            Constants.DeliverableCode_SD01,
            Constants.DeliverableCode_SD02
        };

        public string ErrorMessage => "The CostType is not valid for the DeliverableCode. Please refer to the ESF Supplementary Data supporting documentation for further information.";

        public string ErrorName => "CostType_02";

        public bool IsWarning => false;

        public bool Execute(SupplementaryDataModel model)
        {
            var deliverableCode = model.DeliverableCode?.Trim();
            var costType = model.CostType?.Trim();

            var errorCondition =
                (deliverableCode.CaseInsensitiveEquals(Constants.DeliverableCode_CG01) && !costType.CaseInsensitiveEquals(Constants.CostType_Grant))
                ||
                (deliverableCode.CaseInsensitiveEquals(Constants.DeliverableCode_CG02) && !costType.CaseInsensitiveEquals(Constants.CostType_GrantManagement))
                ||
                (_SDCodes.Any(sd => sd.CaseInsensitiveEquals(deliverableCode)) && !costType.CaseInsensitiveEquals(Constants.CostType_UnitCost))
                ||
                (ESFConstants.UnitCostDeliverableCodes.Any(ucd => ucd.CaseInsensitiveEquals(deliverableCode)) &&
                    (!costType.CaseInsensitiveEquals(Constants.CostType_UnitCost) && !costType.CaseInsensitiveEquals(Constants.CostType_UnitCostDeduction)))
                ||
                ((deliverableCode.CaseInsensitiveEquals(Constants.DeliverableCode_NR01) || deliverableCode.CaseInsensitiveEquals(Constants.DeliverableCode_RQ01)) &&
                    !costType.CaseInsensitiveEquals(Constants.CostType_AuthorisedClaims));

            return !errorCondition;
        }
    }
}
