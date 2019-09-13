using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;

namespace ESFA.DC.ESF.R2.ReportingService.Services
{
    public class ReturnPeriodLookup : IReturnPeriodLookup
    {
        private Dictionary<string, string> _returnPeriodLookup = new Dictionary<string, string>
        {
            { Constants.R01, Constants.R12 },
            { Constants.R02, Constants.R13 },
            { Constants.R03, Constants.R14 },
            { Constants.R04, Constants.R14 },
            { Constants.R05, Constants.R14 },
            { Constants.R06, Constants.R14 },
            { Constants.R07, Constants.R14 },
            { Constants.R08, Constants.R14 },
            { Constants.R09, Constants.R14 },
            { Constants.R10, Constants.R14 },
            { Constants.R11, Constants.R14 },
            { Constants.R12, Constants.R14 },
            { Constants.R13, Constants.R14 },
            { Constants.R14, Constants.R14 }
        };

        public string GetReturnPeriodForPreviousCollectionYear(string returnPeriodCode)
        {
            _returnPeriodLookup.TryGetValue(returnPeriodCode, out var previousYearReturnPeriodCode);

            return previousYearReturnPeriodCode;
        }
    }
}
