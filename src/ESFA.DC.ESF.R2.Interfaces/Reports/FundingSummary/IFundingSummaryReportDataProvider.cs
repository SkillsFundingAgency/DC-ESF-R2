using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Enum;
using ESFA.DC.ESF.R2.Interfaces.FundingSummary.ESF;
using ESFA.DC.ESF.R2.Interfaces.FundingSummary.ILR;
using ESFA.DC.ESF.R2.Interfaces.ReferenceData;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary
{
    public interface IFundingSummaryReportDataProvider
    {
        Task<IOrganisationReferenceData> ProvideOrganisationReferenceDataAsync(int ukprn, CancellationToken cancellationToken);

        Task<IReferenceDataVersions> ProvideReferenceDataVersionsAsync(CancellationToken cancellationToken);

        // Task<IDictionary<string, IDictionary<CollectionYear, IEsfFileData>>> ProvideEsfSuppDataAsync(int ukprn, int collectionYear, CancellationToken cancellationToken);

        Task<IList<SourceFileModel>> GetImportFiles(string ukPrn, CancellationToken cancellationToken);

        Task<IDictionary<string, IEnumerable<SupplementaryDataYearlyModel>>> GetSupplementaryData(int endYear, IEnumerable<SourceFileModel> sourceFiles, CancellationToken cancellationToken);

        Task<IEnumerable<ILRFileDetails>> GetIlrFileDetails(int ukPrn, int collectionYear, CancellationToken cancellationToken);

        Task<IEnumerable<FM70PeriodisedValuesYearly>> GetYearlyIlrData(int ukprn, string collectionName, int collectionYear, string collectionReturnCode, CancellationToken cancellationToken);
        //Task<IDictionary<CollectionYear, IIlrFileData>> ProvideIlrDataAsync(int ukprn, int collectionYear, CancellationToken cancellationToken);
    }
}
