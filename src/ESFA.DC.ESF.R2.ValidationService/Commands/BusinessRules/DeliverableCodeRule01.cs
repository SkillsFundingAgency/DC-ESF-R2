using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class DeliverableCodeRule01 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly List<string> _validValues = new List<string>
        {
            ValidationConstants.DeliverableCode_ST01,
            ValidationConstants.DeliverableCode_CG01,
            ValidationConstants.DeliverableCode_CG02,
            ValidationConstants.DeliverableCode_SD01,
            ValidationConstants.DeliverableCode_SD02,
            ValidationConstants.DeliverableCode_SD10,
            ValidationConstants.DeliverableCode_NR01,
            ValidationConstants.DeliverableCode_RQ01,
            ValidationConstants.DeliverableCode_PG01,
            ValidationConstants.DeliverableCode_PG03,
            ValidationConstants.DeliverableCode_PG04,
            ValidationConstants.DeliverableCode_PG05,
            ValidationConstants.DeliverableCode_SU15,
            ValidationConstants.DeliverableCode_SU21,
            ValidationConstants.DeliverableCode_SU22,
            ValidationConstants.DeliverableCode_SU23,
            ValidationConstants.DeliverableCode_SU24
        };

        public DeliverableCodeRule01(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.DeliverableCode_01;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return _validValues.Any(dc => dc.CaseInsensitiveEquals(model.DeliverableCode?.Trim()));
        }
    }
}
