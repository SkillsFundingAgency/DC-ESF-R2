using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.CrossRecord
{
    public class Duplicate01 : BaseValidationRule, ICrossRecordValidator
    {
        public Duplicate01(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.Duplicate_01;

        public bool IsWarning => false;

        public bool IsValid(ICollection<SupplementaryDataModel> allRecords, SupplementaryDataModel model)
        {
            return allRecords != null && allRecords.Count(
                          m => m.ConRefNumber == model.ConRefNumber &&
                               m.DeliverableCode == model.DeliverableCode &&
                               m.CalendarYear == model.CalendarYear &&
                               m.CalendarMonth == model.CalendarMonth &&
                               m.CostType == model.CostType &&
                               m.ReferenceType == model.ReferenceType &&
                               m.Reference == model.Reference) == 1;
        }
    }
}
