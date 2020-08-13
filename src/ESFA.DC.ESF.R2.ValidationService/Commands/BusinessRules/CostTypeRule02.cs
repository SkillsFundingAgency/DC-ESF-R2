using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.Constants;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CostTypeRule02 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly List<string> _SDCodes = new List<string>
        {
            DeliverableCodeConstants.DeliverableCode_SD01,
            DeliverableCodeConstants.DeliverableCode_SD02
        };

        public CostTypeRule02(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.CostType_02;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            var deliverableCode = model.DeliverableCode?.Trim();
            var costType = model.CostType?.Trim();

            var errorCondition =
                (deliverableCode.CaseInsensitiveEquals(DeliverableCodeConstants.DeliverableCode_CG01) && !costType.CaseInsensitiveEquals(ValidationConstants.CostType_Grant))
                ||
                (deliverableCode.CaseInsensitiveEquals(DeliverableCodeConstants.DeliverableCode_CG02)
                    && (!costType.CaseInsensitiveEquals(ValidationConstants.CostType_GrantManagement) && !costType.CaseInsensitiveEquals(ValidationConstants.CostType_OtherCosts)))
                ||
                (_SDCodes.Any(sd => sd.CaseInsensitiveEquals(deliverableCode)) && !costType.CaseInsensitiveEquals(ValidationConstants.CostType_UnitCost))
                ||
                (ESFConstants.UnitCostDeliverableCodes.Any(ucd => ucd.CaseInsensitiveEquals(deliverableCode)) &&
                    (!costType.CaseInsensitiveEquals(ValidationConstants.CostType_UnitCost) && !costType.CaseInsensitiveEquals(ValidationConstants.CostType_UnitCostDeduction)))
                ||
                ((deliverableCode.CaseInsensitiveEquals(DeliverableCodeConstants.DeliverableCode_NR01) || deliverableCode.CaseInsensitiveEquals(DeliverableCodeConstants.DeliverableCode_RQ01)) &&
                    !costType.CaseInsensitiveEquals(ValidationConstants.CostType_AuthorisedClaims));

            return !errorCondition;
        }
    }
}
