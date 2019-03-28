﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Helpers;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.JobContextManager.Model.Interface;

namespace ESFA.DC.ESF.R2.Service.Helpers
{
    public class FileHelper : IFileHelper
    {
        private readonly IESFProviderService _providerService;

        public FileHelper(IESFProviderService providerService)
        {
            _providerService = providerService;
        }

        public SourceFileModel GetSourceFileData(JobContextModel jobContextModel)
        {
            if (string.IsNullOrWhiteSpace(jobContextModel.FileName))
            {
                throw new ArgumentException($"{nameof(JobContextMessageKey.Filename)} is required");
            }

            var fileName = jobContextModel.FileName;

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

            var jobId = jobContextModel.JobId;

            return new SourceFileModel
            {
                ConRefNumber = fileNameParts[2],
                UKPRN = fileNameParts[1],
                FileName = fileName,
                PreparationDate = preparationDateTime,
                SuppliedDate = jobContextModel.SubmissionDateTimeUtc,
                JobId = jobId
            };
        }

        public async Task<IList<SupplementaryDataLooseModel>> GetESFRecords(SourceFileModel sourceFileModel, CancellationToken cancellationToken)
        {
            return await _providerService.GetESFRecordsFromFile(sourceFileModel, cancellationToken);
        }
    }
}
