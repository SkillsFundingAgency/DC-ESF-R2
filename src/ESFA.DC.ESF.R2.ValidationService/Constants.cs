using System.Text.RegularExpressions;

namespace ESFA.DC.ESF.R2.ValidationService
{
    public static class Constants
    {
        public const string CostType_Grant = "Grant";
        public const string CostType_GrantManagement = "Grant Management";
        public const string CostType_UnitCost = "Unit Cost";
        public const string CostType_UnitCostDeduction = "Unit Cost Deduction";
        public const string CostType_OtherCosts = "Other Costs";
        public const string CostType_AuthorisedClaims = "Authorised Claims";

        public const string DeliverableCode_ST01 = "ST01";
        public const string DeliverableCode_CG01 = "CG01";
        public const string DeliverableCode_CG02 = "CG02";
        public const string DeliverableCode_SD01 = "SD01";
        public const string DeliverableCode_SD02 = "SD02";
        public const string DeliverableCode_SD10 = "SD10";
        public const string DeliverableCode_NR01 = "NR01";
        public const string DeliverableCode_RQ01 = "RQ01";
        public const string DeliverableCode_PG01 = "PG01";
        public const string DeliverableCode_PG03 = "PG03";
        public const string DeliverableCode_PG04 = "PG04";
        public const string DeliverableCode_PG05 = "PG05";
        public const string DeliverableCode_SU15 = "SU15";
        public const string DeliverableCode_SU21 = "SU21";
        public const string DeliverableCode_SU22 = "SU22";
        public const string DeliverableCode_SU23 = "SU23";
        public const string DeliverableCode_SU24 = "SU24";

        public const string ReferenceType_Invoice = "Invoice";
        public const string ReferenceType_GrantRecipient = "Grant Recipient";
        public const string ReferenceType_LearnRefNumber = "LearnRefNumber";
        public const string ReferenceType_Other = "Other";

        public const string LarsLearningDeliveryGenre_EOQ = "EOQ";
        public const string LarsLearningDeliveryGenre_EQQ = "EQQ";
        public const string LarsLearningDeliveryGenre_EOU = "EOU";
        public const string LarsLearningDeliveryGenre_IHE = "IHE";

        public static Regex ReferenceRule03Regex = new Regex(@"^[A-Z,a-z,0-9,\s]*$", RegexOptions.Compiled);
    }
}
