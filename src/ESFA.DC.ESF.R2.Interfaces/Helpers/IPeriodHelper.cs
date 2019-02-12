using ESFA.DC.JobContextManager.Model.Interface;

namespace ESFA.DC.ESF.R2.Interfaces.Helpers
{
    public interface IPeriodHelper
    {
        void CacheCurrentPeriod(IJobContextMessage jobContextMessage);
    }
}