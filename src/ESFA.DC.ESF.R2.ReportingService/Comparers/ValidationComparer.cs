using System;
using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ReportingService.Comparers
{
    public class ValidationComparer : IComparer<ValidationErrorModel>, IValidationComparer
    {
        public int Compare(ValidationErrorModel first, ValidationErrorModel second)
        {
            if (first == null && second == null)
            {
                return 0;
            }

            if (first == null)
            {
                return -1;
            }

            if (second == null)
            {
                return 1;
            }

            if (!first.IsWarning && second.IsWarning)
            {
                return 1;
            }

            if (first.IsWarning && !second.IsWarning)
            {
                return -1;
            }

            var cmp = string.Compare(first.RuleName, second.RuleName, StringComparison.OrdinalIgnoreCase);
            if (cmp != 0)
            {
                return cmp;
            }

            return 0;
        }
    }
}
