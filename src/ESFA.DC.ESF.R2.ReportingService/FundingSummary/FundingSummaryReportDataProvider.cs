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
using ESFA.DC.ESF.R2.ReportingService.Constants;
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

        public async Task<IEnumerable<ILRFileDetails>> GetIlrFileDetailsAsync(int ukprn, int collectionYear, CancellationToken cancellationToken)
        {
            var ilrFileDetails = await _ilrDataProvider.GetIlrFileDetailsAsync(ukprn, cancellationToken);

            return ilrFileDetails;
        }

        public async Task<IEnumerable<FM70PeriodisedValuesYearly>> GetYearlyIlrDataAsync(int ukprn, string collectionName, int collectionYear, string collectionReturnCode, CancellationToken cancellationToken)
        {
            var ilrData = await _ilrDataProvider.GetIlrPeriodisedValuesAsync(ukprn, collectionReturnCode, cancellationToken);

            var fm70YearlyData = GroupFm70DataIntoYears(collectionYear, ilrData);

            return fm70YearlyData;
        }

        private IEnumerable<FM70PeriodisedValuesYearly> GroupFm70DataIntoYears(int endYear, IEnumerable<FM70PeriodisedValues> fm70Data)
        {
            var yearlyFm70Data = new List<FM70PeriodisedValuesYearly>();
            if (fm70Data == null)
            {
                return yearlyFm70Data;
            }

            for (var i = ReportingConstants.StartYear; i <= endYear; i++)
            {
                yearlyFm70Data.Add(new FM70PeriodisedValuesYearly
                {
                    FundingYear = i,
                    Fm70PeriodisedValues = new List<FM70PeriodisedValues>()
                });
            }

            var fm70DataModels = fm70Data.ToList();

            if (!fm70DataModels.Any())
            {
                return yearlyFm70Data;
            }

            foreach (var yearlyModel in yearlyFm70Data)
            {
                yearlyModel.Fm70PeriodisedValues = fm70DataModels
                    .Where(sd => sd.FundingYear == yearlyModel.FundingYear)
                    .ToList();
            }

            return yearlyFm70Data;
        }
    }
}
