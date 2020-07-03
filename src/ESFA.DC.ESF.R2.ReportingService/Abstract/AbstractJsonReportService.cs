using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Cells;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.FileService.Interface;
using ESFA.DC.Serialization.Interfaces;

namespace ESFA.DC.ESF.R2.ReportingService.Abstract
{
    public abstract class AbstractJsonReportService : AbstractReportService
    {
        private readonly IJsonSerializationService _jsonSerializationService;
        private readonly IFileService _fileService;

        protected AbstractJsonReportService(
            IDateTimeProvider dateTimeProvider,
            IFileService fileService,
            IJsonSerializationService jsonSerializationService)
             : base(dateTimeProvider, string.Empty)
        {
            _jsonSerializationService = jsonSerializationService;
            _fileService = fileService;
        }

        public async Task SaveJson<T>(IEsfJobContext esfJobContext, string fileName, T fileValidationResult, CancellationToken cancellationToken)
        {
            using (var stream = await _fileService.OpenWriteStreamAsync(fileName, esfJobContext.BlobContainerName, cancellationToken))
            {
                _jsonSerializationService.Serialize(fileValidationResult, stream);
            }
        }
    }
}
