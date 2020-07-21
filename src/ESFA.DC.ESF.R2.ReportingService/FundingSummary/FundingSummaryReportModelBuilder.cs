using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Config;
using ESFA.DC.ESF.R2.Interfaces.Constants;
using ESFA.DC.ESF.R2.Interfaces.ReferenceData;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.ReportingService.Constants;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ILR.DataService.Models;
using ESFA.DC.Logging.Interfaces;
using FundingSummaryModel = ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.FundingSummaryModel;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary
{
    public class FundingSummaryReportModelBuilder : IModelBuilder<IEnumerable<FundingSummaryReportTab>>
    {
        private const string NotApplicable = "Not Applicable";

        private readonly HashSet<string> _ilrAttributeSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            FundingSummaryReportConstants.IlrStartEarningsAttribute,
            FundingSummaryReportConstants.IlrAchievementEarningsAttribute,
            FundingSummaryReportConstants.IlrAAdditionalProgCostEarningsAttribute,
            FundingSummaryReportConstants.IlrProgressionEarningsAttribute
        };

        private readonly IFundingSummaryReportDataProvider _dataProvider;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IVersionInfo _versionInfo;
        private readonly ILogger _logger;

        public FundingSummaryReportModelBuilder(
            IDateTimeProvider dateTimeProvider,
            IFundingSummaryReportDataProvider dataProvider,
            IVersionInfo versionInfo,
            ILogger logger)
        {
            _dateTimeProvider = dateTimeProvider;
            _versionInfo = versionInfo;
            _logger = logger;
            _dataProvider = dataProvider;
        }

        public async Task<IEnumerable<FundingSummaryReportTab>> Build(IEsfJobContext esfJobContext, CancellationToken cancellationToken)
        {
            var ukPrn = esfJobContext.UkPrn;
            IEnumerable<string> conRefNumbers = new List<string>();
            var dateTimeNowUtc = _dateTimeProvider.GetNowUtc();
            var dateTimeNowUk = _dateTimeProvider.ConvertUtcToUk(dateTimeNowUtc);
            var reportGenerationTime = dateTimeNowUk.ToString("HH:mm:ss") + " on " + dateTimeNowUk.ToString("dd/MM/yyyy");

            var referenceDataVersions = await _dataProvider.ProvideReferenceDataVersionsAsync(cancellationToken);
            var orgData = await _dataProvider.ProvideOrganisationReferenceDataAsync(ukPrn, cancellationToken);

            if (!orgData.ConRefNumbers.Any())
            {
                conRefNumbers = new List<string> { NotApplicable };
            }
            else
            {
                conRefNumbers = orgData.ConRefNumbers;
            }

            var collectionYear = Convert.ToInt32($"20{esfJobContext.CollectionYear.ToString().Substring(0, 2)}");

            var sourceFiles = await _dataProvider.GetImportFiles(esfJobContext.UkPrn.ToString(), cancellationToken);

            _logger.LogDebug($"{sourceFiles.Count} esf files found for ukprn {ukPrn} and collection year 20{esfJobContext.CollectionYear.ToString().Substring(0, 2)}.");

            var supplementaryData = await _dataProvider.GetSupplementaryData(collectionYear, sourceFiles, cancellationToken);

            var ilrYearlyFileData = await _dataProvider.GetIlrFileDetails(ukPrn, collectionYear, cancellationToken);
            var fm70YearlyData = await _dataProvider.GetYearlyIlrData(ukPrn, esfJobContext.CollectionName, collectionYear, esfJobContext.ReturnPeriod, cancellationToken);

            var periodisedEsf = PeriodiseEsfSuppData(supplementaryData);
            var periodisedILR = PeriodiseIlr(fm70YearlyData.SelectMany(x => x.Fm70PeriodisedValues));

            var fundingSummaryTabs = new List<FundingSummaryReportTab>();

            foreach (var conRefNumber in conRefNumbers)
            {
                var file = sourceFiles.FirstOrDefault(sf => sf.ConRefNumber.CaseInsensitiveEquals(conRefNumber));

                FundingSummaryHeaderModel fundingSummaryHeaderModel = PopulateReportHeader(file, ilrYearlyFileData, ukPrn, orgData.Name, conRefNumber, cancellationToken);
                FundingSummaryFooterModel fundingSummaryFooterModel = PopulateReportFooter(referenceDataVersions, reportGenerationTime, cancellationToken);

                var fundingSummaryModels = PopulateReportData(periodisedEsf[conRefNumber], periodisedILR[conRefNumber]);

                fundingSummaryTabs.Add(new FundingSummaryReportTab
                {
                    TabName = conRefNumber,
                    Header = fundingSummaryHeaderModel,
                    Footer = fundingSummaryFooterModel,
                    Body = fundingSummaryModels
                });
            }

            return fundingSummaryTabs;
        }

        public IEnumerable<FundingSummaryModel> PopulateReportData(
            IDictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>> periodisedEsf,
            IDictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>> periodisedILR)
        {
            var model = periodisedEsf.Select(x => new FundingSummaryModel
            {
                Year = x.Key,
                LearnerAssessmentPlans = BuildLearnerAssessmentPlans(x.Key, x.Value, periodisedILR[x.Key]),
                RegulatedLearnings = BuildRegulatedLearning(x.Key, x.Value, periodisedILR[x.Key]),
                NonRegulatedLearnings = BuildNonRegulatedLearning(x.Key, x.Value, periodisedILR[x.Key]),
                Progressions = BuildProgressions(x.Key, x.Value, periodisedILR[x.Key]),
                CommunityGrants = BuildCommunityGrants(x.Key, x.Value),
                SpecificationDefineds = BuildSpecificationDefined(x.Key, x.Value),
            });

            return model;
        }

        public LearnerAssessmentPlan BuildLearnerAssessmentPlans(int year, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues, IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues)
        {
            var headers = FundingSummaryReportConstants.GroupHeaderDictionary[year];
            var ilrST01 = ilrValues[DeliverableCodeConstants.DeliverableCode_ST01].Where(x => _ilrAttributeSet.Contains(x.AttributeName));
            var esfST01 = esfValues[DeliverableCodeConstants.DeliverableCode_ST01];

            return new LearnerAssessmentPlan
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_LearnerAssessment, headers),
                IlrST01 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_ST01, ilrST01),
                EsfST01 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_ST01, esfST01),
            };
        }

        public RegulatedLearning BuildRegulatedLearning(int year, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues, IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues)
        {
            var headers = FundingSummaryReportConstants.GroupHeaderDictionary[year];
            var ilrRQ01Start = ilrValues[DeliverableCodeConstants.DeliverableCode_RQ01].Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.IlrStartEarningsAttribute));
            var ilrRQ01Ach = ilrValues[DeliverableCodeConstants.DeliverableCode_RQ01].Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.IlrAchievementEarningsAttribute));
            var esfRQ01 = esfValues[DeliverableCodeConstants.DeliverableCode_RQ01].Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.EsfReferenceTypeAuthorisedClaims));

            return new RegulatedLearning
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_RegulatedLearning, headers),
                IlrRQ01StartFunding = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_RQ01_Start, ilrRQ01Start),
                IlrRQ01AchFunding = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_RQ01_Ach, ilrRQ01Ach),
                EsfRQ01AuthClaims = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_RQ01, esfRQ01),
            };
        }

        public NonRegulatedLearning BuildNonRegulatedLearning(int year, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues, IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues)
        {
            var headers = FundingSummaryReportConstants.GroupHeaderDictionary[year];
            var ilrNR01Start = ilrValues[DeliverableCodeConstants.DeliverableCode_NR01].Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.IlrStartEarningsAttribute));
            var ilrNR01Ach = ilrValues[DeliverableCodeConstants.DeliverableCode_NR01].Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.IlrAchievementEarningsAttribute));
            var esfNR01 = esfValues[DeliverableCodeConstants.DeliverableCode_NR01].Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.EsfReferenceTypeAuthorisedClaims));

            return new NonRegulatedLearning
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_NonRegulatedLearning, headers),
                IlrNR01StartFunding = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_NR01_Start, ilrNR01Start),
                IlrNR01AchFunding = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_NR01_Ach, ilrNR01Ach),
                EsfNR01AuthClaims = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_NR01, esfNR01),
            };
        }

        public Progression BuildProgressions(int year, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues, IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues)
        {
            var headers = FundingSummaryReportConstants.GroupHeaderDictionary[year];
            var ilrPG01 = esfValues[DeliverableCodeConstants.DeliverableCode_PG01].Where(x => _ilrAttributeSet.Contains(x.AttributeName));
            var esfPG01 = esfValues[DeliverableCodeConstants.DeliverableCode_PG01];
            var ilrPG03 = esfValues[DeliverableCodeConstants.DeliverableCode_PG03].Where(x => _ilrAttributeSet.Contains(x.AttributeName));
            var esfPG03 = esfValues[DeliverableCodeConstants.DeliverableCode_PG03];
            var ilrPG04 = esfValues[DeliverableCodeConstants.DeliverableCode_PG04].Where(x => _ilrAttributeSet.Contains(x.AttributeName));
            var esfPG04 = esfValues[DeliverableCodeConstants.DeliverableCode_PG04];
            var ilrPG05 = esfValues[DeliverableCodeConstants.DeliverableCode_PG05].Where(x => _ilrAttributeSet.Contains(x.AttributeName));
            var esfPG05 = esfValues[DeliverableCodeConstants.DeliverableCode_PG05];

            return new Progression
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_Progression, headers),
                IlrPG01 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_PG01, ilrPG01),
                EsfPG01 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_PG01, esfPG01),
                IlrPG03 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_PG03, ilrPG03),
                EsfPG03 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_PG03, esfPG03),
                IlrPG04 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_PG04, ilrPG04),
                EsfPG04 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_PG04, esfPG04),
                IlrPG05 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_PG05, ilrPG05),
                EsfPG05 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_PG05, esfPG05),
            };
        }

        public CommunityGrant BuildCommunityGrants(int year, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues)
        {
            var headers = FundingSummaryReportConstants.GroupHeaderDictionary[year];
            var cg01 = esfValues[DeliverableCodeConstants.DeliverableCode_CG01];
            var cg02 = esfValues[DeliverableCodeConstants.DeliverableCode_CG02];

            return new CommunityGrant
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_CommunityGrant, headers),
                EsfCG01 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_CG01, cg01),
                EsfCG02 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_CG02, cg02),
            };
        }

        public SpecificationDefined BuildSpecificationDefined(int year, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues)
        {
            var headers = FundingSummaryReportConstants.GroupHeaderDictionary[year];
            var sd01 = esfValues[DeliverableCodeConstants.DeliverableCode_SD01];
            var sd02 = esfValues[DeliverableCodeConstants.DeliverableCode_SD02];

            return new SpecificationDefined
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_SpecificationDefined, headers),
                EsfSD01 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_SD01, sd01),
                EsfSD02 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_SD02, sd02),
            };
        }

        public PeriodisedReportValue BuildPeriodisedReportValue(string title, IEnumerable<PeriodisedValue> periodisedValues)
        {
            return new PeriodisedReportValue(
                title,
                periodisedValues?.Sum(x => x.April),
                periodisedValues?.Sum(x => x.May),
                periodisedValues?.Sum(x => x.June),
                periodisedValues?.Sum(x => x.July),
                periodisedValues?.Sum(x => x.August),
                periodisedValues?.Sum(x => x.September),
                periodisedValues?.Sum(x => x.October),
                periodisedValues?.Sum(x => x.Novemeber),
                periodisedValues?.Sum(x => x.December),
                periodisedValues?.Sum(x => x.January),
                periodisedValues?.Sum(x => x.February),
                periodisedValues?.Sum(x => x.March));
        }

        private GroupHeader BuildFundingHeader(string headerTitle, string[] headers)
        {
            return new GroupHeader(
                headerTitle,
                headers[0],
                headers[1],
                headers[2],
                headers[3],
                headers[4],
                headers[5],
                headers[6],
                headers[7],
                headers[8],
                headers[9],
                headers[10],
                headers[11]);
        }

        private IDictionary<string, Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>> PeriodiseIlr(IEnumerable<FM70PeriodisedValues> fm70Data)
        {
            var conRefDictionary = new Dictionary<string, Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>>();

            var t = fm70Data
                .GroupBy(x => x.ConRefNumber)
                .ToDictionary(
                cr => cr.Key,
                crv => crv.Select(x => x)
                .GroupBy(x => x.FundingYear)
                .ToDictionary(
                    fy => fy.Key,
                    fyv => fyv.Select(x => x)
                    .GroupBy(x => x.DeliverableCode)
                    .ToDictionary(
                        dc => dc.Key,
                        dcv => MapIlrPeriodisedValues(crv.Key, dcv.Key, dcv.ToList()),
                        StringComparer.OrdinalIgnoreCase)));

            return conRefDictionary;
        }

        private IEnumerable<PeriodisedValue> MapIlrPeriodisedValues(string conRef, string deliverableCode, IEnumerable<FM70PeriodisedValues> fm70Data)
        {
            return fm70Data.Select(x => new PeriodisedValue(
                conRef,
                deliverableCode,
                x.AttributeName,
                x.Period4 ?? 0,
                x.Period5 ?? 0,
                x.Period6 ?? 0,
                x.Period7 ?? 0,
                x.Period8 ?? 0,
                x.Period9 ?? 0,
                x.Period10 ?? 0,
                x.Period11 ?? 0,
                x.Period12 ?? 0,
                x.Period1 ?? 0,
                x.Period2 ?? 0,
                x.Period3 ?? 0));
        }

        private IDictionary<string, Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>> PeriodiseEsfSuppData(IDictionary<string, IEnumerable<SupplementaryDataYearlyModel>> supplementaryData)
        {
            var conRefDictionary = new Dictionary<string, Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>>();

            foreach (var conRef in supplementaryData)
            {
                conRefDictionary.Add(
                    conRef.Key,
                    conRef.Value
                    .ToDictionary(
                    key => key.FundingYear,
                    value => value.SupplementaryData.ToList()
                    .GroupBy(dc => dc.DeliverableCode)
                    .ToDictionary(
                        d => d.Key,
                        p => MapEsfPeriodisedValues(conRef.Key, p.Key, p.ToList()),
                        StringComparer.OrdinalIgnoreCase)));
            }

            return conRefDictionary;
        }

        private IEnumerable<PeriodisedValue> MapEsfPeriodisedValues(string conRef, string deliverableCode, IEnumerable<SupplementaryDataModel> supplementaryData)
        {
            return supplementaryData.Select(x => new PeriodisedValue(
                conRef,
                deliverableCode,
                x.ReferenceType,
                x.CalendarMonth == 4 ? x.Value : 0,
                x.CalendarMonth == 5 ? x.Value : 0,
                x.CalendarMonth == 6 ? x.Value : 0,
                x.CalendarMonth == 7 ? x.Value : 0,
                x.CalendarMonth == 8 ? x.Value : 0,
                x.CalendarMonth == 9 ? x.Value : 0,
                x.CalendarMonth == 10 ? x.Value : 0,
                x.CalendarMonth == 11 ? x.Value : 0,
                x.CalendarMonth == 12 ? x.Value : 0,
                x.CalendarMonth == 1 ? x.Value : 0,
                x.CalendarMonth == 2 ? x.Value : 0,
                x.CalendarMonth == 3 ? x.Value : 0));
        }

        private FundingSummaryHeaderModel PopulateReportHeader(
            SourceFileModel sourceFile,
            IEnumerable<ILRFileDetails> fileData,
            int ukPrn,
            string providerName,
            string conRefNumber,
            CancellationToken cancellationToken)
        {
            var ukPrnRow =
                new List<string> { ukPrn.ToString(), null, null };
            var contractReferenceNumberRow =
                new List<string> { conRefNumber, null, null, "ILR File :" };
            var supplementaryDataFileRow =
                new List<string> { sourceFile?.FileName?.Contains("/") ?? false ? sourceFile.FileName.Substring(sourceFile.FileName.IndexOf("/", StringComparison.Ordinal) + 1) : sourceFile?.FileName, null, null, "Last ILR File Update :" };
            var lastSupplementaryDataFileUpdateRow =
                new List<string> { sourceFile?.SuppliedDate?.ToString("dd/MM/yyyy hh:mm:ss"), null, null, "File Preparation Date :" };
            var securityClassificationRow =
                new List<string> { "OFFICIAL-SENSITIVE", null, null, null };

            foreach (var model in fileData)
            {
                var preparationDate = GetPreparedDateFromILRFileName(model.FileName);
                var secondYear = GetSecondYearFromReportYear(model.Year);

                ukPrnRow.Add(null);
                ukPrnRow.Add($"{model.Year}/{secondYear}");
                contractReferenceNumberRow.Add(model.FileName?.Substring(model.FileName.Contains("/") ? model.FileName.IndexOf("/", StringComparison.Ordinal) + 1 : 0));
                contractReferenceNumberRow.Add(null);
                supplementaryDataFileRow.Add(preparationDate);
                supplementaryDataFileRow.Add(null);
                lastSupplementaryDataFileUpdateRow.Add(model.FilePreparationDate?.ToString("dd/MM/yyyy hh:mm:ss"));
                lastSupplementaryDataFileUpdateRow.Add(null);

                if (model.Equals(fileData.Last()))
                {
                    continue;
                }

                securityClassificationRow.Add("(most recent closed collection for year)");
                securityClassificationRow.Add(null);
            }

            var header = new FundingSummaryHeaderModel
            {
                ProviderName = providerName,
                Ukprn = ukPrnRow.ToArray(),
                ContractReferenceNumber = contractReferenceNumberRow.ToArray(),
                SupplementaryDataFile = supplementaryDataFileRow.ToArray(),
                LastSupplementaryDataFileUpdate = lastSupplementaryDataFileUpdateRow.ToArray(),
                SecurityClassification = securityClassificationRow.ToArray()
            };

            return header;
        }

        private FundingSummaryFooterModel PopulateReportFooter(IReferenceDataVersions referenceDataVersions, string reportGeneration, CancellationToken cancellationToken)
        {
            return new FundingSummaryFooterModel
            {
                ReportGeneratedAt = reportGeneration,
                LarsData = referenceDataVersions.LarsVersion,
                OrganisationData = referenceDataVersions.OrganisationVersion,
                PostcodeData = referenceDataVersions.PostcodeVersion,
                ApplicationVersion = _versionInfo.ServiceReleaseVersion
            };
        }

        private string GetSecondYearFromReportYear(int year)
        {
            return year.ToString().Length > 3 ?
                (Convert.ToInt32(year.ToString().Substring(year.ToString().Length - 2)) + 1).ToString() :
                string.Empty;
        }

        private string GetPreparedDateFromILRFileName(string fileName)
        {
            if (fileName == null)
            {
                return null;
            }

            var fileNameParts = fileName.Split('-');
            if (fileNameParts.Length < 6 || fileNameParts[3].Length != 8 || fileNameParts[4].Length != 6)
            {
                return string.Empty;
            }

            var dateString = $"{fileNameParts[3].Substring(0, 4)}/{fileNameParts[3].Substring(4, 2)}/{fileNameParts[3].Substring(6, 2)} " +
                             $"{fileNameParts[4].Substring(0, 2)}:{fileNameParts[4].Substring(2, 2)}:{fileNameParts[4].Substring(4, 2)}";
            return Convert.ToDateTime(dateString).ToString("dd/MM/yyyy hh:mm:ss");
        }
    }
}
