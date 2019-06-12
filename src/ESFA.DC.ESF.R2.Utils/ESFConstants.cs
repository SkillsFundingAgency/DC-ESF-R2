using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Utils
{
    public class ESFConstants
    {
        public const int ESFRound2StartConRefNumber = 5000;

        public const string ConRefNumberPrefix = "ESF-";

        public const string UnitCostDeductionCostType = "Unit Cost Deduction";

        public static readonly List<string> UnitCostDeliverableCodes = new List<string>
        {
            "ST01", "PG01", "PG03", "PG04", "PG05", "PG06", "SD01", "SD02"
        };

        public static readonly Dictionary<int, int> MonthToCollection = new Dictionary<int, int>
        {
            { 1, 6 },
            { 2, 7 },
            { 3, 8 },
            { 4, 9 },
            { 5, 10 },
            { 6, 11 },
            { 7, 12 },
            { 8, 1 },
            { 9, 2 },
            { 10, 3 },
            { 11, 4 },
            { 12, 5 }
        };
    }
}