using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Helpers;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Service.Helpers
{
    public class PeriodHelper : IPeriodHelper
    {
        private readonly IReferenceDataService _service;

        public PeriodHelper(IReferenceDataService service)
        {
            _service = service;
        }

        public void CacheCurrentPeriod(JobContextModel jobContextMessage)
        {
            _service.CurrentPeriod = jobContextMessage.CurrentPeriod;
        }
    }
}