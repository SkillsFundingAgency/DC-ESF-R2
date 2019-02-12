using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Utils
{
    public class ESFConstants
    {
        public const string UnitCostDeductionCostType = "Unit Cost Deduction";

        public static readonly List<string> UnitCostDeliverableCodes = new List<string>
        {
            "ST01", "FS01", "PG01", "PG02", "PG03", "PG04", "PG05", "PG06", "SU01", "SU02", "SU03", "SU04", "SU05", "SU11", "SU12", "SU13", "SU14", "SU15", "SU21", "SU22", "SU23", "SU24"
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