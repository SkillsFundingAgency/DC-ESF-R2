using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Helpers;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.JobContext.Interface;

namespace ESFA.DC.ESF.R2.Service.Helpers
{
    public class FileHelper : IFileHelper
    {
        private readonly IESFProviderService _providerService;

        public FileHelper(IESFProviderService providerService)
        {
            _providerService = providerService;
        }

        public SourceFileModel GetSourceFileData(IEsfJobContext esfJobContext)
        {
            if (string.IsNullOrWhiteSpace(esfJobContext.FileName))
            {
                throw new ArgumentException($"{nameof(JobContextMessageKey.Filename)} is required");
            }

            var fileName = esfJobContext.FileName;

            string[] fileNameParts = FileNameHelper.SplitFileName(fileName);

            if (fileNameParts.Length != 5)
            {
                throw new ArgumentException($"{nameof(JobContextMessageKey.Filename)} is invalid");
            }

            var fileNameDatePart = FileNameHelper.GetPreparedDateFromFileName(fileName);
            if (!DateTime.TryParse(fileNameDatePart, out var preparationDateTime))
            {
                throw new ArgumentException($"{nameof(JobContextMessageKey.Filename)} is invalid");
            }

            var jobId = esfJobContext.JobId;

            return new SourceFileModel
            {
                ConRefNumber = fileNameParts[2],
                UKPRN = fileNameParts[1],
                FileName = fileName,
                PreparationDate = preparationDateTime,
                SuppliedDate = esfJobContext.SubmissionDateTimeUtc,
                JobId = jobId
            };
        }

        public async Task<IList<SupplementaryDataLooseModel>> GetESFRecords(
            IEsfJobContext esfJobContext,
            SourceFileModel sourceFileModel,
            CancellationToken cancellationToken)
        {
            return await _providerService.GetESFRecordsFromFile(esfJobContext, sourceFileModel, cancellationToken);
        }
    }
}
