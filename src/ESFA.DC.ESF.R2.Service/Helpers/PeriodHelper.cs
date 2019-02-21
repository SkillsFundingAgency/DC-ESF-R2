using System;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Helpers;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.JobContextManager.Model.Interface;

namespace ESFA.DC.ESF.R2.Service.Helpers
{
    public class PeriodHelper : IPeriodHelper
    {
        private readonly IReferenceDataService _service;

        public PeriodHelper(IReferenceDataService service)
        {
            _service = service;
        }

        public void CacheCurrentPeriod(IJobContextMessage jobContextMessage)
        {
            _service.CurrentPeriod = Convert.ToInt32(jobContextMessage.KeyValuePairs[JobContextMessageKey.ReturnPeriod]);
        }
    }
}