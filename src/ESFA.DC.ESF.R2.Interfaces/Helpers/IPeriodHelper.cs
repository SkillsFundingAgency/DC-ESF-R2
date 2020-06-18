using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Helpers
{
    public interface IPeriodHelper
    {
        void CacheCurrentPeriod(IEsfJobContext esfJobContext);
    }
}