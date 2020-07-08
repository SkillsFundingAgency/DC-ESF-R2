using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Enum;
using ESFA.DC.ESF.R2.Interfaces.FundingSummary.ESF;
using ESFA.DC.ESF.R2.Interfaces.FundingSummary.ILR;
using ESFA.DC.ESF.R2.Interfaces.ReferenceData;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary
{
    public interface IFundingSummaryReportDataProvider
    {
        Task<IOrganisationReferenceData> ProvideOrganisationReferenceDataAsync(int ukprn, CancellationToken cancellationToken);

        Task<IReferenceDataVersions> ProvideReferenceDataVersionsAsync(CancellationToken cancellationToken);

        Task<IDictionary<string, IDictionary<CollectionYear, IEsfFileData>>> ProvideEsfSuppDataAsync(int ukprn, int collectionYear, CancellationToken cancellationToken);

        Task<IDictionary<CollectionYear, IIlrFileData>> ProvideIlrDataAsync(int ukprn, int collectionYear, CancellationToken cancellationToken);
    }
}
