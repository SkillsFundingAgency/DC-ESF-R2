using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class SupplementaryDataPanelDate03 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly List<string> _deliverableCodes = new List<string>
        {
            ValidationConstants.DeliverableCode_NR01,
            ValidationConstants.DeliverableCode_ST01,
            ValidationConstants.DeliverableCode_RQ01,
            ValidationConstants.DeliverableCode_PG01,
            ValidationConstants.DeliverableCode_PG03,
            ValidationConstants.DeliverableCode_PG04,
            ValidationConstants.DeliverableCode_PG05
        };

        public SupplementaryDataPanelDate03(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.SupplementaryDataPanelDate_03;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return model?.SupplementaryDataPanelDate != null
                   || _deliverableCodes.All(dc => !dc.CaseInsensitiveEquals(model?.DeliverableCode));
        }
    }
}