using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.ReferenceData;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary
{
    public interface IFundingSummaryReportDataProvider
    {
        Task<IOrganisationReferenceData> ProvideOrganisationReferenceDataAsync(int ukprn, CancellationToken cancellationToken);

        Task<IReferenceDataVersions> ProvideReferenceDataVersionsAsync(CancellationToken cancellationToken);

        Task<IList<SourceFileModel>> GetImportFilesAsync(int ukPrn, CancellationToken cancellationToken);

        Task<IDictionary<string, IEnumerable<SupplementaryDataYearlyModel>>> GetSupplementaryDataAsync(int endYear, IEnumerable<SourceFileModel> sourceFiles, CancellationToken cancellationToken);

        Task<IEnumerable<ILRFileDetails>> GetIlrFileDetailsAsync(int ukPrn, int collectionYear, CancellationToken cancellationToken);

        Task<IEnumerable<FM70PeriodisedValuesYearly>> GetYearlyIlrDataAsync(int ukprn, string collectionName, int collectionYear, string collectionReturnCode, CancellationToken cancellationToken);
    }
}
