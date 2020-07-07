using System.Collections.Generic;
using System.Linq;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Config;
using ESFA.DC.ESF.R2.Interfaces.Enum;
using ESFA.DC.ESF.R2.Interfaces.ReferenceData;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.ReportingService.Constants;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary
{
    public class FundingSummaryReportModelBuilder : IModelBuilder<IDictionary<string, FundingSummaryReportModel>>
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
        private readonly IReferenceDataRoot _referenceDataRoot;

        public FundingSummaryReportModelBuilder(IDateTimeProvider dateTimeProvider, IVersionInfo versionInfo, IReferenceDataRoot referenceDataRoot)
        {
            _dateTimeProvider = dateTimeProvider;
            _versionInfo = versionInfo;
            _referenceDataRoot = referenceDataRoot;
        }

        public IDictionary<string, FundingSummaryReportModel> Build(IEsfJobContext esfJobContext)
        {
            var modelDictionary = new Dictionary<string, FundingSummaryReportModel>();

            //// ref data block
            //// not sure where this comes from yet
            var orgData = _referenceDataRoot.OrganisationReferenceData;
            //var esfSuppFiles = new EsfSuppDataFilesStub();
            //var ilrFiles = new IlrFilesStub();
            //var referenceDataVersions = new ReferenceDataVersionsStub();
            //// ref data block end

            var footer = BuildFooter(_referenceDataRoot.ReferenceDataVersions);

            foreach (var conRefNumber in orgData.ConRefNumbers)
            {
                var esfSuppDataFile = _referenceDataRoot.EsfSuppDataFileForConRefNumbers[conRefNumber];

                var header = BuildHeader(esfJobContext.UkPrn, conRefNumber, orgData.Name, esfSuppDataFile, _referenceDataRoot.IlrFileForCollectionYear);

                var model = new FundingSummaryReportModel
                {
                    Header = header,
                    Footer = footer,
                    Body = BuildBody()
                };

                modelDictionary.Add(conRefNumber, model);
            }

            return modelDictionary;
        }

        public IDictionary<CollectionYear, IFundingCategory> BuildBody()
        {
            return new Dictionary<CollectionYear, IFundingCategory>();
        }

        public FundingSummaryHeaderModel BuildHeader(
            int ukprn,
            string conRefNumber,
            string orgName,
            IEsfSuppDataFile esfSuppDataFile,
            IDictionary<CollectionYear, IIlrFile> ilrFiles)
        {
            return new FundingSummaryHeaderModel
            {
                Ukprn = ukprn,
                ProviderName = orgName,
                SecurityClassification = ReportingConstants.Classification,
                ContractReferenceNumber = conRefNumber,
                SupplementaryDataFile = esfSuppDataFile?.FileName,
                LastSupplementaryDataFileUpdate = esfSuppDataFile?.SubmittedDateTime,
                IlrHeader = BuildIlrHeader(ilrFiles)
            };
        }

        public IDictionary<CollectionYear, FundingSummaryIlrHeaderModel> BuildIlrHeader(IDictionary<CollectionYear, IIlrFile> ilrFiles)
        {
            return ilrFiles?.ToDictionary(
                x => x.Key,
                x => new FundingSummaryIlrHeaderModel
                {
                    IlrFileName = x.Value.FileName,
                    IlrFileLastUpdated = x.Value.SubmittedDateTime,
                    IlrFilePrepDate = x.Value.FilePrepDate,
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
