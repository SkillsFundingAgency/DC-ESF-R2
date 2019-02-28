using System.Collections.Generic;
using ESFA.DC.ESF.R2.Models.Ilr;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.Strategies
{
    public interface IILRDataStrategy
    {
        bool IsMatch(string deliverableCode, List<string> attributeNames = null);

        void Execute(
            IEnumerable<FM70PeriodisedValuesYearlyModel> irlData,
            IList<FundingSummaryReportYearlyValueModel> reportRowYearlyValues);
    }
}