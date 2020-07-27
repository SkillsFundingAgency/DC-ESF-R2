using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.FundingSummary;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary
{
    public class FundingSummaryReportDataProvider : IFundingSummaryReportDataProvider
    {
        private readonly IReferenceDataService _referenceDataService;
        private readonly ISupplementaryDataService _supplementaryDataService;

        public FundingSummaryReportDataProvider(
            IReferenceDataService referenceDataService,
            ISupplementaryDataService supplementaryDataService)
        {
            _referenceDataService = referenceDataService;
            _supplementaryDataService = supplementaryDataService;
        }

        public async Task<IOrganisationReferenceData> ProvideOrganisationReferenceDataAsync(int ukprn, CancellationToken cancellationToken)
        {
            return new OrganisationReferenceData
            {
                ConRefNumbers = await _referenceDataService.GetContractAllocationsForUkprn(ukprn, cancellationToken) ?? new List<string>(),
                Name = _referenceDataService.GetProviderName(ukprn, cancellationToken),
                Ukprn = ukprn
            };
        }

        public async Task<IReferenceDataVersions> ProvideReferenceDataVersionsAsync(CancellationToken cancellationToken)
        {
            return new ReferenceDataVersions
            {
                LarsVersion = await _referenceDataService.GetLarsVersion(cancellationToken),
                OrganisationVersion = await _referenceDataService.GetOrganisationVersion(cancellationToken),
                PostcodeVersion = await _referenceDataService.GetPostcodeVersion(cancellationToken)
            };
        }

        public async Task<IList<SourceFileModel>> GetImportFilesAsync(int ukPrn, CancellationToken cancellationToken)
        {
            return new List<SourceFileModel>();
        }

        public async Task<IDictionary<string, IEnumerable<SupplementaryDataYearlyModel>>> GetSupplementaryDataAsync(int endYear, IEnumerable<SourceFileModel> sourceFiles, CancellationToken cancellationToken)
        {
            var suppData = await _supplementaryDataService.GetSupplementaryData(endYear, sourceFiles, cancellationToken);

            return suppData;
        }

        public async Task<IEnumerable<ILRFileDetails>> GetIlrFileDetailsAsync(int ukPrn, int collectionYear, CancellationToken cancellationToken)
        {
            return new List<ILRFileDetails>();
        }

        public async Task<IEnumerable<FM70PeriodisedValuesYearly>> GetYearlyIlrDataAsync(int ukprn, string collectionName, int collectionYear, string collectionReturnCode, CancellationToken cancellationToken)
        {
            return new List<FM70PeriodisedValuesYearly>();
        }
    }
}
