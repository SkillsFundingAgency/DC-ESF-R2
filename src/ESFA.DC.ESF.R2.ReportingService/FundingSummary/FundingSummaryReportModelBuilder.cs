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
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;
using ESFA.DC.ESF.R2.Service.Config.Interfaces;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ILR.DataService.Models;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary
{
    public class FundingSummaryReportModelBuilder : IFundingSummaryReportModelBuilder
    {
        private const string NotApplicable = "Not Applicable";
        private const string ESF2021CollectionName = "ESFR2-2021";

        private readonly int[] _crossOverReturnPeriods = new[] { 1, 2 };

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

        public async Task<ICollection<IFundingSummaryReportTab>> Build(IEsfJobContext esfJobContext, CancellationToken cancellationToken)
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

            var currentCollectionYearString = CalculateCollectionYear(esfJobContext.CurrentPeriod, esfJobContext.StartCollectionYearAbbreviation, esfJobContext.CollectionName);
            var currentCollectionYear = int.Parse(currentCollectionYearString);

            var dataRetrievalCollectionYearString = string.Concat("20", esfJobContext.StartCollectionYearAbbreviation);
            var dataRetrievalCollectionYear = int.Parse(dataRetrievalCollectionYearString);

            var reportGroupHeaderDictionary = _yearConfiguration.PeriodisedValuesHeaderDictionary(currentCollectionYear);

            var esfSourceFiles = await _dataProvider.GetImportFilesAsync(esfJobContext.UkPrn, cancellationToken);

            _logger.LogDebug($"{esfSourceFiles.Count} esf files found for ukprn {ukPrn} and collection year {currentCollectionYearString}.");

            var supplementaryData = await _dataProvider.GetSupplementaryDataAsync(currentCollectionYear, esfSourceFiles, cancellationToken);

            var ilrYearlyFileData = await _dataProvider.GetIlrFileDetailsAsync(ukPrn, reportGroupHeaderDictionary.Keys, cancellationToken);
            var fm70YearlyData = await _dataProvider.GetYearlyIlrDataAsync(ukPrn, dataRetrievalCollectionYear, esfJobContext.ReturnPeriod, _yearConfiguration.YearToCollectionDictionary(), cancellationToken);

            var periodisedEsf = PeriodiseEsfSuppData(conRefNumbers, supplementaryData);
            var periodisedILR = PeriodiseIlr(conRefNumbers, fm70YearlyData.SelectMany(x => x.Fm70PeriodisedValues));

            var fundingSummaryTabs = new List<IFundingSummaryReportTab>();

            var footer = PopulateReportFooter(referenceDataVersions, reportGenerationTime);

            var academicYearDictionary = _yearConfiguration.YearToAcademicYearDictionary();

            foreach (var conRefNumber in conRefNumbers)
            {
                var models = BuildBodyTemplateForYears(currentCollectionYear, baseIlrYear, academicYearDictionary);

                var file = esfSourceFiles.FirstOrDefault(sf => sf.ConRefNumber.CaseInsensitiveEquals(conRefNumber));

                var header = PopulateReportHeader(file, ilrYearlyFileData, ukPrn, orgData.Name, conRefNumber, currentCollectionYear, baseIlrYear, academicYearDictionary);

                var fundingSummaryModels = PopulateReportData(conRefNumber, reportGroupHeaderDictionary, models, periodisedEsf.GetValueOrDefault(conRefNumber), periodisedILR.GetValueOrDefault(conRefNumber));

                fundingSummaryTabs.Add(new FundingSummaryReportTab
                {
                    Title = FundingSummaryReportConstants.BodyTitle,
                    TabName = conRefNumber,
                    Header = header,
                    Footer = footer,
                    Body = fundingSummaryModels
                });
            }

            return fundingSummaryTabs;
        }

        public ICollection<FundingSummaryReportEarnings> PopulateReportData(
            string conRefNumber,
            IDictionary<int, string[]> reportGroupHeaderDictionary,
            ICollection<FundingSummaryReportEarnings> models,
            IDictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>> periodisedEsf,
            IDictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>> periodisedILR)
        {
            foreach (var model in models)
            {
                var header = reportGroupHeaderDictionary.GetValueOrDefault(model.Year);

                model.ConRefNumber = conRefNumber;

                model.DeliverableCategories = new List<IDeliverableCategory>
                {
                    BuildLearnerAssessmentPlans(header, periodisedEsf.GetValueOrDefault(model.Year), periodisedILR.GetValueOrDefault(model.Year)),
                    BuildRegulatedLearning(header, periodisedEsf.GetValueOrDefault(model.Year), periodisedILR.GetValueOrDefault(model.Year)),
                    BuildNonRegulatedActivity(header, periodisedEsf.GetValueOrDefault(model.Year), periodisedILR.GetValueOrDefault(model.Year)),
                    BuildProgressions(header, periodisedEsf.GetValueOrDefault(model.Year), periodisedILR.GetValueOrDefault(model.Year)),
                    BuildCommunityGrants(header, periodisedEsf.GetValueOrDefault(model.Year)),
                    BuildSpecificationDefined(header, periodisedEsf.GetValueOrDefault(model.Year))
                };
            }

            var modelsArray = models.OrderBy(x => x.Year).ToArray();
            var modelLength = modelsArray.Length;

            modelsArray[0].MonthlyTotals = BuildMonthlyTotalsForArrayIndex(0, conRefNumber, modelsArray);
            modelsArray[0].YearTotal = modelsArray[0].MonthlyTotals.Total;
            modelsArray[0].CumulativeYearTotal = modelsArray[0].MonthlyTotals.Total;
            modelsArray[0].CumulativeMonthlyTotals = BuildCumulativeMonthlyTotalsForArrayIndex(0, conRefNumber, modelsArray);

            for (int i = 1; i < modelLength; i++)
            {
                modelsArray[i].MonthlyTotals = BuildMonthlyTotalsForArrayIndex(i, conRefNumber, modelsArray);
                modelsArray[i].YearTotal = modelsArray[i].MonthlyTotals.Total;
                modelsArray[i].CumulativeYearTotal = modelsArray[i].MonthlyTotals.Total;

                var previousCumuativeYearTotal = modelsArray[i - 1].CumulativeYearTotal;

                modelsArray[i].CumulativeYearTotal = modelsArray[i].YearTotal + previousCumuativeYearTotal;
                modelsArray[i].PreviousYearCumulativeTotal = previousCumuativeYearTotal;

                modelsArray[i].CumulativeMonthlyTotals = BuildCumulativeMonthlyTotalsForArrayIndex(i, conRefNumber, modelsArray);
            }

            return modelsArray;
        }

        public IDeliverableCategory BuildLearnerAssessmentPlans(string[] headers, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues, IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues)
        {
            var ilrST01 = ilrValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_ST01)?.Where(x => _ilrAttributeSet.Contains(x.AttributeName));
            var esfST01 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_ST01);

            return new DeliverableCategory(FundingSummaryReportConstants.Total_LearnerAssessment)
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_LearnerAssessment, headers),
                DeliverableSubCategories = new List<IDeliverableSubCategory>
                {
                    new DeliverableSubCategory(FundingSummaryReportConstants.Default_SubCateogryTitle, false)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_ST01, ilrST01),
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_ST01, esfST01),
                        }
                    }
                }
            };
        }

        public IDeliverableCategory BuildRegulatedLearning(string[] headers, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues, IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues)
        {
            var ilrRQ01Start = ilrValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_RQ01)?.Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.IlrStartEarningsAttribute));
            var ilrRQ01Ach = ilrValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_RQ01)?.Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.IlrAchievementEarningsAttribute));
            var esfRQ01 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_RQ01)?.Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.EsfReferenceTypeAuthorisedClaims));

            return new DeliverableCategory(FundingSummaryReportConstants.Total_RegulatedLearning)
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_RegulatedLearning, headers),
                DeliverableSubCategories = new List<IDeliverableSubCategory>
                {
                    new DeliverableSubCategory(FundingSummaryReportConstants.SubCategoryHeader_IlrRegulatedLearning, true)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_RQ01_Start, ilrRQ01Start),
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_RQ01_Ach, ilrRQ01Ach),
                        }
                    },
                    new DeliverableSubCategory(FundingSummaryReportConstants.Default_SubCateogryTitle, false)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_RQ01, esfRQ01),
                        }
                    }
                }
            };
        }

        public IDeliverableCategory BuildNonRegulatedActivity(string[] headers, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues, IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues)
        {
            var ilrNR01Start = ilrValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_NR01)?.Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.IlrStartEarningsAttribute));
            var ilrNR01Ach = ilrValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_NR01)?.Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.IlrAchievementEarningsAttribute));
            var esfNR01 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_NR01)?.Where(x => x.AttributeName.CaseInsensitiveEquals(FundingSummaryReportConstants.EsfReferenceTypeAuthorisedClaims));

            return new DeliverableCategory(FundingSummaryReportConstants.Total_NonRegulatedActivity)
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_NonRegulatedActivity, headers),
                DeliverableSubCategories = new List<IDeliverableSubCategory>
                {
                    new DeliverableSubCategory(FundingSummaryReportConstants.SubCategoryHeader_IlrNonRegulatedActivity, true)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_NR01_Start, ilrNR01Start),
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_NR01_Ach, ilrNR01Ach),
                        }
                    },
                    new DeliverableSubCategory(FundingSummaryReportConstants.Default_SubCateogryTitle, false)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_NR01, esfNR01),
                        }
                    }
                }
            };
        }

        public IDeliverableCategory BuildProgressions(string[] headers, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues, IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues)
        {
            var ilrPG01 = ilrValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG01)?.Where(x => _ilrAttributeSet.Contains(x.AttributeName));
            var esfPG01 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG01);
            var ilrPG03 = ilrValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG03)?.Where(x => _ilrAttributeSet.Contains(x.AttributeName));
            var esfPG03 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG03);
            var ilrPG04 = ilrValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG04)?.Where(x => _ilrAttributeSet.Contains(x.AttributeName));
            var esfPG04 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG04);
            var ilrPG05 = ilrValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG05)?.Where(x => _ilrAttributeSet.Contains(x.AttributeName));
            var esfPG05 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_PG05);

            return new DeliverableCategory(FundingSummaryReportConstants.Total_Progression)
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_Progression, headers),
                DeliverableSubCategories = new List<IDeliverableSubCategory>
                {
                    new DeliverableSubCategory(FundingSummaryReportConstants.SubCategoryHeader_PG01, true)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_PG01, ilrPG01),
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_PG01, esfPG01),
                        }
                    },
                    new DeliverableSubCategory(FundingSummaryReportConstants.SubCategoryHeader_PG03, true)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_PG03, ilrPG03),
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_PG03, esfPG03),
                        }
                    },
                    new DeliverableSubCategory(FundingSummaryReportConstants.SubCategoryHeader_PG04, true)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_PG04, ilrPG04),
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_PG04, esfPG04),
                        }
                    },
                    new DeliverableSubCategory(FundingSummaryReportConstants.SubCategoryHeader_PG05, true)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ILR_PG05, ilrPG05),
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_PG05, esfPG05),
                        }
                    },
                }
            };
        }

        public IDeliverableCategory BuildCommunityGrants(string[] headers, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues)
        {
            var cg01 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_CG01);
            var cg02 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_CG02);

            return new DeliverableCategory(FundingSummaryReportConstants.Total_CommunityGrant)
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_CommunityGrant, headers),
                DeliverableSubCategories = new List<IDeliverableSubCategory>
                {
                    new DeliverableSubCategory(FundingSummaryReportConstants.Default_SubCateogryTitle, false)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_CG01, cg01),
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_CG02, cg02),
                        }
                    }
                }
            };
        }

        public IDeliverableCategory BuildSpecificationDefined(string[] headers, IDictionary<string, IEnumerable<PeriodisedValue>> esfValues)
        {
            var sd01 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_SD01);
            var sd02 = esfValues.GetValueOrDefault(DeliverableCodeConstants.DeliverableCode_SD02);

            return new DeliverableCategory(FundingSummaryReportConstants.Total_SpecificationDefined)
            {
                GroupHeader = BuildFundingHeader(FundingSummaryReportConstants.Header_SpecificationDefined, headers),
                DeliverableSubCategories = new List<IDeliverableSubCategory>
                {
                    new DeliverableSubCategory(FundingSummaryReportConstants.Default_SubCateogryTitle, false)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_SD01, sd01),
                            BuildPeriodisedReportValue(FundingSummaryReportConstants.Deliverable_ESF_SD02, sd02),
                        }
                    }
                }
            };
        }

        public PeriodisedReportValue BuildPeriodisedReportValue(string title, IEnumerable<PeriodisedValue> periodisedValues)
        {
            return new PeriodisedReportValue(title, BuildPeriodisedReportValueTTotals(periodisedValues ?? Enumerable.Empty<PeriodisedValue>()));
        }

        public FundingSummaryReportHeaderModel PopulateReportHeader(
          SourceFileModel sourceFile,
          IEnumerable<ILRFileDetails> ilrFileData,
          int ukPrn,
          string providerName,
          string conRefNumber,
          int collectionYear,
          int baseIlrYear,
          IDictionary<int, string> academicYearDictionary)
        {
            var ilrFileDetailModels = BuildIlrFileDetailModelsForYears(collectionYear, baseIlrYear, academicYearDictionary);

            var lastSupplementaryDataFileUpdateUk = sourceFile?.SuppliedDate.HasValue ?? false ? _dateTimeProvider.ConvertUtcToUk(sourceFile.SuppliedDate.Value) : (DateTime?)null;

            foreach (var model in ilrFileDetailModels)
            {
                var ilrData = ilrFileData?.Where(x => x?.Year == model.Year).FirstOrDefault();

                if (ilrData != null)
                {
                    var lastIlrFileUpdateUk = ilrData.LastSubmission.HasValue ? _dateTimeProvider.ConvertUtcToUk(ilrData.LastSubmission.Value) : (DateTime?)null;

                    model.IlrFile = !string.IsNullOrWhiteSpace(ilrData?.FileName) ? Path.GetFileName(ilrData?.FileName) : null;
                    model.FilePrepDate = ilrData?.FilePreparationDate?.ToString(ReportingConstants.ShortDateFormat);
                    model.LastIlrFileUpdate = lastIlrFileUpdateUk?.ToString(ReportingConstants.LongDateFormat);
                }
            }

            var header = new FundingSummaryReportHeaderModel
            {
                Ukprn = ukPrn.ToString(),
                ProviderName = providerName,
                ContractReferenceNumber = conRefNumber,
                SecurityClassification = ReportingConstants.Classification,
                SupplementaryDataFile = !string.IsNullOrWhiteSpace(sourceFile?.FileName) ? Path.GetFileName(sourceFile?.FileName) : null,
                LastSupplementaryDataFileUpdate = lastSupplementaryDataFileUpdateUk?.ToString(ReportingConstants.LongDateFormat),
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

        public IDictionary<string, Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>> PeriodiseIlr(IEnumerable<string> conRefNumbers, IEnumerable<FM70PeriodisedValues> fm70Data)
        {
            var contractsFromIlr = fm70Data.Select(x => x.ConRefNumber).Distinct().ToList();

            return conRefNumbers.Contains(NotApplicable) || !contractsFromIlr.Any(x => conRefNumbers.Contains(x)) ?
                new Dictionary<string, Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>>(StringComparer.OrdinalIgnoreCase)
                {
                    { NotApplicable, new Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>() }
                } :

                fm70Data?
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

        public IDictionary<string, Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>> PeriodiseEsfSuppData(IEnumerable<string> conRefNumbers, IDictionary<string, IEnumerable<SupplementaryDataYearlyModel>> supplementaryData)
        {
            if (conRefNumbers.Contains(NotApplicable))
            {
                return new Dictionary<string, Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>>(StringComparer.OrdinalIgnoreCase)
                {
                    { NotApplicable, new Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>() }
                };
            }

            return conRefNumbers
                .ToDictionary(
                conRef => conRef,
                conRef => supplementaryData.GetValueOrDefault(conRef)?
                .ToDictionary(
                    key => key.FundingYear,
                    value => value.SupplementaryData.ToList()
                    .GroupBy(dc => dc.DeliverableCode)
                    .ToDictionary(
                        d => d.Key,
                        p => MapEsfPeriodisedValues(conRef, p.Key, p.ToList()),
                        StringComparer.OrdinalIgnoreCase)) ?? new Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>());
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

        private IEnumerable<PeriodisedValue> MapIlrPeriodisedValues(string conRef, string deliverableCode, IEnumerable<FM70PeriodisedValues> fm70Data)
        {
            return fm70Data.Select(x => new PeriodisedValue(
                conRef,
                deliverableCode,
                x.AttributeName,
                new decimal[]
                {
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
                    x.Period12 ?? 0
                }));
        }

        private IEnumerable<PeriodisedValue> MapEsfPeriodisedValues(string conRef, string deliverableCode, IEnumerable<SupplementaryDataModel> supplementaryData)
        {
            return supplementaryData.Select(x => new PeriodisedValue(
                conRef,
                deliverableCode,
                x.CostType,
                new decimal[]
                {
                    x.CalendarMonth == 8 ? (x.CostType.CaseInsensitiveEquals(ESFConstants.UnitCostDeductionCostType) ? x.Value * -1 : x.Value) ?? 0m : 0m,
                    x.CalendarMonth == 9 ? (x.CostType.CaseInsensitiveEquals(ESFConstants.UnitCostDeductionCostType) ? x.Value * -1 : x.Value) ?? 0m : 0m,
                    x.CalendarMonth == 10 ? (x.CostType.CaseInsensitiveEquals(ESFConstants.UnitCostDeductionCostType) ? x.Value * -1 : x.Value) ?? 0m : 0m,
                    x.CalendarMonth == 11 ? (x.CostType.CaseInsensitiveEquals(ESFConstants.UnitCostDeductionCostType) ? x.Value * -1 : x.Value) ?? 0m : 0m,
                    x.CalendarMonth == 12 ? (x.CostType.CaseInsensitiveEquals(ESFConstants.UnitCostDeductionCostType) ? x.Value * -1 : x.Value) ?? 0m : 0m,
                    x.CalendarMonth == 1 ? (x.CostType.CaseInsensitiveEquals(ESFConstants.UnitCostDeductionCostType) ? x.Value * -1 : x.Value) ?? 0m : 0m,
                    x.CalendarMonth == 2 ? (x.CostType.CaseInsensitiveEquals(ESFConstants.UnitCostDeductionCostType) ? x.Value * -1 : x.Value) ?? 0m : 0m,
                    x.CalendarMonth == 3 ? (x.CostType.CaseInsensitiveEquals(ESFConstants.UnitCostDeductionCostType) ? x.Value * -1 : x.Value) ?? 0m : 0m,
                    x.CalendarMonth == 4 ? (x.CostType.CaseInsensitiveEquals(ESFConstants.UnitCostDeductionCostType) ? x.Value * -1 : x.Value) ?? 0m : 0m,
                    x.CalendarMonth == 5 ? (x.CostType.CaseInsensitiveEquals(ESFConstants.UnitCostDeductionCostType) ? x.Value * -1 : x.Value) ?? 0m : 0m,
                    x.CalendarMonth == 6 ? (x.CostType.CaseInsensitiveEquals(ESFConstants.UnitCostDeductionCostType) ? x.Value * -1 : x.Value) ?? 0m : 0m,
                    x.CalendarMonth == 7 ? (x.CostType.CaseInsensitiveEquals(ESFConstants.UnitCostDeductionCostType) ? x.Value * -1 : x.Value) ?? 0m : 0m
                }));
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

        private List<FundingSummaryReportEarnings> BuildBodyTemplateForYears(int collectionYear, int baseIlrYear, IDictionary<int, string> academicYearDictionary)
        {
            var year = baseIlrYear;

            var models = new List<FundingSummaryReportEarnings>();

            while (year <= collectionYear)
            {
                models.Add(new FundingSummaryReportEarnings
                {
                    Year = year,
                    AcademicYear = academicYearDictionary.GetValueOrDefault(year)
                });

                year++;
            }

            return models;
        }

        private List<IlrFileDetail> BuildIlrFileDetailModelsForYears(int collectionYear, int baseIlrYear, IDictionary<int, string> academicYearDictionary)
        {
            var year = baseIlrYear;

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

        private decimal Sum(params decimal?[] values)
        {
            return values.Where(x => x.HasValue).Sum(x => x.Value);
        }

        private PeriodisedReportValue BuildMonthlyTotalsForArrayIndex(int index, string conRefNumber, FundingSummaryReportEarnings[] models)
        {
            var totals = BuildPeriodisedReportValueTTotals(models[index].DeliverableCategories);

            if (index > 0)
            {
                return new PeriodisedReportValue(
                    string.Concat(conRefNumber, " Total (£)"),
                    totals);
            }
            else
            {
                for (int i = 0; i < 8; i++)
                {
                    totals[i] = 0m;
                }

                return new PeriodisedReportValue(
                    string.Concat(conRefNumber, " Total (£)"),
                    totals);
            }
        }

        private PeriodisedReportValue BuildCumulativeMonthlyTotalsForArrayIndex(int index, string conRefNumber, FundingSummaryReportEarnings[] models)
        {
            var monthlyTotals = models[index].MonthlyTotals.MonthlyValues;
            var previousYearCumulativeTotal = index > 0 ? models[index].PreviousYearCumulativeTotal ?? 0m : 0m;

            if (index > 0)
            {
                return new PeriodisedReportValue(
                    string.Concat(conRefNumber, " Cumulative (£)"),
                    BuildCumulativePeriodisedReportValueTTotals(monthlyTotals, previousYearCumulativeTotal, 12));
            }
            else
            {
                var totals = new List<decimal>
                {
                    0m,
                    0m,
                    0m,
                    0m,
                    0m,
                    0m,
                    0m,
                    0m,
                };

                totals.AddRange(BuildCumulativePeriodisedReportValueTTotals(monthlyTotals, previousYearCumulativeTotal, 4, 8));

                return new PeriodisedReportValue(
                  string.Concat(conRefNumber, " Cumulative (£)"),
                  totals.ToArray());
            }
        }

        private decimal[] BuildPeriodisedReportValueTTotals(IEnumerable<PeriodisedValue> input)
        {
            return Enumerable.Range(0, 12).Select(s => input.Sum(x => x.MonthlyValues[s])).ToArray();
        }

        private decimal[] BuildPeriodisedReportValueTTotals(IEnumerable<IDeliverableCategory> input)
        {
            return Enumerable.Range(0, 12).Select(s => input.Sum(x => x.Totals.MonthlyValues[s])).ToArray();
        }

        private decimal[] BuildCumulativePeriodisedReportValueTTotals(decimal[] input, decimal previousYearCumulativeTotal, int range, int startIndex = 0)
        {
            return Enumerable.Range(0, range).Select(s => SumCumulativeMonths(input, previousYearCumulativeTotal, s + 1, startIndex)).ToArray();
        }

        private decimal SumCumulativeMonths(decimal[] monthlyTotals, decimal previousYearCumulativeTotal, int range, int startIndex)
        {
            return Enumerable.Range(startIndex, range).Sum(s => monthlyTotals[s]) + previousYearCumulativeTotal;
        }

        private string CalculateCollectionYear(int returnPeriod, string startCollectionYearAbbreviation, string collectionName)
        {
            if (collectionName == ESF2021CollectionName && _crossOverReturnPeriods.Contains(returnPeriod))
            {
                if (int.TryParse(startCollectionYearAbbreviation, out var startYear))
                {
                    startYear--;

                    return string.Concat("20", startYear);
                }
            }

            return string.Concat("20", startCollectionYearAbbreviation);
        }
    }
}