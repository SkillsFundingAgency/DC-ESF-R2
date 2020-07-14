using System;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Stateless.Context;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.JobContextManager.Model.Interface;

namespace ESFA.DC.ESF.R2.Stateless.Mappers
{
    public class JobContextMapper
    {
        public static IEsfJobContext MapJobContextToModel(IJobContextMessage message)
        {
            var collectionYear = message.KeyValuePairs[JobContextMessageKey.CollectionYear].ToString();

            return new EsfJobContext
            {
                JobId = message.JobId,
                UkPrn = Convert.ToInt32(message.KeyValuePairs[JobContextMessageKey.UkPrn]),
                BlobContainerName = message.KeyValuePairs[JobContextMessageKey.Container].ToString(),
                SubmissionDateTimeUtc = message.SubmissionDateTimeUtc,
                FileName = message.KeyValuePairs[JobContextMessageKey.Filename].ToString(),
                CurrentPeriod = Convert.ToInt32(message.KeyValuePairs[JobContextMessageKey.ReturnPeriod]),
                CollectionYear = Convert.ToInt32(message.KeyValuePairs[JobContextMessageKey.CollectionYear]),
                Tasks = message.Topics[message.TopicPointer].Tasks?.SelectMany(t => t.Tasks).ToList(),
                IlrReferenceDataKey = message.KeyValuePairs.ContainsKey(JobContextMessageKey.IlrReferenceData)
                    ? message.KeyValuePairs[JobContextMessageKey.IlrReferenceData].ToString()
                    : null,
                CollectionName = message.KeyValuePairs[JobContextMessageKey.CollectionName].ToString(),
                StartCollectionYearAbbreviation = collectionYear.Substring(0, 2),
                EndCollectionYearAbbreviation = collectionYear.Substring(2)
            };
        }
    }
}