using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class SupplementaryDataPanelDate03 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly List<string> _deliverableCodes = new List<string>
        {
            Constants.DeliverableCode_NR01,
            Constants.DeliverableCode_ST01,
            Constants.DeliverableCode_RQ01,
            Constants.DeliverableCode_PG01,
            Constants.DeliverableCode_PG03,
            Constants.DeliverableCode_PG04,
            Constants.DeliverableCode_PG05
        };

        public SupplementaryDataPanelDate03(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "SupplementaryDataPanelDate_03";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return model?.SupplementaryDataPanelDate != null
                   || _deliverableCodes.All(dc => !dc.CaseInsensitiveEquals(model?.DeliverableCode));
        }
    }
}