using System;
using System.Linq;
//using ESFA.DC.ESF.R2.Models;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.JobContextManager.Model.Interface;

namespace ESFA.DC.ESF.R2.Stateless.Mappers
{
    public class JobContextMapper
    {
        //public static JobContextModel MapJobContextToModel(IJobContextMessage message)
        //{
        //    return new JobContextModel
        //    {
        //        JobId = message.JobId,
        //        SubmissionDateTimeUtc = message.SubmissionDateTimeUtc,
        //        FileName = message.KeyValuePairs[JobContextMessageKey.Filename].ToString(),
        //        CurrentPeriod = Convert.ToInt32(message.KeyValuePairs[JobContextMessageKey.ReturnPeriod]),
        //        Tasks = message.Topics[message.TopicPointer].Tasks?.SelectMany(t => t.Tasks).ToList()
        //    };
        //}
    }
}