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
    public class DeliverableCodeRule01 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly List<string> _validValues = new List<string>
        {
            DeliverableCodeConstants.DeliverableCode_ST01,
            DeliverableCodeConstants.DeliverableCode_CG01,
            DeliverableCodeConstants.DeliverableCode_CG02,
            DeliverableCodeConstants.DeliverableCode_SD01,
            DeliverableCodeConstants.DeliverableCode_SD02,
            DeliverableCodeConstants.DeliverableCode_SD10,
            DeliverableCodeConstants.DeliverableCode_NR01,
            DeliverableCodeConstants.DeliverableCode_RQ01,
            DeliverableCodeConstants.DeliverableCode_PG01,
            DeliverableCodeConstants.DeliverableCode_PG03,
            DeliverableCodeConstants.DeliverableCode_PG04,
            DeliverableCodeConstants.DeliverableCode_PG05,
            DeliverableCodeConstants.DeliverableCode_SU15,
            DeliverableCodeConstants.DeliverableCode_SU21,
            DeliverableCodeConstants.DeliverableCode_SU22,
            DeliverableCodeConstants.DeliverableCode_SU23,
            DeliverableCodeConstants.DeliverableCode_SU24
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
