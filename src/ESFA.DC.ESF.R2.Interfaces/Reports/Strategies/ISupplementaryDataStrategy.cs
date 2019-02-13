using System.Collections.Generic;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.Strategies
{
    public interface ISupplementaryDataStrategy
    {
        bool IsMatch(string deliverableCode, string referenceType = null);

        void Execute(IEnumerable<SupplementaryDataYearlyModel> data, IList<FundingSummaryReportYearlyValueModel> reportRowYearlyValues);
    }
}