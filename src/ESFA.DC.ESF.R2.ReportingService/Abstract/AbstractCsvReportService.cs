using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using ESFA.DC.CsvService.Interface;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.FileService.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.Abstract
{
    public abstract class AbstractCsvReportService<TModel, TClassMap> : AbstractReportService
         where TClassMap : ClassMap<TModel>
    {
        private readonly ICsvFileService _csvFileService;

        protected AbstractCsvReportService(
            IDateTimeProvider dateTimeProvider,
            ICsvFileService csvFileService,
            string taskName)
             : base(dateTimeProvider, taskName)
        {
            _csvFileService = csvFileService;
        }

        public async Task WriteCsv(IEsfJobContext esfJobContext, string fileName, IEnumerable<TModel> models, CancellationToken cancellationToken)
        {
            await _csvFileService.WriteAsync<TModel, TClassMap>(models, fileName, esfJobContext.BlobContainerName, cancellationToken);
        }
    }
}
