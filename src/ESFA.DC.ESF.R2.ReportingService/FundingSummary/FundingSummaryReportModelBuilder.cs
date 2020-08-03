using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Constants;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ESF.R2.ReportingService.Constants;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Constants;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Interface;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model;
using ESFA.DC.ESF.R2.Service.Config.Interfaces;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ILR.DataService.Models;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary
{
    public class FundingSummaryReportModelBuilder : IFundingSummaryReportModelBuilder
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
        private readonly IFundingSummaryYearConfiguration _yearConfiguration;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IVersionInfo _versionInfo;
        private readonly ILogger _logger;

        public FundingSummaryReportModelBuilder(
            IDateTimeProvider dateTimeProvider,
            IFundingSummaryReportDataProvider dataProvider,
            IFundingSummaryYearConfiguration yearConfiguration,
            IVersionInfo versionInfo,
            ILogger logger)
        {
            _dateTimeProvider = dateTimeProvider;
            _versionInfo = versionInfo;
            _logger = logger;
            _dataProvider = dataProvider;
            _yearConfiguration = yearConfiguration;
        }

        public async Task<IEnumerable<FundingSummaryReportTab>> Build(IEsfJobContext esfJobContext, CancellationToken cancellationToken)
        {
            var baseIlrYear = _yearConfiguration.BaseIlrYear;

            var ukPrn = esfJobContext.UkPrn;
            IEnumerable<string> conRefNumbers;
            var dateTimeNowUtc = _dateTimeProvider.GetNowUtc();
            var dateTimeNowUk = _dateTimeProvider.ConvertUtcToUk(dateTimeNowUtc);
            var reportGenerationTime = dateTimeNowUk.ToString(ReportingConstants.TimeFormat) + " on " + dateTimeNowUk.ToString(ReportingConstants.ShortDateFormat);

            var referenceDataVersions = await _dataProvider.ProvideReferenceDataVersionsAsync(cancellationToken);
            var orgData = await _dataProvider.ProvideOrganisationReferenceDataAsync(ukPrn, cancellationToken);

            if (!orgData.ConRefNumbers.Any())
            {
                conRefNumbers = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { NotApplicable };
            }
            else
            {
                conRefNumbers = new HashSet<string>(orgData.ConRefNumbers, StringComparer.OrdinalIgnoreCase);
            }

            var currentCollectionYearString = string.Concat("20", esfJobContext.StartCollectionYearAbbreviation);
            var currentCollectionYear = int.Parse(currentCollectionYearString);

            var reportGroupHeaderDictionary = _yearConfiguration.PeriodisedValuesHeaderDictionary(currentCollectionYear);

            var esfSourceFiles = await _dataProvider.GetImportFilesAsync(esfJobContext.UkPrn, cancellationToken);

            _logger.LogDebug($"{esfSourceFiles.Count} esf files found for ukprn {ukPrn} and collection year {currentCollectionYearString}.");

            var supplementaryData = await _dataProvider.GetSupplementaryDataAsync(currentCollectionYear, esfSourceFiles, cancellationToken);

            var ilrYearlyFileData = await _dataProvider.GetIlrFileDetailsAsync(ukPrn, reportGroupHeaderDictionary.Keys, cancellationToken);
            var fm70YearlyData = await _dataProvider.GetYearlyIlrDataAsync(ukPrn, currentCollectionYear, esfJobContext.ReturnPeriod, _yearConfiguration.YearToCollectionDictionary(), cancellationToken);

            var periodisedEsf = PeriodiseEsfSuppData(conRefNumbers, supplementaryData);
            var periodisedILR = PeriodiseIlr(conRefNumbers, fm70YearlyData.SelectMany(x => x.Fm70PeriodisedValues));

            var fundingSummaryTabs = new List<FundingSummaryReportTab>();

            var footer = PopulateReportFooter(referenceDataVersions, reportGenerationTime);

            foreach (var conRefNumber in conRefNumbers)
            {
                var baseModels = BuildBaseModels(currentCollectionYear, baseIlrYear);

                var file = esfSourceFiles.FirstOrDefault(sf => sf.ConRefNumber.CaseInsensitiveEquals(conRefNumber));

                var header = PopulateReportHeader(file, ilrYearlyFileData, ukPrn, orgData.Name, conRefNumber, currentCollectionYear, baseIlrYear);

                var fundingSummaryModels = PopulateReportData(reportGroupHeaderDictionary, baseModels, periodisedEsf.GetValueOrDefault(conRefNumber), periodisedILR.GetValueOrDefault(conRefNumber));

                fundingSummaryTabs.Add(new FundingSummaryReportTab
                {
                    TabName = conRefNumber,
                    Header = header,
                    Footer = footer,
                    Body = fundingSummaryModels
                });
            }

            return fundingSummaryTabs;
        }

        public ICollection<FundingSummaryModel> PopulateReportData(
            IDictionary<int, string[]> reportGroupHeaderDictionary,
            ICollection<FundingSummaryModel> models,
            IDictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>> periodisedEsf,
            IDictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>> periodisedILR)
        {
            foreach (var model in models)
            {
                var header = reportGroupHeaderDictionary.GetValueOrDefault(model.Year);

                model.LearnerAssessmentPlans = BuildLearnerAssessmentPlans(header, periodisedEsf.GetValueOrDefault(model.Year), periodisedILR.GetValueOrDefault(model.Year));
                model.RegulatedLearnings = BuildRegulatedLearning(header, periodisedEsf.GetValueOrDefault(model.Year), periodisedILR.GetValueOrDefault(model.Year));
                model.NonRegulatedLearnings = BuildNonRegulatedLearning(header, periodisedEsf.GetValueOrDefault(model.Year), periodisedILR.GetValueOrDefault(model.Year));
                model.Progressions = BuildProgressions(header, periodisedEsf.GetValueOrDefault(model.Year), periodisedILR.GetValueOrDefault(model.Year));
                model.CommunityGrants = BuildCommunityGrants(header, periodisedEsf.GetValueOrDefault(model.Year));
                model.SpecificationDefineds = BuildSpecificationDefined(header, periodisedEsf.GetValueOrDefault(model.Year));
            }

            return models;
        }

        public LearnerAssessmentPlan BuildLearnerAssessmentPlans(string[] headers, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues, IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues)
        {
            var ilrST01 = ilrValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_ST01)?.Where(x => _ilrAttributeSet.Contains(x.AttributeName));
            var esfST01 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_ST01);

            return new LearnerAssessmentPlan
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_LearnerAssessment, headers),
                IlrST01 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_ST01, ilrST01),
                EsfST01 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_ST01, esfST01),
            };
        }

        public RegulatedLearning BuildRegulatedLearning(string[] headers, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues, IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues)
        {
            var ilrRQ01Start = ilrValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_RQ01)?.Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.IlrStartEarningsAttribute));
            var ilrRQ01Ach = ilrValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_RQ01)?.Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.IlrAchievementEarningsAttribute));
            var esfRQ01 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_RQ01)?.Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.EsfReferenceTypeAuthorisedClaims));

            return new RegulatedLearning
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_RegulatedLearning, headers),
                IlrRQ01StartFunding = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_RQ01_Start, ilrRQ01Start),
                IlrRQ01AchFunding = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_RQ01_Ach, ilrRQ01Ach),
                EsfRQ01AuthClaims = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_RQ01, esfRQ01),
            };
        }

        public NonRegulatedLearning BuildNonRegulatedLearning(string[] headers, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues, IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues)
        {
            var ilrNR01Start = ilrValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_NR01)?.Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.IlrStartEarningsAttribute));
            var ilrNR01Ach = ilrValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_NR01)?.Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.IlrAchievementEarningsAttribute));
            var esfNR01 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_NR01)?.Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.EsfReferenceTypeAuthorisedClaims));

            return new NonRegulatedLearning
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_NonRegulatedLearning, headers),
                IlrNR01StartFunding = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_NR01_Start, ilrNR01Start),
                IlrNR01AchFunding = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_NR01_Ach, ilrNR01Ach),
                EsfNR01AuthClaims = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_NR01, esfNR01),
            };
        }

        public Progression BuildProgressions(string[] headers, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues, IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues)
        {
            var ilrPG01 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG01)?.Where(x => _ilrAttributeSet.Contains(x.AttributeName));
            var esfPG01 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG01);
            var ilrPG03 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG03)?.Where(x => _ilrAttributeSet.Contains(x.AttributeName));
            var esfPG03 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG03);
            var ilrPG04 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG04)?.Where(x => _ilrAttributeSet.Contains(x.AttributeName));
            var esfPG04 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG04);
            var ilrPG05 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG05)?.Where(x => _ilrAttributeSet.Contains(x.AttributeName));
            var esfPG05 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG05);

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

        public CommunityGrant BuildCommunityGrants(string[] headers, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues)
        {
            var cg01 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_CG01);
            var cg02 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_CG02);

            return new CommunityGrant
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_CommunityGrant, headers),
                EsfCG01 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_CG01, cg01),
                EsfCG02 = BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_CG02, cg02),
            };
        }

        public SpecificationDefined BuildSpecificationDefined(string[] headers, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues)
        {
            var sd01 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_SD01);
            var sd02 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_SD02);

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
                    periodisedValues?.Sum(x => x.August) ?? 0m,
                    periodisedValues?.Sum(x => x.September) ?? 0m,
                    periodisedValues?.Sum(x => x.October) ?? 0m,
                    periodisedValues?.Sum(x => x.Novemeber) ?? 0m,
                    periodisedValues?.Sum(x => x.December) ?? 0m,
                    periodisedValues?.Sum(x => x.January) ?? 0m,
                    periodisedValues?.Sum(x => x.February) ?? 0m,
                    periodisedValues?.Sum(x => x.March) ?? 0m,
                    periodisedValues?.Sum(x => x.April) ?? 0m,
                    periodisedValues?.Sum(x => x.May) ?? 0m,
                    periodisedValues?.Sum(x => x.June) ?? 0m,
                    periodisedValues?.Sum(x => x.July) ?? 0m);
        }

        public FundingSummaryReportHeaderModel PopulateReportHeader(
          SourceFileModel sourceFile,
          IEnumerable<ILRFileDetails> ilrFileData,
          int ukPrn,
          string providerName,
          string conRefNumber,
          int collectionYear,
          int baseIlrYear)
        {
            var ilrFileDetailModels = BuildBaseIlrFileDetailModels(collectionYear, baseIlrYear);

            foreach (var model in ilrFileDetailModels)
            {
                var ilrData = ilrFileData?.Where(x => x?.Year == model.Year).FirstOrDefault();

                if (ilrData != null)
                {
                    model.IlrFile = !string.IsNullOrWhiteSpace(ilrData?.FileName) ? Path.GetFileName(ilrData?.FileName) : null;
                    model.FilePrepDate = ilrData?.FilePreparationDate?.ToString(ReportingConstants.ShortDateFormat);
                    model.LastIlrFileUpdate = ilrData?.LastSubmission?.ToString(ReportingConstants.LongDateFormat);
                }
            }

            var header = new FundingSummaryReportHeaderModel
            {
                Ukprn = ukPrn.ToString(),
                ProviderName = providerName,
                ContractReferenceNumber = conRefNumber,
                SecurityClassification = ReportingConstants.Classification,
                SupplementaryDataFile = !string.IsNullOrWhiteSpace(sourceFile?.FileName) ? Path.GetFileName(sourceFile?.FileName) : null,
                LastSupplementaryDataFileUpdate = sourceFile?.SuppliedDate?.ToString(ReportingConstants.LongDateFormat),
                IlrFileDetails = ilrFileDetailModels
            };

            return header;
        }

        public FundingSummaryReportFooterModel PopulateReportFooter(IReferenceDataVersions referenceDataVersions, string reportGeneration)
        {
            return new FundingSummaryReportFooterModel
            {
                ReportGeneratedAt = reportGeneration,
                LarsData = referenceDataVersions.LarsVersion,
                OrganisationData = referenceDataVersions.OrganisationVersion,
                PostcodeData = referenceDataVersions.PostcodeVersion,
                ApplicationVersion = _versionInfo.ServiceReleaseVersion
            };
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

        private IDictionary<string, Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>> PeriodiseIlr(IEnumerable<string> conRefNumbers, IEnumerable<FM70PeriodisedValues> fm70Data)
        {
            return fm70Data?
                .GroupBy(x => conRefNumbers.Contains(x.ConRefNumber) ? x.ConRefNumber : NotApplicable)
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
        }

        private IEnumerable<PeriodisedValue> MapIlrPeriodisedValues(string conRef, string deliverableCode, IEnumerable<FM70PeriodisedValues> fm70Data)
        {
            return fm70Data.Select(x => new PeriodisedValue(
                conRef,
                deliverableCode,
                x.AttributeName,
                x.Period1 ?? 0,
                x.Period2 ?? 0,
                x.Period3 ?? 0,
                x.Period4 ?? 0,
                x.Period5 ?? 0,
                x.Period6 ?? 0,
                x.Period7 ?? 0,
                x.Period8 ?? 0,
                x.Period9 ?? 0,
                x.Period10 ?? 0,
                x.Period11 ?? 0,
                x.Period12 ?? 0));
        }

        private IDictionary<string, Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>> PeriodiseEsfSuppData(IEnumerable<string> conRefNumbers, IDictionary<string, IEnumerable<SupplementaryDataYearlyModel>> supplementaryData)
        {
            var conRefDictionary = new Dictionary<string, Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>>();

            foreach (var conRef in supplementaryData)
            {
                if (conRefNumbers.Contains(conRef.Key))
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
                else
                {
                    conRefDictionary.Add(
                        NotApplicable,
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
            }

            return conRefDictionary;
        }

        private IEnumerable<PeriodisedValue> MapEsfPeriodisedValues(string conRef, string deliverableCode, IEnumerable<SupplementaryDataModel> supplementaryData)
        {
            return supplementaryData.Select(x => new PeriodisedValue(
                conRef,
                deliverableCode,
                x.ReferenceType,
                x.CalendarMonth == 8 ? x.Value : 0,
                x.CalendarMonth == 9 ? x.Value : 0,
                x.CalendarMonth == 10 ? x.Value : 0,
                x.CalendarMonth == 11 ? x.Value : 0,
                x.CalendarMonth == 12 ? x.Value : 0,
                x.CalendarMonth == 1 ? x.Value : 0,
                x.CalendarMonth == 2 ? x.Value : 0,
                x.CalendarMonth == 3 ? x.Value : 0,
                x.CalendarMonth == 4 ? x.Value : 0,
                x.CalendarMonth == 5 ? x.Value : 0,
                x.CalendarMonth == 6 ? x.Value : 0,
                x.CalendarMonth == 7 ? x.Value : 0));
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

        private List<FundingSummaryModel> BuildBaseModels(int collectionYear, int baseIlrYear)
        {
            var year = baseIlrYear;

            var models = new List<FundingSummaryModel>();

            while (year <= collectionYear)
            {
                models.Add(new FundingSummaryModel { Year = year });
                year++;
            }

            return models;
        }

        private List<IlrFileDetail> BuildBaseIlrFileDetailModels(int collectionYear, int baseIlrYear)
        {
            var year = baseIlrYear;
            var academicYearDictionary = _yearConfiguration.YearToAcademicYearDictionary();

            var models = new List<IlrFileDetail>();

            while (year <= collectionYear)
            {
                models.Add(
                    new IlrFileDetail
                    {
                        Year = year,
                        AcademicYear = academicYearDictionary.GetValueOrDefault(year),
                        MostRecent = year != collectionYear ? ReportingConstants.IlrCollectionClosedMessage : null
                    });

                year++;
            }

            return models;
        }
    }
}
