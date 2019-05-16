using System;
using System.Linq;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.JobContextManager.Model.Interface;

namespace ESFA.DC.ESF.R2.Stateless.Mappers
{
    public class JobContextMapper
    {
        public static JobContextModel MapJobContextToModel(IJobContextMessage message)
        {
            return new JobContextModel
            {
                JobId = message.JobId,
                UkPrn = Convert.ToInt32(message.KeyValuePairs[JobContextMessageKey.UkPrn]),
                BlobContainerName = message.KeyValuePairs[JobContextMessageKey.Container].ToString(),
                SubmissionDateTimeUtc = message.SubmissionDateTimeUtc,
                FileName = message.KeyValuePairs[JobContextMessageKey.Filename].ToString(),
                CurrentPeriod = Convert.ToInt32(message.KeyValuePairs[JobContextMessageKey.ReturnPeriod]),
                Tasks = message.Topics[message.TopicPointer].Tasks?.SelectMany(t => t.Tasks).ToList(),
                IlrReferenceDataKey = message.KeyValuePairs.ContainsKey(JobContextMessageKey.IlrReferenceData)
                    ? message.KeyValuePairs[JobContextMessageKey.IlrReferenceData].ToString()
                    : null
            };
        }
    }
}