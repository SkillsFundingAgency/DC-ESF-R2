using System;
using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Database.EF
{
    public partial class ValidationErrorMessage
    {
        public int ValidationErrorMessageId { get; set; }
        public string RuleName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
