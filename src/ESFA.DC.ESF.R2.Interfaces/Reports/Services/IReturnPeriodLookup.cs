using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.Services
{
    public interface IReturnPeriodLookup
    {
        string GetReturnPeriodForPreviousCollectionYear(string returnPeriodCode);
    }
}
