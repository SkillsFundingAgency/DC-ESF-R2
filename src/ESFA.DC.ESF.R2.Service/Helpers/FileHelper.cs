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
        private const string _filenameExtension = @"\.csv";
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

            string[] fileNameParts = fileName.SplitFileName(_filenameExtension);

            if (fileNameParts.Length != 5)
            {
                throw new ArgumentException($"{nameof(JobContextMessageKey.Filename)} is invalid");
            }

            var fileNameDatePart = GetPreparedDateFromFileName(fileName);
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

        public static string GetPreparedDateFromFileName(string fileName)
        {
            if (fileName == null)
            {
                return null;
            }

            var fileNameParts = fileName.SplitFileName(_filenameExtension);
            return fileNameParts.Length < 5 || fileNameParts[3].Length < 8 || fileNameParts[4].Length < 6
                ? string.Empty
                : $"{fileNameParts[3].Substring(0, 4)}/{fileNameParts[3].Substring(4, 2)}/{fileNameParts[3].Substring(6, 2)} " +
                  $"{fileNameParts[4].Substring(0, 2)}:{fileNameParts[4].Substring(2, 2)}:{fileNameParts[4].Substring(4, 2)}";
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
