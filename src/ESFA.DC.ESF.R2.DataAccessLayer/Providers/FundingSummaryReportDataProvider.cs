using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.DataAccessLayer.Models;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.ReferenceData;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.DataAccessLayer.Providers
{
    public class FundingSummaryReportDataProvider : IFundingSummaryReportDataProvider
    {
        private readonly IReferenceDataService _referenceDataService;

        public FundingSummaryReportDataProvider(
            IReferenceDataService referenceDataService)
        {
            _referenceDataService = referenceDataService;
        }

        public async Task<IOrganisationReferenceData> ProvideOrganisationReferenceDataAsync(int ukprn, CancellationToken cancellationToken)
        {
            return new OrganisationReferenceData();
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
            return new Dictionary<string, IEnumerable<SupplementaryDataYearlyModel>>();
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
