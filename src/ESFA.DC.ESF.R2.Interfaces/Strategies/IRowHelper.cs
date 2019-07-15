using System.Collections.Generic;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Strategies
{
    public interface IRowHelper
    {
        bool IsMatch(RowType rowType);

        void Execute(
            int endYear,
            IList<FundingSummaryModel> reportOutput,
            FundingReportRow row,
            IEnumerable<SupplementaryDataYearlyModel> esfDataModels,
            IEnumerable<FM70PeriodisedValuesYearly> ilrData);
    }
}