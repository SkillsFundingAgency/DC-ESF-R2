using System.Collections.Generic;
using System.Linq;
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
        private readonly IIlrDataProvider _ilrDataProvider;

        public FundingSummaryReportDataProvider(
            IReferenceDataService referenceDataService,
            ISupplementaryDataService supplementaryDataService,
            IIlrDataProvider ilrDataProvider)
        {
            _referenceDataService = referenceDataService;
            _supplementaryDataService = supplementaryDataService;
            _ilrDataProvider = ilrDataProvider;
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
            var importFiles = await _supplementaryDataService.GetImportFiles(ukPrn.ToString(), cancellationToken);

            return importFiles;
        }

        public async Task<IDictionary<string, IEnumerable<SupplementaryDataYearlyModel>>> GetSupplementaryDataAsync(int endYear, IEnumerable<SourceFileModel> sourceFiles, CancellationToken cancellationToken)
        {
            var suppData = await _supplementaryDataService.GetSupplementaryData(endYear, sourceFiles, cancellationToken);

            return suppData;
        }

        public async Task<IEnumerable<ILRFileDetails>> GetIlrFileDetailsAsync(int ukprn, IEnumerable<int> ilrYears, CancellationToken cancellationToken)
        {
            var ilrFileDetails = await _ilrDataProvider.GetIlrFileDetailsAsync(ukprn, ilrYears, cancellationToken);

            return ilrFileDetails;
        }

        public async Task<IEnumerable<FM70PeriodisedValuesYearly>> GetYearlyIlrDataAsync(int ukprn, int collectionYear, string collectionReturnCode, IDictionary<int, string> ilrYearsToCollectionDictionary, CancellationToken cancellationToken)
        {
            var ilrData = await _ilrDataProvider.GetIlrPeriodisedValuesAsync(ukprn, collectionYear, collectionReturnCode, ilrYearsToCollectionDictionary, cancellationToken);

            var fm70YearlyData = GroupFm70DataIntoYears(ilrData);

            return fm70YearlyData;
        }

        private IEnumerable<FM70PeriodisedValuesYearly> GroupFm70DataIntoYears(IEnumerable<FM70PeriodisedValues> fm70Data)
        {
            return fm70Data?
                .GroupBy(x => x.FundingYear)
                .Select(x => new FM70PeriodisedValuesYearly
                {
                    FundingYear = x.Key,
                    Fm70PeriodisedValues = x.Select(pv => pv).ToList()
                }) ?? Enumerable.Empty<FM70PeriodisedValuesYearly>();
        }
    }
}
