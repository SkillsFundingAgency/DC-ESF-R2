using System.Text.RegularExpressions;

namespace ESFA.DC.ESF.R2.ValidationService.Constants
{
    public static class ValidationConstants
    {
        public const string CostType_Grant = "Grant";
        public const string CostType_GrantManagement = "Grant Management";
        public const string CostType_UnitCost = "Unit Cost";
        public const string CostType_UnitCostDeduction = "Unit Cost Deduction";
        public const string CostType_OtherCosts = "Other Costs";
        public const string CostType_AuthorisedClaims = "Authorised Claims";

        public const string ReferenceType_Invoice = "Invoice";
        public const string ReferenceType_GrantRecipient = "Grant Recipient";
        public const string ReferenceType_LearnRefNumber = "LearnRefNumber";
        public const string ReferenceType_Other = "Other";

        public const string LarsLearningDeliveryGenre_EOQ = "EOQ";
        public const string LarsLearningDeliveryGenre_EQQ = "EQQ";
        public const string LarsLearningDeliveryGenre_EOU = "EOU";
        public const string LarsLearningDeliveryGenre_IHE = "IHE";

        public const int CalendarYearMinValue = 2019;
        public const int CalendarYearMaxValue = 2021;

        public const int CalendarMonthMinValue = 1;
        public const int CalendarMonthMaxValue = 12;

        public const long TemporaryUln = 9999999999;

        public const string ShortDateFormat = "dd/MM/yyyy";

        public static Regex ReferenceRule03Regex = new Regex(@"^[A-Z,a-z,0-9,\s]*$", RegexOptions.Compiled);
    }
}
