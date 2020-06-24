using System;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Builders;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.JobContext.Interface;

namespace ESFA.DC.ESF.R2.Service.Builders
{
    public class SourceFileModelBuilder : ISourceFileModelBuilder
    {
        private const string _filenameExtension = @"\.csv";

        public ISourceFileModel Build(IEsfJobContext esfJobContext)
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
                JobId = jobId
            };
        }

        public ISourceFileModel BuildDefault(IEsfJobContext esfJobContext)
        {
            return new SourceFileModel();
        }

        private string GetPreparedDateFromFileName(string fileName)
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
    }
}
