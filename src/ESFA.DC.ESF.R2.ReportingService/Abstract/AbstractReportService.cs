using System;
using ESFA.DC.DateTimeProvider.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.Abstract
{
    public abstract class AbstractReportService
    {
        protected string ReportFileName;

        private readonly IDateTimeProvider _dateTimeProvider;

        protected AbstractReportService(
            IDateTimeProvider dateTimeProvider,
            string taskName)
        {
            _dateTimeProvider = dateTimeProvider;
            TaskName = taskName;
        }

        public string TaskName { get; }

        public string GetExternalFilename(int ukPrn, long jobId, DateTime submissionDateTime, string extension)
        {
            DateTime dateTime = _dateTimeProvider.ConvertUtcToUk(submissionDateTime);
            return $"{ukPrn}/{jobId}/{ReportFileName} {dateTime:yyyyMMdd-HHmmss}{extension}";
        }

        public string GetExternalFilename(string ukPrn, long jobId, DateTime submissionDateTime, string extension)
        {
            DateTime dateTime = _dateTimeProvider.ConvertUtcToUk(submissionDateTime);
            return $"{ukPrn}/{jobId}/{ReportFileName} {dateTime:yyyyMMdd-HHmmss}{extension}";
        }
    }
}
