using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.ReportingService.Constants;

namespace ESFA.DC.ESF.R2.ReportingService.Services
{
    public class ReturnPeriodLookup : IReturnPeriodLookup
    {
        private Dictionary<string, string> _returnPeriodLookup = new Dictionary<string, string>
        {
            { ReportingConstants.R01, ReportingConstants.R12 },
            { ReportingConstants.R02, ReportingConstants.R13 },
            { ReportingConstants.R03, ReportingConstants.R14 },
            { ReportingConstants.R04, ReportingConstants.R14 },
            { ReportingConstants.R05, ReportingConstants.R14 },
            { ReportingConstants.R06, ReportingConstants.R14 },
            { ReportingConstants.R07, ReportingConstants.R14 },
            { ReportingConstants.R08, ReportingConstants.R14 },
            { ReportingConstants.R09, ReportingConstants.R14 },
            { ReportingConstants.R10, ReportingConstants.R14 },
            { ReportingConstants.R11, ReportingConstants.R14 },
            { ReportingConstants.R12, ReportingConstants.R14 },
            { ReportingConstants.R13, ReportingConstants.R14 },
            { ReportingConstants.R14, ReportingConstants.R14 }
        };

        public string GetReturnPeriodForPreviousCollectionYear(string returnPeriodCode)
        {
            _returnPeriodLookup.TryGetValue(returnPeriodCode, out var previousYearReturnPeriodCode);

            return previousYearReturnPeriodCode;
        }
    }
}
