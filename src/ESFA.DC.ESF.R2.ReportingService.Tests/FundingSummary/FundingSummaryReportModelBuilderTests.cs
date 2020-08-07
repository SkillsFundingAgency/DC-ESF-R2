using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.FundingSummary;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;
using ESFA.DC.ESF.R2.Service.Config.Interfaces;
using ESFA.DC.ILR.DataService.Models;
using ESFA.DC.Logging.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ReportingService.Tests.FundingSummary
{
    public class FundingSummaryReportModelBuilderTests
    {
        [Fact]
        public void BuildPeriodisedReportValue()
        {
            var title = "Title";
            IEnumerable<PeriodisedValue> periodisedValues = new List<PeriodisedValue>
            {
                new PeriodisedValue("ConRef1", "Code1", "StartEarnings", 1m, 1m, 1m, 0m, 0m, 1m, null, 1m, 1m, 1m, 1m, 1m),
                new PeriodisedValue("ConRef1", "Code1", "AchievementEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m)
            };

            var expectedValue = new PeriodisedReportValue("Title", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 1m, 2m, 1m, 2m);

            NewBuilder().BuildPeriodisedReportValue(title, periodisedValues).Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void BuildSpecificationDefined()
        {
            IDictionary<string, IEnumerable<PeriodisedValue>> esfValues = new Dictionary<string, IEnumerable<PeriodisedValue>>
            {
                {
                    "SD01", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "SD01", "StartEarnings", 1m, 1m, 1m, 0m, 0m, 1m, null, 1m, 1m, 1m, 1m, 1m),
                        new PeriodisedValue("ConRef1", "SD01", "AchievementEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m)
                    }
                },
                {
                    "SD02", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "SD02", "AchievementEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m)
                    }
                }
            };

            var expectedValue = new SpecificationDefined
            {
                GroupHeader = Year2020GroupHeader("Specification Defined"),
                EsfSD01 = new PeriodisedReportValue("SUPPDATA SD01 Progression Within Work (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                EsfSD02 = new PeriodisedReportValue("SUPPDATA SD02 LEP Agreed Delivery Plan (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
            };

            NewBuilder().BuildSpecificationDefined(HeaderStringArray(), esfValues).Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void BuildCommunityGrants()
        {
            IDictionary<string, IEnumerable<PeriodisedValue>> esfValues = new Dictionary<string, IEnumerable<PeriodisedValue>>
            {
                {
                    "CG01", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "CG01", "StartEarnings", 1m, 1m, 1m, 0m, 0m, 1m, null, 1m, 1m, 1m, 1m, 1m),
                        new PeriodisedValue("ConRef1", "CG01", "AchievementEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m)
                    }
                },
                {
                    "CG02", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "CG02", "AchievementEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m) }
                }
            };

            var expectedValue = new CommunityGrant
            {
                GroupHeader = Year2020GroupHeader("Community Grant"),
                ReportValues = new List<IPeriodisedReportValue>
                {
                    new PeriodisedReportValue("SUPPDATA CG01 Community Grant Payment (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                    new PeriodisedReportValue("SUPPDATA CG02 Community Grant Management Cost (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                }
            };

            NewBuilder().BuildCommunityGrants(HeaderStringArray(), esfValues).Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void BuildNonRegulatedLearning()
        {
            IDictionary<string, IEnumerable<PeriodisedValue>> esfValues = new Dictionary<string, IEnumerable<PeriodisedValue>>
            {
                {
                    "NR01", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "NR01", "Authorised Claims", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m)
                    }
                }
            };

            IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues = new Dictionary<string, IEnumerable<PeriodisedValue>>
            {
                {
                    "NR01", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "NR01", "StartEarnings", 1m, 1m, 1m, 0m, 0m, 1m, null, 1m, 1m, 1m, 1m, 1m),
                        new PeriodisedValue("ConRef1", "NR01", "AchievementEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m)
                    }
                }
            };

            var expectedValue = new NonRegulatedLearning
            {
                GroupHeader = Year2020GroupHeader("Non Regulated Learning"),
                IlrNR01StartFunding = new PeriodisedReportValue("ILR NR01 Non Regulated Learning - Start Funding (£)", 1m, 1m, 1m, 0m, 0m, 1m, 0m, 1m, 1m, 1m, 1m, 1m),
                IlrNR01AchFunding = new PeriodisedReportValue("ILR NR01 Non Regulated Learning - Achievement Funding (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m),
                EsfNR01AuthClaims = new PeriodisedReportValue("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
            };

            NewBuilder().BuildNonRegulatedLearning(HeaderStringArray(), esfValues, ilrValues).Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void BuildRegulatedLearning()
        {
            IDictionary<string, IEnumerable<PeriodisedValue>> esfValues = new Dictionary<string, IEnumerable<PeriodisedValue>>
            {
                {
                    "RQ01", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "RQ01", "Authorised Claims", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m)
                    }
                }
            };

            IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues = new Dictionary<string, IEnumerable<PeriodisedValue>>
            {
                {
                    "RQ01", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "RQ01", "StartEarnings", 1m, 1m, 1m, 0m, 0m, 1m, null, 1m, 1m, 1m, 1m, 1m),
                        new PeriodisedValue("ConRef1", "RQ01", "AchievementEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m)
                    }
                }
            };

            var expectedValue = new RegulatedLearning
            {
                GroupHeader = Year2020GroupHeader("Regulated Learning"),
                IlrRQ01StartFunding = new PeriodisedReportValue("ILR RQ01 Regulated Learning - Start Funding (£)", 1m, 1m, 1m, 0m, 0m, 1m, 0m, 1m, 1m, 1m, 1m, 1m),
                IlrRQ01AchFunding = new PeriodisedReportValue("ILR RQ01 Regulated Learning - Achievement Funding (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m),
                EsfRQ01AuthClaims = new PeriodisedReportValue("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
            };

            NewBuilder().BuildRegulatedLearning(HeaderStringArray(), esfValues, ilrValues).Should().BeEquivalentTo(expectedValue);
        }

        public void BuildProgressions()
        {
            IDictionary<string, IEnumerable<PeriodisedValue>> esfValues = new Dictionary<string, IEnumerable<PeriodisedValue>>
            {
                {
                    "PG01", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "PG01", "Authorised Claims", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m),
                        new PeriodisedValue("ConRef1", "PG01", "StartEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m)
                    }
                },
                {
                    "PG03", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "PG03", "Authorised Claims", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m),
                    }
                },
                {
                    "PG04", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "PG04", "Authorised Claims", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m),
                    }
                },
                {
                    "PG05", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "PG05", "Authorised Claims", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m),
                    }
                }
            };

            IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues = new Dictionary<string, IEnumerable<PeriodisedValue>>
            {
                {
                    "PG01", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "PG01", "StartEarnings", 1m, 1m, 1m, 0m, 0m, 1m, null, 1m, 1m, 1m, 1m, 1m),
                        new PeriodisedValue("ConRef1", "PG01", "AchievementEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m)
                    }
                },
                {
                    "PG03", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "PG03", "StartEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m),
                    }
                },
                {
                    "PG04", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "PG04", "StartEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m),
                    }
                },
                {
                    "PG05", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "PG05", "AchievementEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m),
                    }
                }
            };

            var expectedValue = new Progression
            {
                GroupHeader = Year2020GroupHeader("Progression and Sustained Progression"),
                IlrPG01 = new PeriodisedReportValue("ILR PG01 Progression Paid Employment(£)", 2m, 2m, 2m, 1m, 0m, 1m, 1m, 2m, 2m, 2m, 2m, 2m),
                EsfPG01 = new PeriodisedReportValue("SUPPDATA PG01 Progression Paid Employment Adjustments (£)", 2m, 2m, 2m, 0m, 0m, 2m, 2m, 2m, 0m, 2m, 0m, 2m),
                IlrPG03 = new PeriodisedReportValue("ILR PG03 Progression Education (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m),
                EsfPG03 = new PeriodisedReportValue("SUPPDATA PG03 Progression Education Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m),
                IlrPG04 = new PeriodisedReportValue("ILR PG04 Progression Apprenticeship (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m),
                EsfPG04 = new PeriodisedReportValue("SUPPDATA PG04 Progression Apprenticeship Adjustments(£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m),
                IlrPG05 = new PeriodisedReportValue("ILR PG05 Progression Traineeship (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m),
                EsfPG05 = new PeriodisedReportValue("SUPPDATA PG05 Progression Traineeship Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
            };

            NewBuilder().BuildProgressions(HeaderStringArray(), esfValues, ilrValues).Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void BuildLearnerAssessmentPlans()
        {
            IDictionary<string, IEnumerable<PeriodisedValue>> esfValues = new Dictionary<string, IEnumerable<PeriodisedValue>>
            {
                {
                    "ST01", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "ST01", "Authorised Claims", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m)
                    }
                }
            };

            IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues = new Dictionary<string, IEnumerable<PeriodisedValue>>
            {
                {
                    "ST01", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "ST01", "StartEarnings", 1m, 1m, 1m, 0m, 0m, 1m, null, 1m, 1m, 1m, 1m, 1m),
                        new PeriodisedValue("ConRef1", "ST01", "AchievementEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m)
                    }
                }
            };

            var expectedValue = new LearnerAssessmentPlan
            {
                GroupHeader = Year2020GroupHeader("Learner Assessment and Plan"),
                IlrST01 = new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
            };

            var result = NewBuilder().BuildLearnerAssessmentPlans(HeaderStringArray(), esfValues, ilrValues);
            result.Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void PopulateReportData()
        {
            var baseModels = new List<FundingSummaryModel>
            {
                new FundingSummaryModel { Year = 2020 },
                new FundingSummaryModel { Year = 2019 },
                new FundingSummaryModel { Year = 2018 }
            };

            var esfValues = EsfValuesDictionary();
            var ilrValues = IlrValuesDictionary();

            var models = NewBuilder().PopulateReportData(HeaderDictionary(), baseModels, esfValues, ilrValues);
            var expectedModels = new List<FundingSummaryModel>
            {
                new FundingSummaryModel
                {
                    Year = 2020,
                    LearnerAssessmentPlans = new LearnerAssessmentPlan
                    {
                        GroupHeader = Year2020GroupHeader("Learner Assessment and Plan"),
                        IlrST01 = new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                        EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                    },
                    RegulatedLearnings = new RegulatedLearning
                    {
                        GroupHeader = Year2020GroupHeader("Regulated Learning"),
                        IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                        IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                        EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                    },
                    NonRegulatedLearnings = new NonRegulatedLearning
                    {
                        GroupHeader = Year2020GroupHeader("Non Regulated Learning"),
                        IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                        IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                        EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                    },
                    CommunityGrants = new CommunityGrant
                    {
                        GroupHeader = Year2020GroupHeader("Community Grant"),
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                            ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                        }
                    },
                    SpecificationDefineds = new SpecificationDefined
                    {
                        GroupHeader = Year2020GroupHeader("Specification Defined"),
                        EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                        EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                    },
                    Progressions = new Progression
                    {
                        GroupHeader = Year2020GroupHeader("Progression and Sustained Progression"),
                        IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                        EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                        IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                        EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                        IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                        EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                        IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                        EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                    }
                },
                new FundingSummaryModel
                {
                    Year = 2019,
                    LearnerAssessmentPlans = new LearnerAssessmentPlan
                    {
                        GroupHeader = Year2019GroupHeader("Learner Assessment and Plan"),
                        IlrST01 = new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                        EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m)
                    },
                    RegulatedLearnings = new RegulatedLearning
                    {
                        GroupHeader = Year2019GroupHeader("Regulated Learning"),
                        IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                        IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                        EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                    },
                    NonRegulatedLearnings = new NonRegulatedLearning
                    {
                        GroupHeader = Year2019GroupHeader("Non Regulated Learning"),
                        IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                        IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                        EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                    },
                    CommunityGrants = new CommunityGrant
                    {
                        GroupHeader = Year2019GroupHeader("Community Grant"),
                        ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                                }
                            },
                    SpecificationDefineds = new SpecificationDefined
                    {
                        GroupHeader = Year2019GroupHeader("Specification Defined"),
                        EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                        EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                    },
                    Progressions = new Progression
                    {
                        GroupHeader = Year2019GroupHeader("Progression and Sustained Progression"),
                        IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                        EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                        IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                        EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                        IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                        EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                        IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                        EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                    }
                },
                new FundingSummaryModel
                {
                    Year = 2018,
                    LearnerAssessmentPlans = new LearnerAssessmentPlan
                    {
                        GroupHeader = Year2018GroupHeader("Learner Assessment and Plan"),
                        IlrST01 = new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m),
                        EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                    },
                    RegulatedLearnings = new RegulatedLearning
                    {
                        GroupHeader = Year2018GroupHeader("Regulated Learning"),
                        IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                        IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                        EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                    },
                    NonRegulatedLearnings = new NonRegulatedLearning
                    {
                        GroupHeader = Year2018GroupHeader("Non Regulated Learning"),
                        IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                        IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                        EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                    },
                    CommunityGrants = new CommunityGrant
                    {
                        GroupHeader = Year2018GroupHeader("Community Grant"),
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                            ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                        }
                    },
                    SpecificationDefineds = new SpecificationDefined
                    {
                        GroupHeader = Year2018GroupHeader("Specification Defined"),
                        EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                        EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                    },
                    Progressions = new Progression
                    {
                        GroupHeader = Year2018GroupHeader("Progression and Sustained Progression"),
                        IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                        EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                        IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                        EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                        IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                        EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                        IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                        EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                    }
                }
            };

            models.Should().BeEquivalentTo(expectedModels);
        }

        [Fact]
        public void PopulateReportData1920()
        {
            var baseModels = new List<FundingSummaryModel>
            {
                new FundingSummaryModel { Year = 2019 },
                new FundingSummaryModel { Year = 2018 }
            };

            var esfValues = EsfValuesDictionary();
            var ilrValues = IlrValuesDictionary();

            var models = NewBuilder().PopulateReportData(HeaderDictionary(), baseModels, esfValues, ilrValues);
            var expectedModels = new List<FundingSummaryModel>
            {
                new FundingSummaryModel
                {
                    Year = 2019,
                    LearnerAssessmentPlans = new LearnerAssessmentPlan
                    {
                        GroupHeader = Year2019GroupHeader("Learner Assessment and Plan"),
                        IlrST01 = new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                        EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m)
                    },
                    RegulatedLearnings = new RegulatedLearning
                    {
                        GroupHeader = Year2019GroupHeader("Regulated Learning"),
                        IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                        IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                        EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                    },
                    NonRegulatedLearnings = new NonRegulatedLearning
                    {
                        GroupHeader = Year2019GroupHeader("Non Regulated Learning"),
                        IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                        IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                        EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                    },
                    CommunityGrants = new CommunityGrant
                    {
                        GroupHeader = Year2019GroupHeader("Community Grant"),
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                            ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                        }
                    },
                    SpecificationDefineds = new SpecificationDefined
                    {
                        GroupHeader = Year2019GroupHeader("Specification Defined"),
                        EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                        EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                    },
                    Progressions = new Progression
                    {
                        GroupHeader = Year2019GroupHeader("Progression and Sustained Progression"),
                        IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                        EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                        IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                        EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                        IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                        EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                        IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                        EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                    }
                },
                new FundingSummaryModel
                {
                    Year = 2018,
                    LearnerAssessmentPlans = new LearnerAssessmentPlan
                    {
                        GroupHeader = Year2018GroupHeader("Learner Assessment and Plan"),
                        IlrST01 = new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m),
                        EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                    },
                    RegulatedLearnings = new RegulatedLearning
                    {
                        GroupHeader = Year2018GroupHeader("Regulated Learning"),
                        IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                        IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                        EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                    },
                    NonRegulatedLearnings = new NonRegulatedLearning
                    {
                        GroupHeader = Year2018GroupHeader("Non Regulated Learning"),
                        IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                        IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                        EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                    },
                    CommunityGrants = new CommunityGrant
                    {
                        GroupHeader = Year2018GroupHeader("Community Grant"),
                        ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                                }
                            },
                    SpecificationDefineds = new SpecificationDefined
                    {
                        GroupHeader = Year2018GroupHeader("Specification Defined"),
                        EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                        EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                    },
                    Progressions = new Progression
                    {
                        GroupHeader = Year2018GroupHeader("Progression and Sustained Progression"),
                        IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                        EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                        IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                        EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                        IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                        EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                        IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                        EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                    }
                }
            };

            models.Should().BeEquivalentTo(expectedModels);
        }

        [Fact]
        public void PopulateReportData_NoData()
        {
            var baseModels = new List<FundingSummaryModel>
            {
                new FundingSummaryModel { Year = 2020 },
                new FundingSummaryModel { Year = 2019 },
                new FundingSummaryModel { Year = 2018 }
            };

            var esfValues = EsfValuesDictionary();
            var ilrValues = IlrValuesDictionary();

            var models = NewBuilder().PopulateReportData(HeaderDictionary(), baseModels, esfValues, ilrValues);
            var expectedModels = new List<FundingSummaryModel>
            {
                new FundingSummaryModel
                {
                    Year = 2020,
                    LearnerAssessmentPlans = new LearnerAssessmentPlan
                    {
                        GroupHeader = Year2020GroupHeader("Learner Assessment and Plan"),
                        IlrST01 = new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                        EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                    },
                    RegulatedLearnings = new RegulatedLearning
                    {
                        GroupHeader = Year2020GroupHeader("Regulated Learning"),
                        IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                        IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                        EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                    },
                    NonRegulatedLearnings = new NonRegulatedLearning
                    {
                        GroupHeader = Year2020GroupHeader("Non Regulated Learning"),
                        IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                        IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                        EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                    },
                    CommunityGrants = new CommunityGrant
                    {
                        GroupHeader = Year2020GroupHeader("Community Grant"),
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                            ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                        }
                    },
                    SpecificationDefineds = new SpecificationDefined
                    {
                        GroupHeader = Year2020GroupHeader("Specification Defined"),
                        EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                        EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                    },
                    Progressions = new Progression
                    {
                        GroupHeader = Year2020GroupHeader("Progression and Sustained Progression"),
                        IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                        EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                        IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                        EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                        IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                        EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                        IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                        EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                    }
                },
                new FundingSummaryModel
                {
                    Year = 2019,
                    LearnerAssessmentPlans = new LearnerAssessmentPlan
                    {
                        GroupHeader = Year2019GroupHeader("Learner Assessment and Plan"),
                        IlrST01 = new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                        EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m)
                    },
                    RegulatedLearnings = new RegulatedLearning
                    {
                        GroupHeader = Year2019GroupHeader("Regulated Learning"),
                        IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                        IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                        EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                    },
                    NonRegulatedLearnings = new NonRegulatedLearning
                    {
                        GroupHeader = Year2019GroupHeader("Non Regulated Learning"),
                        IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                        IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                        EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                    },
                    CommunityGrants = new CommunityGrant
                    {
                        GroupHeader = Year2019GroupHeader("Community Grant"),
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                            ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                        }
                    },
                    SpecificationDefineds = new SpecificationDefined
                    {
                        GroupHeader = Year2019GroupHeader("Specification Defined"),
                        EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                        EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                    },
                    Progressions = new Progression
                    {
                        GroupHeader = Year2019GroupHeader("Progression and Sustained Progression"),
                        IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                        EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                        IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                        EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                        IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                        EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                        IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                        EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                    }
                },
                new FundingSummaryModel
                {
                    Year = 2018,
                    LearnerAssessmentPlans = new LearnerAssessmentPlan
                    {
                        GroupHeader = Year2018GroupHeader("Learner Assessment and Plan"),
                        IlrST01 = new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m),
                        EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                    },
                    RegulatedLearnings = new RegulatedLearning
                    {
                        GroupHeader = Year2018GroupHeader("Regulated Learning"),
                        IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                        IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                        EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                    },
                    NonRegulatedLearnings = new NonRegulatedLearning
                    {
                        GroupHeader = Year2018GroupHeader("Non Regulated Learning"),
                        IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                        IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                        EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                    },
                    CommunityGrants = new CommunityGrant
                    {
                        GroupHeader = Year2018GroupHeader("Community Grant"),
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                            ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                        }
                    },
                    SpecificationDefineds = new SpecificationDefined
                    {
                        GroupHeader = Year2018GroupHeader("Specification Defined"),
                        EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                        EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                    },
                    Progressions = new Progression
                    {
                        GroupHeader = Year2018GroupHeader("Progression and Sustained Progression"),
                        IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                        EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                        IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                        EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                        IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                        EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                        IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                        EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                    }
                }
            };

            models.Should().BeEquivalentTo(expectedModels);
        }

        [Fact]
        public void PopulateReportHeader()
        {
            var esfFile = new SourceFileModel
            {
                ConRefNumber = "ConRef",
                FileName = @"12345678\EsfFile.csv",
                PreparationDate = new DateTime(2020, 8, 1),
                SuppliedDate = new DateTime(2020, 8, 1),
                UKPRN = "12345678"
            };

            var ilrFileDetails = new List<ILRFileDetails>
            {
                new ILRFileDetails
                {
                    FileName = "File1.xml",
                    FilePreparationDate = new DateTime(2020, 8, 1),
                    LastSubmission = new DateTime(2020, 8, 1),
                    Year = 2020
                },
                new ILRFileDetails
                {
                    FileName = "File2.xml",
                    FilePreparationDate = new DateTime(2019, 8, 1),
                    LastSubmission = new DateTime(2019, 8, 1),
                    Year = 2019
                },
                new ILRFileDetails
                {
                    FileName = "File3.xml",
                    FilePreparationDate = new DateTime(2018, 8, 1),
                    LastSubmission = new DateTime(2018, 8, 1),
                    Year = 2018
                }
            };

            var expectedHeader = new FundingSummaryReportHeaderModel
            {
                ContractReferenceNumber = "ConRef",
                ProviderName = "OrgName",
                SecurityClassification = "OFFICIAL-SENSITIVE",
                Ukprn = "12345678",
                LastSupplementaryDataFileUpdate = "01/08/2020 00:00",
                SupplementaryDataFile = "EsfFile.csv",
                IlrFileDetails = new List<IlrFileDetail>
                {
                    new IlrFileDetail
                    {
                        IlrFile = "File1.xml",
                        FilePrepDate = "01/08/2020",
                        LastIlrFileUpdate = "01/08/2020 00:00",
                        Year = 2020,
                        AcademicYear = "2020/21"
                    },
                    new IlrFileDetail
                    {
                        IlrFile = "File2.xml",
                        FilePrepDate = "01/08/2019",
                        LastIlrFileUpdate = "01/08/2019 00:00",
                        Year = 2019,
                        AcademicYear = "2019/20",
                        MostRecent = "(most recent closed collection for year)"
                    },
                    new IlrFileDetail
                    {
                        IlrFile = "File3.xml",
                        FilePrepDate = "01/08/2018",
                        LastIlrFileUpdate = "01/08/2018 00:00",
                        Year = 2018,
                        AcademicYear = "2018/19",
                        MostRecent = "(most recent closed collection for year)"
                    }
                }
            };

            var yearToAcademicYearDictionary = new Dictionary<int, string>
            {
                { 2018, "2018/19" },
                { 2019, "2019/20" },
                { 2020, "2020/21" }
            };

            NewBuilder().PopulateReportHeader(esfFile, ilrFileDetails, 12345678, "OrgName", "ConRef", 2020, 2018, yearToAcademicYearDictionary)
                .Should().BeEquivalentTo(expectedHeader);
        }

        [Fact]
        public void PopulateReportHeader_1920()
        {
            var esfFile = new SourceFileModel
            {
                ConRefNumber = "ConRef",
                FileName = @"12345678\EsfFile.csv",
                PreparationDate = new DateTime(2019, 8, 1),
                SuppliedDate = new DateTime(2019, 8, 1),
                UKPRN = "12345678"
            };

            var ilrFileDetails = new List<ILRFileDetails>
            {
                new ILRFileDetails
                {
                    FileName = "File1.xml",
                    FilePreparationDate = new DateTime(2019, 8, 1),
                    LastSubmission = new DateTime(2019, 8, 1),
                    Year = 2019
                },
                new ILRFileDetails
                {
                    FileName = "File2.xml",
                    FilePreparationDate = new DateTime(2018, 8, 1),
                    LastSubmission = new DateTime(2018, 8, 1),
                    Year = 2018
                }
            };

            var expectedHeader = new FundingSummaryReportHeaderModel
            {
                ContractReferenceNumber = "ConRef",
                ProviderName = "OrgName",
                SecurityClassification = "OFFICIAL-SENSITIVE",
                Ukprn = "12345678",
                LastSupplementaryDataFileUpdate = "01/08/2019 00:00",
                SupplementaryDataFile = "EsfFile.csv",
                IlrFileDetails = new List<IlrFileDetail>
                {
                    new IlrFileDetail
                    {
                        IlrFile = "File1.xml",
                        FilePrepDate = "01/08/2019",
                        LastIlrFileUpdate = "01/08/2019 00:00",
                        Year = 2019,
                        AcademicYear = "2019/20",
                    },
                    new IlrFileDetail
                    {
                        IlrFile = "File2.xml",
                        FilePrepDate = "01/08/2018",
                        LastIlrFileUpdate = "01/08/2018 00:00",
                        Year = 2018,
                        AcademicYear = "2018/19",
                        MostRecent = "(most recent closed collection for year)"
                    }
                }
            };

            var yearToAcademicYearDictionary = new Dictionary<int, string>
            {
                { 2018, "2018/19" },
                { 2019, "2019/20" }
            };

            NewBuilder().PopulateReportHeader(esfFile, ilrFileDetails, 12345678, "OrgName", "ConRef", 2019, 2018, yearToAcademicYearDictionary)
                .Should().BeEquivalentTo(expectedHeader);
        }

        [Fact]
        public void PopulateReportHeader_NoPReviousIlr()
        {
            var esfFile = new SourceFileModel
            {
                ConRefNumber = "ConRef",
                FileName = @"12345678\EsfFile.csv",
                PreparationDate = new DateTime(2020, 8, 1),
                SuppliedDate = new DateTime(2020, 8, 1),
                UKPRN = "12345678"
            };

            var ilrFileDetails = new List<ILRFileDetails>();

            var expectedHeader = new FundingSummaryReportHeaderModel
            {
                ContractReferenceNumber = "ConRef",
                ProviderName = "OrgName",
                SecurityClassification = "OFFICIAL-SENSITIVE",
                Ukprn = "12345678",
                LastSupplementaryDataFileUpdate = "01/08/2020 00:00",
                SupplementaryDataFile = "EsfFile.csv",
                IlrFileDetails = new List<IlrFileDetail>
                {
                    new IlrFileDetail
                    {
                        Year = 2020,
                        AcademicYear = "2020/21"
                    },
                    new IlrFileDetail
                    {
                        Year = 2019,
                        AcademicYear = "2019/20",
                        MostRecent = "(most recent closed collection for year)"
                    },
                    new IlrFileDetail
                    {
                        Year = 2018,
                        AcademicYear = "2018/19",
                        MostRecent = "(most recent closed collection for year)"
                    }
                }
            };

            var yearToAcademicYearDictionary = new Dictionary<int, string>
            {
                { 2018, "2018/19" },
                { 2019, "2019/20" },
                { 2020, "2020/21" }
            };

            NewBuilder().PopulateReportHeader(esfFile, ilrFileDetails, 12345678, "OrgName", "ConRef", 2020, 2018, yearToAcademicYearDictionary)
                .Should().BeEquivalentTo(expectedHeader);
        }

        [Fact]
        public void PopulateReportHeader_NoEsf()
        {
            var ilrFileDetails = new List<ILRFileDetails>
            {
                new ILRFileDetails
                {
                    FileName = "File1.xml",
                    FilePreparationDate = new DateTime(2020, 8, 1),
                    LastSubmission = new DateTime(2020, 8, 1),
                    Year = 2020
                },
                new ILRFileDetails
                {
                    FileName = "File2.xml",
                    FilePreparationDate = new DateTime(2019, 8, 1),
                    LastSubmission = new DateTime(2019, 8, 1),
                    Year = 2019
                },
                new ILRFileDetails
                {
                    FileName = "File3.xml",
                    FilePreparationDate = new DateTime(2018, 8, 1),
                    LastSubmission = new DateTime(2018, 8, 1),
                    Year = 2018
                }
            };

            var expectedHeader = new FundingSummaryReportHeaderModel
            {
                ContractReferenceNumber = "Not Applicable",
                ProviderName = "OrgName",
                SecurityClassification = "OFFICIAL-SENSITIVE",
                Ukprn = "12345678",
                IlrFileDetails = new List<IlrFileDetail>
                {
                    new IlrFileDetail
                    {
                        IlrFile = "File1.xml",
                        FilePrepDate = "01/08/2020",
                        LastIlrFileUpdate = "01/08/2020 00:00",
                        Year = 2020,
                        AcademicYear = "2020/21"
                    },
                    new IlrFileDetail
                    {
                        IlrFile = "File2.xml",
                        FilePrepDate = "01/08/2019",
                        LastIlrFileUpdate = "01/08/2019 00:00",
                        Year = 2019,
                        AcademicYear = "2019/20",
                        MostRecent = "(most recent closed collection for year)"
                    },
                    new IlrFileDetail
                    {
                        IlrFile = "File3.xml",
                        FilePrepDate = "01/08/2018",
                        LastIlrFileUpdate = "01/08/2018 00:00",
                        Year = 2018,
                        AcademicYear = "2018/19",
                        MostRecent = "(most recent closed collection for year)"
                    }
                }
            };

            var yearToAcademicYearDictionary = new Dictionary<int, string>
            {
                { 2018, "2018/19" },
                { 2019, "2019/20" },
                { 2020, "2020/21" }
            };

            NewBuilder().PopulateReportHeader(null, ilrFileDetails, 12345678, "OrgName", "Not Applicable", 2020, 2018, yearToAcademicYearDictionary)
                .Should().BeEquivalentTo(expectedHeader);
        }

        [Fact]
        public void PopulateReportFooter()
        {
            var referenceDataVersions = new ReferenceDataVersions
            {
                LarsVersion = "LARS",
                OrganisationVersion = "ORG",
                PostcodeVersion = "POSTCODE",
            };

            var versionInfo = new Mock<IVersionInfo>();
            versionInfo.Setup(x => x.ServiceReleaseVersion).Returns("1.0.0");

            var expectedFooter = new FundingSummaryReportFooterModel
            {
                LarsData = "LARS",
                OrganisationData = "ORG",
                PostcodeData = "POSTCODE",
                ReportGeneratedAt = "01/02/2020",
                ApplicationVersion = "1.0.0"
            };

            NewBuilder(versionInfo: versionInfo.Object).PopulateReportFooter(referenceDataVersions, "01/02/2020").Should().BeEquivalentTo(expectedFooter);
        }

        [Fact]
        public async Task Build_NotApplicable()
        {
            var esfJobContext = new Mock<IEsfJobContext>();
            var cancellationToken = CancellationToken.None;
            var reportDateTime = new DateTime(2020, 8, 1);
            var ukprn = 12345678;
            var collectionYear = 2020;
            var collectionName = "ESF";
            var returnPeriod = "R01";

            var esfSourceFile = new SourceFileModel();
            var esfSourceFiles = new List<SourceFileModel> { esfSourceFile };
            var esfValues = EsfValuesDictionary();

            var ilrValues = IlrValuesDictionary();

            esfJobContext.Setup(x => x.UkPrn).Returns(ukprn);
            esfJobContext.Setup(x => x.CollectionYear).Returns(collectionYear);
            esfJobContext.Setup(x => x.CollectionName).Returns(collectionName);
            esfJobContext.Setup(x => x.ReturnPeriod).Returns(returnPeriod);
            esfJobContext.Setup(x => x.StartCollectionYearAbbreviation).Returns("20");

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(x => x.GetNowUtc()).Returns(reportDateTime);
            dateTimeProvider.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns(reportDateTime);

            var versionInfo = new Mock<IVersionInfo>();
            versionInfo.Setup(x => x.ServiceReleaseVersion).Returns("1.0.0");

            var referenceDataVersions = new ReferenceDataVersions
            {
                LarsVersion = "LARS",
                OrganisationVersion = "ORG",
                PostcodeVersion = "POSTCODE",
            };
            var orgRefData = new OrganisationReferenceData
            {
                ConRefNumbers = new List<string>(),
                Name = "OrgName",
                Ukprn = ukprn
            };

            var suppData = new Dictionary<string, IEnumerable<SupplementaryDataYearlyModel>>
            {
                {
                    "NotAMatch", new List<SupplementaryDataYearlyModel>
                    {
                        new SupplementaryDataYearlyModel
                        {
                            FundingYear = 2020,
                            SupplementaryData = new List<SupplementaryDataModel>
                            {
                                new SupplementaryDataModel
                                {
                                    DeliverableCode = "ST01",
                                    Value = 1m,
                                    ConRefNumber = "NotAMatch",
                                    CalendarYear = 2020,
                                    CalendarMonth = 8,
                                },
                                new SupplementaryDataModel
                                {
                                    DeliverableCode = "ST01",
                                    Value = 2m,
                                    ConRefNumber = "NotAMatch",
                                    CalendarYear = 2020,
                                    CalendarMonth = 9,
                                }
                            }
                        }
                    }
                }
            };

            var ilrFileDetails = new List<ILRFileDetails>
            {
                new ILRFileDetails
                {
                    FileName = "File1.xml",
                    FilePreparationDate = new DateTime(2020, 8, 1),
                    LastSubmission = new DateTime(2020, 8, 1),
                    Year = 2020
                },
                new ILRFileDetails
                {
                    FileName = "File2.xml",
                    FilePreparationDate = new DateTime(2019, 8, 1),
                    LastSubmission = new DateTime(2019, 8, 1),
                    Year = 2019
                },
                new ILRFileDetails
                {
                    FileName = "File3.xml",
                    FilePreparationDate = new DateTime(2018, 8, 1),
                    LastSubmission = new DateTime(2018, 8, 1),
                    Year = 2018
                }
            };
            var ilrPeriodisedValues = new List<FM70PeriodisedValuesYearly>();

            var yearToCollectionDictionary = new Dictionary<int, string>
            {
                { 2018, "ILR1819" },
                { 2019, "ILR1920" },
                { 2020, "ILR2021" }
            };

            var yearToAcademicYearDictionary = new Dictionary<int, string>
            {
                { 2018, "2018/19" },
                { 2019, "2019/20" },
                { 2020, "2020/21" }
            };

            var fundingConfig = new Mock<IFundingSummaryYearConfiguration>();
            fundingConfig.Setup(x => x.BaseIlrYear).Returns(2018);
            fundingConfig.Setup(x => x.YearToCollectionDictionary()).Returns(yearToCollectionDictionary);
            fundingConfig.Setup(x => x.YearToAcademicYearDictionary()).Returns(yearToAcademicYearDictionary);
            fundingConfig.Setup(x => x.PeriodisedValuesHeaderDictionary(collectionYear)).Returns(HeaderDictionary());

            var dataProvider = new Mock<IFundingSummaryReportDataProvider>();
            dataProvider.Setup(x => x.ProvideReferenceDataVersionsAsync(cancellationToken)).ReturnsAsync(referenceDataVersions);
            dataProvider.Setup(x => x.ProvideOrganisationReferenceDataAsync(ukprn, cancellationToken)).ReturnsAsync(orgRefData);
            dataProvider.Setup(x => x.GetImportFilesAsync(ukprn, cancellationToken)).ReturnsAsync(esfSourceFiles);
            dataProvider.Setup(x => x.GetSupplementaryDataAsync(collectionYear, esfSourceFiles, cancellationToken)).ReturnsAsync(suppData);
            dataProvider.Setup(x => x.GetIlrFileDetailsAsync(ukprn, new List<int> { 2020, 2019, 2018 }, cancellationToken)).ReturnsAsync(ilrFileDetails);
            dataProvider.Setup(x => x.GetYearlyIlrDataAsync(ukprn, collectionYear, returnPeriod, yearToCollectionDictionary, cancellationToken)).ReturnsAsync(ilrPeriodisedValues);

            var expectedTabs = new List<FundingSummaryReportTab>
            {
                new FundingSummaryReportTab
                {
                    TabName = "Not Applicable",
                    Title = "European Social Fund 2014-2020 (round 2)",
                    Header = new FundingSummaryReportHeaderModel
                    {
                        ContractReferenceNumber = "Not Applicable",
                        ProviderName = "OrgName",
                        SecurityClassification = "OFFICIAL-SENSITIVE",
                        Ukprn = "12345678",
                        IlrFileDetails = new List<IlrFileDetail>
                        {
                            new IlrFileDetail
                            {
                                IlrFile = "File1.xml",
                                FilePrepDate = "01/08/2020",
                                LastIlrFileUpdate = "01/08/2020 00:00",
                                Year = 2020,
                                AcademicYear = "2020/21"
                            },
                            new IlrFileDetail
                            {
                                IlrFile = "File2.xml",
                                FilePrepDate = "01/08/2019",
                                LastIlrFileUpdate = "01/08/2019 00:00",
                                Year = 2019,
                                AcademicYear = "2019/20",
                                MostRecent = "(most recent closed collection for year)"
                            },
                            new IlrFileDetail
                            {
                                IlrFile = "File3.xml",
                                FilePrepDate = "01/08/2018",
                                LastIlrFileUpdate = "01/08/2018 00:00",
                                Year = 2018,
                                AcademicYear = "2018/19",
                                MostRecent = "(most recent closed collection for year)"
                            }
                        }
                    },
                    Footer = new FundingSummaryReportFooterModel
                    {
                        LarsData = "LARS",
                        OrganisationData = "ORG",
                        PostcodeData = "POSTCODE",
                        ReportGeneratedAt = reportDateTime.ToString("HH:mm:ss") + " on " + reportDateTime.ToString("dd/MM/yyyy"),
                        ApplicationVersion = "1.0.0"
                    },
                    Body = new List<FundingSummaryModel>
                    {
                        new FundingSummaryModel
                        {
                            Year = 2020,
                            AcademicYear = "2020/21",
                            LearnerAssessmentPlans = new LearnerAssessmentPlan
                            {
                                GroupHeader = Year2020GroupHeader("Learner Assessment and Plan"),
                                IlrST01 = ZeroFundedPeriodisedValues("ILR ST01 Learner Assessment and Plan (£)"),
                                EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 2m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m)
                            },
                            RegulatedLearnings = new RegulatedLearning
                            {
                                GroupHeader = Year2020GroupHeader("Regulated Learning"),
                                IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                                IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                                EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                            },
                            NonRegulatedLearnings = new NonRegulatedLearning
                            {
                                GroupHeader = Year2020GroupHeader("Non Regulated Learning"),
                                IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                                IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                                EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                            },
                            CommunityGrants = new CommunityGrant
                            {
                                GroupHeader = Year2020GroupHeader("Community Grant"),
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                                }
                            },
                            SpecificationDefineds = new SpecificationDefined
                            {
                                GroupHeader = Year2020GroupHeader("Specification Defined"),
                                EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                                EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                            },
                            Progressions = new Progression
                            {
                                GroupHeader = Year2020GroupHeader("Progression and Sustained Progression"),
                                IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                                EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                                IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                                EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                                IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                                EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                                IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                                EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                            }
                        },
                        new FundingSummaryModel
                        {
                            Year = 2019,
                            AcademicYear = "2019/20",
                            LearnerAssessmentPlans = new LearnerAssessmentPlan
                            {
                                GroupHeader = Year2019GroupHeader("Learner Assessment and Plan"),
                                IlrST01 = ZeroFundedPeriodisedValues("ILR ST01 Learner Assessment and Plan (£)"),
                                EsfST01 = ZeroFundedPeriodisedValues("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)")
                            },
                            RegulatedLearnings = new RegulatedLearning
                            {
                                GroupHeader = Year2019GroupHeader("Regulated Learning"),
                                IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                                IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                                EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                            },
                            NonRegulatedLearnings = new NonRegulatedLearning
                            {
                                GroupHeader = Year2019GroupHeader("Non Regulated Learning"),
                                IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                                IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                                EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                            },
                            CommunityGrants = new CommunityGrant
                            {
                                GroupHeader = Year2019GroupHeader("Community Grant"),
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                                }
                            },
                            SpecificationDefineds = new SpecificationDefined
                            {
                                GroupHeader = Year2019GroupHeader("Specification Defined"),
                                EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                                EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                            },
                            Progressions = new Progression
                            {
                                GroupHeader = Year2019GroupHeader("Progression and Sustained Progression"),
                                IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                                EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                                IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                                EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                                IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                                EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                                IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                                EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                            }
                        },
                        new FundingSummaryModel
                        {
                            Year = 2018,
                            AcademicYear = "2018/19",
                            LearnerAssessmentPlans = new LearnerAssessmentPlan
                            {
                                GroupHeader = Year2018GroupHeader("Learner Assessment and Plan"),
                                IlrST01 = ZeroFundedPeriodisedValues("ILR ST01 Learner Assessment and Plan (£)"),
                                EsfST01 = ZeroFundedPeriodisedValues("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)")
                            },
                            RegulatedLearnings = new RegulatedLearning
                            {
                                GroupHeader = Year2018GroupHeader("Regulated Learning"),
                                IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                                IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                                EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                            },
                            NonRegulatedLearnings = new NonRegulatedLearning
                            {
                                GroupHeader = Year2018GroupHeader("Non Regulated Learning"),
                                IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                                IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                                EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                            },
                            CommunityGrants = new CommunityGrant
                            {
                                GroupHeader = Year2018GroupHeader("Community Grant"),
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                                }
                            },
                            SpecificationDefineds = new SpecificationDefined
                            {
                                GroupHeader = Year2018GroupHeader("Specification Defined"),
                                EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                                EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                            },
                            Progressions = new Progression
                            {
                                GroupHeader = Year2018GroupHeader("Progression and Sustained Progression"),
                                IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                                EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                                IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                                EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                                IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                                EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                                IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                                EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                            }
                        }
                    }
                }
            };

            var reportTabs = await NewBuilder(dateTimeProvider.Object, dataProvider.Object, fundingConfig.Object, versionInfo.Object).Build(esfJobContext.Object, cancellationToken);

            reportTabs.Should().BeEquivalentTo(expectedTabs);
        }

        [Fact]
        public async Task Build_MultipleContracts()
        {
            var esfJobContext = new Mock<IEsfJobContext>();
            var cancellationToken = CancellationToken.None;
            var reportDateTime = new DateTime(2020, 8, 1);
            var ukprn = 12345678;
            var collectionYear = 2020;
            var collectionName = "ESF";
            var returnPeriod = "R01";
            var conRefNumbers = new List<string> { "ConRef1", "ConRef2" };

            var esfSourceFile = new SourceFileModel();
            var esfSourceFiles = new List<SourceFileModel> { esfSourceFile };
            var esfValues = EsfValuesDictionary();

            var ilrValues = IlrValuesDictionary();

            esfJobContext.Setup(x => x.UkPrn).Returns(ukprn);
            esfJobContext.Setup(x => x.CollectionYear).Returns(collectionYear);
            esfJobContext.Setup(x => x.CollectionName).Returns(collectionName);
            esfJobContext.Setup(x => x.ReturnPeriod).Returns(returnPeriod);
            esfJobContext.Setup(x => x.StartCollectionYearAbbreviation).Returns("20");

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(x => x.GetNowUtc()).Returns(reportDateTime);
            dateTimeProvider.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns(reportDateTime);

            var versionInfo = new Mock<IVersionInfo>();
            versionInfo.Setup(x => x.ServiceReleaseVersion).Returns("1.0.0");

            var referenceDataVersions = new ReferenceDataVersions
            {
                LarsVersion = "LARS",
                OrganisationVersion = "ORG",
                PostcodeVersion = "POSTCODE",
            };
            var orgRefData = new OrganisationReferenceData
            {
                ConRefNumbers = conRefNumbers,
                Name = "OrgName",
                Ukprn = ukprn
            };

            var suppData = new Dictionary<string, IEnumerable<SupplementaryDataYearlyModel>>
            {
                {
                    "ConRef1", new List<SupplementaryDataYearlyModel>
                    {
                        new SupplementaryDataYearlyModel
                        {
                            FundingYear = 2020,
                            SupplementaryData = new List<SupplementaryDataModel>
                            {
                                new SupplementaryDataModel
                                {
                                    DeliverableCode = "ST01",
                                    Value = 1m,
                                    ConRefNumber = "ConRef1",
                                    CalendarYear = 2020,
                                    CalendarMonth = 8,
                                },
                                new SupplementaryDataModel
                                {
                                    DeliverableCode = "ST01",
                                    Value = 2m,
                                    ConRefNumber = "ConRef1",
                                    CalendarYear = 2020,
                                    CalendarMonth = 9,
                                },
                            }
                        }
                    }
                },
                {
                    "ConRef2", new List<SupplementaryDataYearlyModel>
                    {
                        new SupplementaryDataYearlyModel
                        {
                            FundingYear = 2019,
                            SupplementaryData = new List<SupplementaryDataModel>
                            {
                                new SupplementaryDataModel
                                {
                                    DeliverableCode = "ST01",
                                    Value = 1m,
                                    ConRefNumber = "ConRef2",
                                    CalendarYear = 2019,
                                    CalendarMonth = 11,
                                },
                                new SupplementaryDataModel
                                {
                                    DeliverableCode = "ST01",
                                    Value = 2m,
                                    ConRefNumber = "ConRef2",
                                    CalendarYear = 2019,
                                    CalendarMonth = 12,
                                },
                            }
                        }
                    }
                }
            };

            var ilrFileDetails = new List<ILRFileDetails>
            {
                new ILRFileDetails
                {
                    FileName = "File1.xml",
                    FilePreparationDate = new DateTime(2020, 8, 1),
                    LastSubmission = new DateTime(2020, 8, 1),
                    Year = 2020
                },
                new ILRFileDetails
                {
                    FileName = "File2.xml",
                    FilePreparationDate = new DateTime(2019, 8, 1),
                    LastSubmission = new DateTime(2019, 8, 1),
                    Year = 2019
                },
                new ILRFileDetails
                {
                    FileName = "File3.xml",
                    FilePreparationDate = new DateTime(2018, 8, 1),
                    LastSubmission = new DateTime(2018, 8, 1),
                    Year = 2018
                }
            };
            var ilrPeriodisedValues = new List<FM70PeriodisedValuesYearly>
            {
                new FM70PeriodisedValuesYearly
                {
                    FundingYear = 2020,
                    Fm70PeriodisedValues = new List<FM70PeriodisedValues>
                    {
                        new FM70PeriodisedValues
                        {
                            AttributeName = "StartEarnings",
                            ConRefNumber = "ConRef1",
                            DeliverableCode = "ST01",
                            FundingYear = 2020,
                            Period1 = 10m,
                            Period2 = 20m,
                        },
                        new FM70PeriodisedValues
                        {
                            AttributeName = "StartEarnings",
                            ConRefNumber = "ConRef2",
                            DeliverableCode = "ST01",
                            FundingYear = 2020,
                            Period1 = 10m,
                            Period2 = 20m,
                        }
                    }
                }
            };

            var yearToCollectionDictionary = new Dictionary<int, string>
            {
                { 2018, "ILR1819" },
                { 2019, "ILR1920" },
                { 2020, "ILR2021" }
            };

            var yearToAcademicYearDictionary = new Dictionary<int, string>
            {
                { 2018, "2018/19" },
                { 2019, "2019/20" },
                { 2020, "2020/21" }
            };

            var fundingConfig = new Mock<IFundingSummaryYearConfiguration>();
            fundingConfig.Setup(x => x.BaseIlrYear).Returns(2018);
            fundingConfig.Setup(x => x.YearToCollectionDictionary()).Returns(yearToCollectionDictionary);
            fundingConfig.Setup(x => x.YearToAcademicYearDictionary()).Returns(yearToAcademicYearDictionary);
            fundingConfig.Setup(x => x.PeriodisedValuesHeaderDictionary(collectionYear)).Returns(HeaderDictionary());

            var dataProvider = new Mock<IFundingSummaryReportDataProvider>();
            dataProvider.Setup(x => x.ProvideReferenceDataVersionsAsync(cancellationToken)).ReturnsAsync(referenceDataVersions);
            dataProvider.Setup(x => x.ProvideOrganisationReferenceDataAsync(ukprn, cancellationToken)).ReturnsAsync(orgRefData);
            dataProvider.Setup(x => x.GetImportFilesAsync(ukprn, cancellationToken)).ReturnsAsync(esfSourceFiles);
            dataProvider.Setup(x => x.GetSupplementaryDataAsync(collectionYear, esfSourceFiles, cancellationToken)).ReturnsAsync(suppData);
            dataProvider.Setup(x => x.GetIlrFileDetailsAsync(ukprn, new List<int> { 2020, 2019, 2018 }, cancellationToken)).ReturnsAsync(ilrFileDetails);
            dataProvider.Setup(x => x.GetYearlyIlrDataAsync(ukprn, collectionYear, returnPeriod, yearToCollectionDictionary, cancellationToken)).ReturnsAsync(ilrPeriodisedValues);

            var expectedTabs = new List<FundingSummaryReportTab>
            {
                new FundingSummaryReportTab
                {
                    TabName = "ConRef1",
                    Title = "European Social Fund 2014-2020 (round 2)",
                    Header = new FundingSummaryReportHeaderModel
                    {
                        ContractReferenceNumber = "ConRef1",
                        ProviderName = "OrgName",
                        SecurityClassification = "OFFICIAL-SENSITIVE",
                        Ukprn = "12345678",
                        IlrFileDetails = new List<IlrFileDetail>
                        {
                            new IlrFileDetail
                            {
                                IlrFile = "File1.xml",
                                FilePrepDate = "01/08/2020",
                                LastIlrFileUpdate = "01/08/2020 00:00",
                                Year = 2020,
                                AcademicYear = "2020/21"
                            },
                            new IlrFileDetail
                            {
                                IlrFile = "File2.xml",
                                FilePrepDate = "01/08/2019",
                                LastIlrFileUpdate = "01/08/2019 00:00",
                                Year = 2019,
                                AcademicYear = "2019/20",
                                MostRecent = "(most recent closed collection for year)"
                            },
                            new IlrFileDetail
                            {
                                IlrFile = "File3.xml",
                                FilePrepDate = "01/08/2018",
                                LastIlrFileUpdate = "01/08/2018 00:00",
                                Year = 2018,
                                AcademicYear = "2018/19",
                                MostRecent = "(most recent closed collection for year)"
                            }
                        }
                    },
                    Footer = new FundingSummaryReportFooterModel
                    {
                        LarsData = "LARS",
                        OrganisationData = "ORG",
                        PostcodeData = "POSTCODE",
                        ReportGeneratedAt = reportDateTime.ToString("HH:mm:ss") + " on " + reportDateTime.ToString("dd/MM/yyyy"),
                        ApplicationVersion = "1.0.0"
                    },
                    Body = new List<FundingSummaryModel>
                    {
                        new FundingSummaryModel
                        {
                            Year = 2020,
                            AcademicYear = "2020/21",
                            LearnerAssessmentPlans = new LearnerAssessmentPlan
                            {
                                GroupHeader = Year2020GroupHeader("Learner Assessment and Plan"),
                                IlrST01 = new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 10m, 20m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m),
                                EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 2m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m)
                            },
                            RegulatedLearnings = new RegulatedLearning
                            {
                                GroupHeader = Year2020GroupHeader("Regulated Learning"),
                                IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                                IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                                EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                            },
                            NonRegulatedLearnings = new NonRegulatedLearning
                            {
                                GroupHeader = Year2020GroupHeader("Non Regulated Learning"),
                                IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                                IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                                EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                            },
                            CommunityGrants = new CommunityGrant
                            {
                                GroupHeader = Year2020GroupHeader("Community Grant"),
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                                }
                            },
                            SpecificationDefineds = new SpecificationDefined
                            {
                                GroupHeader = Year2020GroupHeader("Specification Defined"),
                                EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                                EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                            },
                            Progressions = new Progression
                            {
                                GroupHeader = Year2020GroupHeader("Progression and Sustained Progression"),
                                IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                                EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                                IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                                EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                                IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                                EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                                IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                                EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                            }
                        },
                        new FundingSummaryModel
                        {
                            Year = 2019,
                            AcademicYear = "2019/20",
                            LearnerAssessmentPlans = new LearnerAssessmentPlan
                            {
                                GroupHeader = Year2019GroupHeader("Learner Assessment and Plan"),
                                IlrST01 = ZeroFundedPeriodisedValues("ILR ST01 Learner Assessment and Plan (£)"),
                                EsfST01 = ZeroFundedPeriodisedValues("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)")
                            },
                            RegulatedLearnings = new RegulatedLearning
                            {
                                GroupHeader = Year2019GroupHeader("Regulated Learning"),
                                IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                                IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                                EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                            },
                            NonRegulatedLearnings = new NonRegulatedLearning
                            {
                                GroupHeader = Year2019GroupHeader("Non Regulated Learning"),
                                IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                                IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                                EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                            },
                            CommunityGrants = new CommunityGrant
                            {
                                GroupHeader = Year2019GroupHeader("Community Grant"),
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                                }
                            },
                            SpecificationDefineds = new SpecificationDefined
                            {
                                GroupHeader = Year2019GroupHeader("Specification Defined"),
                                EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                                EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                            },
                            Progressions = new Progression
                            {
                                GroupHeader = Year2019GroupHeader("Progression and Sustained Progression"),
                                IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                                EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                                IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                                EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                                IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                                EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                                IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                                EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                            }
                        },
                        new FundingSummaryModel
                        {
                            Year = 2018,
                            AcademicYear = "2018/19",
                            LearnerAssessmentPlans = new LearnerAssessmentPlan
                            {
                                GroupHeader = Year2018GroupHeader("Learner Assessment and Plan"),
                                IlrST01 = ZeroFundedPeriodisedValues("ILR ST01 Learner Assessment and Plan (£)"),
                                EsfST01 = ZeroFundedPeriodisedValues("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)")
                            },
                            RegulatedLearnings = new RegulatedLearning
                            {
                                GroupHeader = Year2018GroupHeader("Regulated Learning"),
                                IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                                IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                                EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                            },
                            NonRegulatedLearnings = new NonRegulatedLearning
                            {
                                GroupHeader = Year2018GroupHeader("Non Regulated Learning"),
                                IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                                IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                                EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                            },
                            CommunityGrants = new CommunityGrant
                            {
                                GroupHeader = Year2018GroupHeader("Community Grant"),
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                                }
                            },
                            SpecificationDefineds = new SpecificationDefined
                            {
                                GroupHeader = Year2018GroupHeader("Specification Defined"),
                                EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                                EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                            },
                            Progressions = new Progression
                            {
                                GroupHeader = Year2018GroupHeader("Progression and Sustained Progression"),
                                IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                                EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                                IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                                EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                                IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                                EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                                IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                                EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                            }
                        }
                    }
                },
                new FundingSummaryReportTab
                {
                    TabName = "ConRef2",
                    Title = "European Social Fund 2014-2020 (round 2)",
                    Header = new FundingSummaryReportHeaderModel
                    {
                        ContractReferenceNumber = "ConRef2",
                        ProviderName = "OrgName",
                        SecurityClassification = "OFFICIAL-SENSITIVE",
                        Ukprn = "12345678",
                        IlrFileDetails = new List<IlrFileDetail>
                        {
                            new IlrFileDetail
                            {
                                IlrFile = "File1.xml",
                                FilePrepDate = "01/08/2020",
                                LastIlrFileUpdate = "01/08/2020 00:00",
                                Year = 2020,
                                AcademicYear = "2020/21"
                            },
                            new IlrFileDetail
                            {
                                IlrFile = "File2.xml",
                                FilePrepDate = "01/08/2019",
                                LastIlrFileUpdate = "01/08/2019 00:00",
                                Year = 2019,
                                AcademicYear = "2019/20",
                                MostRecent = "(most recent closed collection for year)"
                            },
                            new IlrFileDetail
                            {
                                IlrFile = "File3.xml",
                                FilePrepDate = "01/08/2018",
                                LastIlrFileUpdate = "01/08/2018 00:00",
                                Year = 2018,
                                AcademicYear = "2018/19",
                                MostRecent = "(most recent closed collection for year)"
                            }
                        }
                    },
                    Footer = new FundingSummaryReportFooterModel
                    {
                        LarsData = "LARS",
                        OrganisationData = "ORG",
                        PostcodeData = "POSTCODE",
                        ReportGeneratedAt = reportDateTime.ToString("HH:mm:ss") + " on " + reportDateTime.ToString("dd/MM/yyyy"),
                        ApplicationVersion = "1.0.0"
                    },
                    Body = new List<FundingSummaryModel>
                    {
                        new FundingSummaryModel
                        {
                            Year = 2020,
                            AcademicYear = "2020/21",
                            LearnerAssessmentPlans = new LearnerAssessmentPlan
                            {
                                GroupHeader = Year2020GroupHeader("Learner Assessment and Plan"),
                                IlrST01 = new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 10m, 20m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m),
                                EsfST01 = ZeroFundedPeriodisedValues("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)")
                            },
                            RegulatedLearnings = new RegulatedLearning
                            {
                                GroupHeader = Year2020GroupHeader("Regulated Learning"),
                                IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                                IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                                EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                            },
                            NonRegulatedLearnings = new NonRegulatedLearning
                            {
                                GroupHeader = Year2020GroupHeader("Non Regulated Learning"),
                                IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                                IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                                EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                            },
                            CommunityGrants = new CommunityGrant
                            {
                                GroupHeader = Year2020GroupHeader("Community Grant"),
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                                }
                            },
                            SpecificationDefineds = new SpecificationDefined
                            {
                                GroupHeader = Year2020GroupHeader("Specification Defined"),
                                EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                                EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                            },
                            Progressions = new Progression
                            {
                                GroupHeader = Year2020GroupHeader("Progression and Sustained Progression"),
                                IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                                EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                                IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                                EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                                IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                                EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                                IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                                EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                            }
                        },
                        new FundingSummaryModel
                        {
                            Year = 2019,
                            AcademicYear = "2019/20",
                            LearnerAssessmentPlans = new LearnerAssessmentPlan
                            {
                                GroupHeader = Year2019GroupHeader("Learner Assessment and Plan"),
                                IlrST01 = ZeroFundedPeriodisedValues("ILR ST01 Learner Assessment and Plan (£)"),
                                EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 0m, 0m, 0m, 1m, 2m, 0m, 0m, 0m, 0m, 0m, 0m, 0m)
                            },
                            RegulatedLearnings = new RegulatedLearning
                            {
                                GroupHeader = Year2019GroupHeader("Regulated Learning"),
                                IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                                IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                                EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                            },
                            NonRegulatedLearnings = new NonRegulatedLearning
                            {
                                GroupHeader = Year2019GroupHeader("Non Regulated Learning"),
                                IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                                IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                                EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                            },
                            CommunityGrants = new CommunityGrant
                            {
                                GroupHeader = Year2019GroupHeader("Community Grant"),
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                                }
                            },
                            SpecificationDefineds = new SpecificationDefined
                            {
                                GroupHeader = Year2019GroupHeader("Specification Defined"),
                                EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                                EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                            },
                            Progressions = new Progression
                            {
                                GroupHeader = Year2019GroupHeader("Progression and Sustained Progression"),
                                IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                                EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                                IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                                EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                                IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                                EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                                IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                                EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                            }
                        },
                        new FundingSummaryModel
                        {
                            Year = 2018,
                            AcademicYear = "2018/19",
                            LearnerAssessmentPlans = new LearnerAssessmentPlan
                            {
                                GroupHeader = Year2018GroupHeader("Learner Assessment and Plan"),
                                IlrST01 = ZeroFundedPeriodisedValues("ILR ST01 Learner Assessment and Plan (£)"),
                                EsfST01 = ZeroFundedPeriodisedValues("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)")
                            },
                            RegulatedLearnings = new RegulatedLearning
                            {
                                GroupHeader = Year2018GroupHeader("Regulated Learning"),
                                IlrRQ01AchFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                                IlrRQ01StartFunding = ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                                EsfRQ01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                            },
                            NonRegulatedLearnings = new NonRegulatedLearning
                            {
                                GroupHeader = Year2018GroupHeader("Non Regulated Learning"),
                                IlrNR01AchFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Achievement Funding (£)"),
                                IlrNR01StartFunding = ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Learning - Start Funding (£)"),
                                EsfNR01AuthClaims = ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)")
                            },
                            CommunityGrants = new CommunityGrant
                            {
                                GroupHeader = Year2018GroupHeader("Community Grant"),
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                                }
                            },
                            SpecificationDefineds = new SpecificationDefined
                            {
                                GroupHeader = Year2018GroupHeader("Specification Defined"),
                                EsfSD01 = ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                                EsfSD02 = ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                            },
                            Progressions = new Progression
                            {
                                GroupHeader = Year2018GroupHeader("Progression and Sustained Progression"),
                                IlrPG01 = ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                                EsfPG01 = ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                                IlrPG03 = ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                                EsfPG03 = ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                                IlrPG04 = ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                                EsfPG04 = ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                                IlrPG05 = ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                                EsfPG05 = ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                            }
                        }
                    }
                }
            };

            var reportTabs = await NewBuilder(dateTimeProvider.Object, dataProvider.Object, fundingConfig.Object, versionInfo.Object).Build(esfJobContext.Object, cancellationToken);

            reportTabs.Should().BeEquivalentTo(expectedTabs);
        }

        private IDictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>> EsfValuesDictionary()
        {
            return new Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>
            {
                {
                    2020,  new Dictionary<string, IEnumerable<PeriodisedValue>>
                    {
                        {
                            "ST01", new List<PeriodisedValue>
                            {
                                new PeriodisedValue("ConRef1", "ST01", "Authorised Claims", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m)
                            }
                        }
                    }
                },
                {
                    2018,  new Dictionary<string, IEnumerable<PeriodisedValue>>
                    {
                        {
                            "ST01", new List<PeriodisedValue>
                            {
                                new PeriodisedValue("ConRef1", "ST01", "Authorised Claims", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, null, 1m, 0m, 1m)
                            }
                        }
                    }
                }
            };
        }

        private IDictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>> IlrValuesDictionary()
        {
            return new Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>
            {
                {
                    2020,  new Dictionary<string, IEnumerable<PeriodisedValue>>
                    {
                        {
                            "ST01", new List<PeriodisedValue>
                            {
                                new PeriodisedValue("ConRef1", "ST01", "StartEarnings", 1m, 1m, 1m, 0m, 0m, 1m, null, 1m, 1m, 1m, 1m, 1m),
                                new PeriodisedValue("ConRef1", "ST01", "AchievementEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m)
                            }
                        }
                    }
                },
                {
                    2019,  new Dictionary<string, IEnumerable<PeriodisedValue>>
                    {
                        {
                            "ST01", new List<PeriodisedValue>
                            {
                                new PeriodisedValue("ConRef1", "ST01", "StartEarnings", 1m, 1m, 1m, 0m, 0m, 1m, null, 1m, 1m, 1m, 1m, 1m),
                                new PeriodisedValue("ConRef1", "ST01", "AchievementEarnings", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m)
                            }
                        }
                    }
                }
            };
        }

        private string[] HeaderStringArray() => new string[]
        {
            "August 2020",
            "September 2020",
            "October 2020",
            "November 2020",
            "December 2020",
            "January 2021",
            "February 2021",
            "March 2021",
            "April 2021",
            "May 2021",
            "June 2021",
            "July 2021"
        };

        private GroupHeader Year2020GroupHeader(string title) => new GroupHeader(
            title,
            "August 2020",
            "September 2020",
            "October 2020",
            "November 2020",
            "December 2020",
            "January 2021",
            "February 2021",
            "March 2021",
            "April 2021",
            "May 2021",
            "June 2021",
            "July 2021");

        private GroupHeader Year2019GroupHeader(string title) => new GroupHeader(
            title,
            "August 2019",
            "September 2019",
            "October 2019",
            "November 2019",
            "December 2019",
            "January 2020",
            "February 2020",
            "March 2020",
            "April 2020",
            "May 2020",
            "June 2020",
            "July 2020");

        private GroupHeader Year2018GroupHeader(string title) => new GroupHeader(
            title,
            "August 2018",
            "September 2018",
            "October 2018",
            "November 2018",
            "December 2018",
            "January 2019",
            "February 2019",
            "March 2019",
            "April 2019",
            "May 2019",
            "June 2019",
            "July 2019");

        private IDictionary<int, string[]> HeaderDictionary()
        {
            return new Dictionary<int, string[]>
            {
                {
                    2020, new string[]
                    {
                        "August 2020",
                        "September 2020",
                        "October 2020",
                        "November 2020",
                        "December 2020",
                        "January 2021",
                        "February 2021",
                        "March 2021",
                        "April 2021",
                        "May 2021",
                        "June 2021",
                        "July 2021"
                    }
                },
                {
                    2019, new string[]
                    {
                        "August 2019",
                        "September 2019",
                        "October 2019",
                        "November 2019",
                        "December 2019",
                        "January 2020",
                        "February 2020",
                        "March 2020",
                        "April 2020",
                        "May 2020",
                        "June 2020",
                        "July 2020"
                    }
                },
                {
                    2018, new string[]
                    {
                        "August 2018",
                        "September 2018",
                        "October 2018",
                        "November 2018",
                        "December 2018",
                        "January 2019",
                        "February 2019",
                        "March 2019",
                        "April 2019",
                        "May 2019",
                        "June 2019",
                        "July 2019"
                    }
                }
            };
        }

        private PeriodisedReportValue ZeroFundedPeriodisedValues(string title) => new PeriodisedReportValue(title, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m);

        private FundingSummaryReportModelBuilder NewBuilder(
            IDateTimeProvider dateTimeProvider = null,
            IFundingSummaryReportDataProvider dataProvider = null,
            IFundingSummaryYearConfiguration fundingConfig = null,
            IVersionInfo versionInfo = null)
        {
            return new FundingSummaryReportModelBuilder(dateTimeProvider, dataProvider, fundingConfig, versionInfo, Mock.Of<ILogger>());
        }
    }
}