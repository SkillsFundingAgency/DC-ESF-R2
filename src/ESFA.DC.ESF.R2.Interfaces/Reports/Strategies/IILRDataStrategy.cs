using System.Collections.Generic;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.Strategies
{
    public interface IILRDataStrategy
    {
        bool IsMatch(string deliverableCode, List<string> attributeNames = null);

        void Execute(
            IEnumerable<FM70PeriodisedValuesYearly> irlData,
            IList<FundingSummaryReportYearlyValueModel> reportRowYearlyValues);
    }
}