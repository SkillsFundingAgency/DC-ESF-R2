using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Helpers;

namespace ESFA.DC.ESF.R2.Service.Helpers
{
    public class PeriodHelper : IPeriodHelper
    {
        private readonly IReferenceDataService _service;

        public PeriodHelper(IReferenceDataService service)
        {
            _service = service;
        }

        public void CacheCurrentPeriod(IEsfJobContext esfJobContext)
        {
            _service.CurrentPeriod = esfJobContext.CurrentPeriod;
        }
    }
}