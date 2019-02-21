using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class DeliverableCodeRule01 : IBusinessRuleValidator
    {
        private readonly List<string> _validValues = new List<string>
        {
            Constants.DeliverableCode_ST01,
            Constants.DeliverableCode_CG01,
            Constants.DeliverableCode_CG02,
            Constants.DeliverableCode_SD01,
            Constants.DeliverableCode_SD02,
            Constants.DeliverableCode_SD10,
            Constants.DeliverableCode_NR01,
            Constants.DeliverableCode_RQ01,
            Constants.DeliverableCode_PG01,
            Constants.DeliverableCode_PG03,
            Constants.DeliverableCode_PG04,
            Constants.DeliverableCode_PG05,
            Constants.DeliverableCode_SU15,
            Constants.DeliverableCode_SU21,
            Constants.DeliverableCode_SU22,
            Constants.DeliverableCode_SU23,
            Constants.DeliverableCode_SU24
        };

        public string ErrorMessage => "The DeliverableCode is not valid.";

        public string ErrorName => "DeliverableCode_01";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return _validValues.Any(dc => dc.CaseInsensitiveEquals(model.DeliverableCode?.Trim()));
        }
    }
}
