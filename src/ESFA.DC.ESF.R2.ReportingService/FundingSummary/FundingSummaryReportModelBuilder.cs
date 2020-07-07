using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Config;
using ESFA.DC.ESF.R2.Interfaces.Enum;
using ESFA.DC.ESF.R2.Interfaces.FundingSummary.ESF;
using ESFA.DC.ESF.R2.Interfaces.FundingSummary.ILR;
using ESFA.DC.ESF.R2.Interfaces.ReferenceData;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary;
using ESFA.DC.ESF.R2.ReportingService.Constants;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary
{
    public class FundingSummaryReportModelBuilder : IModelBuilder<IEnumerable<FundingSummaryReportTab>>
    {
        private readonly CollectionYear _currentCollectionYear = CollectionYear.Year2021;
        private readonly IReadOnlyDictionary<CollectionYear, string> _collectionYearToStringMap = new Dictionary<CollectionYear, string>
        {
            { CollectionYear.Year1819, ReportingConstants.IlrHeader1819 },
            { CollectionYear.Year1920, ReportingConstants.IlrHeader1920 },
            { CollectionYear.Year2021, ReportingConstants.IlrHeader2021 }
        };

        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IVersionInfo _versionInfo;
        private readonly IFundingSummaryReportDataProvider _fundingSummaryReportDataProvider;

        public FundingSummaryReportModelBuilder(IDateTimeProvider dateTimeProvider, IVersionInfo versionInfo, IFundingSummaryReportDataProvider fundingSummaryReportDataProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            _versionInfo = versionInfo;
            _fundingSummaryReportDataProvider = fundingSummaryReportDataProvider;
        }

        public async Task<IEnumerable<FundingSummaryReportTab>> Build(IEsfJobContext esfJobContext, CancellationToken cancellationToken)
        {
            var tabs = new List<FundingSummaryReportTab>();

            var orgData = await _fundingSummaryReportDataProvider.ProvideOrganisationReferenceDataAsync(esfJobContext.UkPrn, cancellationToken);
            var referenceDataVersions = await _fundingSummaryReportDataProvider.ProvideReferenceDataVersionsAsync();
            var esfSuppData = await _fundingSummaryReportDataProvider.ProvideEsfSuppDataAsync(esfJobContext.UkPrn, esfJobContext.CollectionYear, cancellationToken);
            var ilrData = await _fundingSummaryReportDataProvider.ProvideIlrDataAsync(esfJobContext.UkPrn, esfJobContext.CollectionYear, cancellationToken);

            var footer = BuildFooter(referenceDataVersions);

            foreach (var conRefNumber in orgData.ConRefNumbers)
            {
                var esfSuppDataFile = esfSuppData[conRefNumber];

                var header = BuildHeader(esfJobContext.UkPrn, conRefNumber, orgData.Name, esfSuppDataFile.EsfFile, ilrData);

                var body = BuildBody();

                var model = new FundingSummaryReportTab
                {
                    TabName = conRefNumber,
                    Header = header,
                    Footer = footer,
                    Body = body
                };

                tabs.Add(model);
            }

            return tabs;
        }

        public IDictionary<CollectionYear, IFundingCategory> BuildBody()
        {
            return new Dictionary<CollectionYear, IFundingCategory>();
        }

        public FundingSummaryHeaderModel BuildHeader(
            int ukprn,
            string conRefNumber,
            string orgName,
            IEsfFile esfFile,
            IDictionary<CollectionYear, IIlrFileData> ilrData)
        {
            return new FundingSummaryHeaderModel
            {
                Ukprn = ukprn,
                ProviderName = orgName,
                SecurityClassification = ReportingConstants.Classification,
                ContractReferenceNumber = conRefNumber,
                SupplementaryDataFile = esfFile?.FileName,
                LastSupplementaryDataFileUpdate = esfFile?.SubmittedDateTime,
                IlrHeader = BuildIlrHeader(ilrData)
            };
        }

        public IDictionary<CollectionYear, FundingSummaryIlrHeaderModel> BuildIlrHeader(IDictionary<CollectionYear, IIlrFileData> ilrData)
        {
            return ilrData?.ToDictionary(
                x => x.Key,
                x => new FundingSummaryIlrHeaderModel
                {
                    IlrFileName = x.Value.IlrFile.FileName,
                    IlrFileLastUpdated = x.Value.IlrFile.SubmittedDateTime,
                    IlrFilePrepDate = x.Value.IlrFile.FilePrepDate,
                    CollectionYear = _collectionYearToStringMap[x.Key],
                    CollectionClosedMessage = x.Key == _currentCollectionYear ? null : ReportingConstants.IlrCollectionClosedMessage
                });
        }

        public FundingSummaryFooterModel BuildFooter(IReferenceDataVersions referenceDataVersions)
        {
            var dateTimeNowUtc = _dateTimeProvider.GetNowUtc();
            var dateTimeNowUk = _dateTimeProvider.ConvertUtcToUk(dateTimeNowUtc);

            return new FundingSummaryFooterModel
            {
                ReportGeneratedAt = dateTimeNowUk.ToString(ReportingConstants.TimeFormat) + " on " + dateTimeNowUk.ToString(ReportingConstants.ShortDateFormat),
                ApplicationVersion = _versionInfo.ServiceReleaseVersion,
                LarsData = referenceDataVersions.LarsVersion,
                PostcodeData = referenceDataVersions.PostcodeVersion,
                OrganisationData = referenceDataVersions.OrganisationVersion
            };
        }
    }
}
