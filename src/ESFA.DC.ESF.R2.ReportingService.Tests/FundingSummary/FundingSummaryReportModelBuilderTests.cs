using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.DateTimeProvider.Interface;
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

            var expectedValue = new DeliverableCategory("Total Specification Defined (£)")
            {
                GroupHeader = Year2020GroupHeader("Specification Defined"),
                DeliverableSubCategories = new List<IDeliverableSubCategory>
                {
                    new DeliverableSubCategory("Default Category should not render", false)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            new PeriodisedReportValue("SUPPDATA SD01 Progression Within Work (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                            new PeriodisedReportValue("SUPPDATA SD02 LEP Agreed Delivery Plan (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                        }
                    }
                }
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

            var expectedValue = new DeliverableCategory("Total Community Grant (£)")
            {
                GroupHeader = Year2020GroupHeader("Community Grant"),
                DeliverableSubCategories = new List<IDeliverableSubCategory>
                {
                    new DeliverableSubCategory("Default Category should not render", false)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            new PeriodisedReportValue("SUPPDATA CG01 Community Grant Payment (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                            new PeriodisedReportValue("SUPPDATA CG02 Community Grant Management Cost (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                        }
                    }
                }
            };

            NewBuilder().BuildCommunityGrants(HeaderStringArray(), esfValues).Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void BuildNonRegulatedActivity()
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

            var expectedValue = new DeliverableCategory("Total Non Regulated Activity (£)")
            {
                GroupHeader = Year2020GroupHeader("Non Regulated Activity"),
                DeliverableSubCategories = new List<IDeliverableSubCategory>
                {
                    new DeliverableSubCategory("ILR Total NR01 Non Regulated Activity (£)", true)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            new PeriodisedReportValue("ILR NR01 Non Regulated Activity - Start Funding (£)", 1m, 1m, 1m, 0m, 0m, 1m, 0m, 1m, 1m, 1m, 1m, 1m),
                            new PeriodisedReportValue("ILR NR01 Non Regulated Activity - Achievement Funding (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m)
                        }
                    },
                    new DeliverableSubCategory("Default Category should not render", false)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            new PeriodisedReportValue("SUPPDATA NR01 Non Regulated Activity Authorised Claims (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                        }
                    }
                }
            };

            NewBuilder().BuildNonRegulatedActivity(HeaderStringArray(), esfValues, ilrValues).Should().BeEquivalentTo(expectedValue);
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

            var expectedValue = new DeliverableCategory("Total Regulated Learning (£)")
            {
                GroupHeader = Year2020GroupHeader("Regulated Learning"),
                DeliverableSubCategories = new List<IDeliverableSubCategory>
                {
                    new DeliverableSubCategory("ILR Total RQ01 Regulated Learning (£)", true)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            new PeriodisedReportValue("ILR RQ01 Regulated Learning - Start Funding (£)", 1m, 1m, 1m, 0m, 0m, 1m, 0m, 1m, 1m, 1m, 1m, 1m),
                            new PeriodisedReportValue("ILR RQ01 Regulated Learning - Achievement Funding (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m),
                        }
                    },
                    new DeliverableSubCategory("Default Category should not render", false)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            new PeriodisedReportValue("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                        }
                    }
                }
            };

            NewBuilder().BuildRegulatedLearning(HeaderStringArray(), esfValues, ilrValues).Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
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

            var expectedValue = new DeliverableCategory("Total Progression and Sustained Progression (£)")
            {
                GroupHeader = Year2020GroupHeader("Progression and Sustained Progression"),
                DeliverableSubCategories = new List<IDeliverableSubCategory>()
                {
                    new DeliverableSubCategory("Total Paid Employment Progression (£)", true)
                    {
                        ReportValues = new List<IPeriodisedReportValue>()
                        {
                            new PeriodisedReportValue("ILR PG01 Progression Paid Employment (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                            new PeriodisedReportValue("SUPPDATA PG01 Progression Paid Employment Adjustments (£)", 2m, 2m, 2m, 0m, 0m, 2m, 2m, 2m, 0m, 2m, 0m, 2m)
                        }
                    },
                    new DeliverableSubCategory("Total Education Progression (£)", true)
                    {
                        ReportValues = new List<IPeriodisedReportValue>()
                        {
                            new PeriodisedReportValue("ILR PG03 Progression Education (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m),
                            new PeriodisedReportValue("SUPPDATA PG03 Progression Education Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                        }
                    },
                    new DeliverableSubCategory("Total Apprenticeship Progression (£)", true)
                    {
                        ReportValues = new List<IPeriodisedReportValue>()
                        {
                            new PeriodisedReportValue("ILR PG04 Progression Apprenticeship (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m),
                            new PeriodisedReportValue("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                        }
                    },
                    new DeliverableSubCategory("Total Traineeship Progression (£)", true)
                    {
                        ReportValues = new List<IPeriodisedReportValue>()
                        {
                            new PeriodisedReportValue("ILR PG05 Progression Traineeship (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m),
                            new PeriodisedReportValue("SUPPDATA PG05 Progression Traineeship Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                        }
                    }
                }
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

            var expectedValue = new DeliverableCategory("Total Learner Assessment and Plan (£)")
            {
                GroupHeader = Year2020GroupHeader("Learner Assessment and Plan"),
                DeliverableSubCategories = new List<IDeliverableSubCategory>
                {
                    new DeliverableSubCategory("Default Category should not render", false)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                            new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                        }
                    }
                }
            };

            var result = NewBuilder().BuildLearnerAssessmentPlans(HeaderStringArray(), esfValues, ilrValues);
            result.Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void PopulateReportData()
        {
            string conRefNumber = null;
            var baseModels = new List<FundingSummaryModel>
            {
                new FundingSummaryModel { Year = 2020 },
                new FundingSummaryModel { Year = 2019 },
                new FundingSummaryModel { Year = 2018 }
            };

            var esfValues = EsfValuesDictionary();
            var ilrValues = IlrValuesDictionary();

            var models = NewBuilder().PopulateReportData(conRefNumber, HeaderDictionary(), baseModels, esfValues, ilrValues);
            var expectedModels = new List<FundingSummaryModel>
            {
                new FundingSummaryModel
                {
                    Year = 2020,
                    YearTotal = 27M,
                    CumulativeYearTotal = 54M,
                    PreviousYearCumulativeTotal = 27m,
                    LearnerAssessmentPlans = new DeliverableCategory("Total Learner Assessment and Plan (£)")
                    {
                        GroupHeader = Year2020GroupHeader("Learner Assessment and Plan"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>
                        {
                            new DeliverableSubCategory("Default Category should not render", false)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                                    new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                                }
                            }
                        }
                    },
                    RegulatedLearnings = new DeliverableCategory("Total Regulated Learning (£)")
                    {
                        GroupHeader = Year2020GroupHeader("Regulated Learning"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>()
                        {
                            new DeliverableSubCategory("ILR Total RQ01 Regulated Learning (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                                    ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                                }
                            },
                            new DeliverableSubCategory("Default Category should not render", false)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                                }
                            }
                        }
                    },
                    NonRegulatedActivities = new DeliverableCategory("Total Non Regulated Activity (£)")
                    {
                        GroupHeader = Year2020GroupHeader("Non Regulated Activity"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>
                        {
                            new DeliverableSubCategory("ILR Total NR01 Non Regulated Activity (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Activity - Achievement Funding (£)"),
                                    ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Activity - Start Funding (£)")
                                }
                            },
                            new DeliverableSubCategory("Default Category should not render", false)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Activity Authorised Claims (£)")
                                }
                            }
                        }
                    },
                    CommunityGrants = new DeliverableCategory("Total Community Grant (£)")
                    {
                        GroupHeader = Year2020GroupHeader("Community Grant"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>
                        {
                            new DeliverableSubCategory("Default Category should not render", false)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                                }
                            }
                        }
                    },
                    SpecificationDefineds = new DeliverableCategory("Total Specification Defined (£)")
                    {
                        GroupHeader = Year2020GroupHeader("Specification Defined"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>
                        {
                            new DeliverableSubCategory("Default Category should not render", false)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                                }
                            }
                        }
                    },
                    Progressions = new DeliverableCategory("Total Progression and Sustained Progression (£)")
                    {
                        GroupHeader = Year2020GroupHeader("Progression and Sustained Progression"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>
                        {
                            new DeliverableSubCategory("Total Paid Employment Progression (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                                }
                            },
                            new DeliverableSubCategory("Total Education Progression (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                                }
                            },
                            new DeliverableSubCategory("Total Apprenticeship Progression (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                                }
                            },
                            new DeliverableSubCategory("Total Traineeship Progression (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                                }
                            }
                        }
                    }
                },
                new FundingSummaryModel
                {
                    Year = 2019,
                    YearTotal = 19M,
                    CumulativeYearTotal = 27M,
                    PreviousYearCumulativeTotal = 8m,
                    LearnerAssessmentPlans = new DeliverableCategory("Total Learner Assessment and Plan (£)")
                    {
                        GroupHeader = Year2019GroupHeader("Learner Assessment and Plan"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>
                        {
                            new DeliverableSubCategory("Default Category should not render", false)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                                    new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m)
                                }
                            }
                        }
                    },
                    RegulatedLearnings = new DeliverableCategory("Total Regulated Learning (£)")
                    {
                        GroupHeader = Year2019GroupHeader("Regulated Learning"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>()
                        {
                            new DeliverableSubCategory("ILR Total RQ01 Regulated Learning (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                                    ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                                }
                            },
                            new DeliverableSubCategory("Default Category should not render", false)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                                }
                            }
                        }
                    },
                    NonRegulatedActivities = new DeliverableCategory("Total Non Regulated Activity (£)")
                    {
                        GroupHeader = Year2019GroupHeader("Non Regulated Activity"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>
                        {
                            new DeliverableSubCategory("ILR Total NR01 Non Regulated Activity (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Activity - Achievement Funding (£)"),
                                    ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Activity - Start Funding (£)")
                                }
                            },
                            new DeliverableSubCategory("Default Category should not render", false)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Activity Authorised Claims (£)")
                                }
                            }
                        }
                    },
                    CommunityGrants = new DeliverableCategory("Total Community Grant (£)")
                    {
                        GroupHeader = Year2019GroupHeader("Community Grant"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>
                        {
                            new DeliverableSubCategory("Default Category should not render", false)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                                }
                            }
                        }
                    },
                    SpecificationDefineds = new DeliverableCategory("Total Specification Defined (£)")
                    {
                        GroupHeader = Year2019GroupHeader("Specification Defined"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>
                        {
                            new DeliverableSubCategory("Default Category should not render", false)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                                }
                            }
                        }
                    },
                    Progressions = new DeliverableCategory("Total Progression and Sustained Progression (£)")
                    {
                        GroupHeader = Year2019GroupHeader("Progression and Sustained Progression"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>
                        {
                            new DeliverableSubCategory("Total Paid Employment Progression (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                                }
                            },
                            new DeliverableSubCategory("Total Education Progression (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                                }
                            },
                            new DeliverableSubCategory("Total Apprenticeship Progression (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                                }
                            },
                            new DeliverableSubCategory("Total Traineeship Progression (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                                }
                            }
                        }
                    }
                },
                new FundingSummaryModel
                {
                    Year = 2018,
                    YearTotal = 8M,
                    CumulativeYearTotal = 8M,
                    LearnerAssessmentPlans = new DeliverableCategory("Total Learner Assessment and Plan (£)")
                    {
                        GroupHeader = Year2018GroupHeader("Learner Assessment and Plan"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>
                        {
                            new DeliverableSubCategory("Default Category should not render", false)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m),
                                    new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                                }
                            }
                        }
                    },
                    RegulatedLearnings = new DeliverableCategory("Total Regulated Learning (£)")
                    {
                        GroupHeader = Year2018GroupHeader("Regulated Learning"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>()
                        {
                            new DeliverableSubCategory("ILR Total RQ01 Regulated Learning (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Achievement Funding (£)"),
                                    ZeroFundedPeriodisedValues("ILR RQ01 Regulated Learning - Start Funding (£)"),
                                }
                            },
                            new DeliverableSubCategory("Default Category should not render", false)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)")
                                }
                            }
                        }
                    },
                    NonRegulatedActivities = new DeliverableCategory("Total Non Regulated Activity (£)")
                    {
                        GroupHeader = Year2018GroupHeader("Non Regulated Activity"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>
                        {
                            new DeliverableSubCategory("ILR Total NR01 Non Regulated Activity (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Activity - Achievement Funding (£)"),
                                    ZeroFundedPeriodisedValues("ILR NR01 Non Regulated Activity - Start Funding (£)")
                                }
                            },
                            new DeliverableSubCategory("Default Category should not render", false)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA NR01 Non Regulated Activity Authorised Claims (£)")
                                }
                            }
                        }
                    },
                    CommunityGrants = new DeliverableCategory("Total Community Grant (£)")
                    {
                        GroupHeader = Year2018GroupHeader("Community Grant"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>
                        {
                            new DeliverableSubCategory("Default Category should not render", false)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA CG01 Community Grant Payment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA CG02 Community Grant Management Cost (£)")
                                }
                            }
                        }
                    },
                    SpecificationDefineds = new DeliverableCategory("Total Specification Defined (£)")
                    {
                        GroupHeader = Year2018GroupHeader("Specification Defined"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>
                        {
                            new DeliverableSubCategory("Default Category should not render", false)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("SUPPDATA SD01 Progression Within Work (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA SD02 LEP Agreed Delivery Plan (£)")
                                }
                            }
                        }
                    },
                    Progressions = new DeliverableCategory("Total Progression and Sustained Progression (£)")
                    {
                        GroupHeader = Year2018GroupHeader("Progression and Sustained Progression"),
                        DeliverableSubCategories = new List<IDeliverableSubCategory>
                        {
                            new DeliverableSubCategory("Total Paid Employment Progression (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR PG01 Progression Paid Employment (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA PG01 Progression Paid Employment Adjustments (£)"),
                                }
                            },
                            new DeliverableSubCategory("Total Education Progression (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR PG03 Progression Education (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA PG03 Progression Education Adjustments (£)"),
                                }
                            },
                            new DeliverableSubCategory("Total Apprenticeship Progression (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR PG04 Progression Apprenticeship (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)"),
                                }
                            },
                            new DeliverableSubCategory("Total Traineeship Progression (£)", true)
                            {
                                ReportValues = new List<IPeriodisedReportValue>
                                {
                                    ZeroFundedPeriodisedValues("ILR PG05 Progression Traineeship (£)"),
                                    ZeroFundedPeriodisedValues("SUPPDATA PG05 Progression Traineeship Adjustments (£)"),
                                }
                            }
                        }
                    }
                }
            };

            models.Should().BeEquivalentTo(expectedModels);
        }

        [Fact]
        public void PopulateReportData_CheckYearlyTotals()
        {
            string conRefNumber = null;
            var baseModels = new List<FundingSummaryModel>
            {
                new FundingSummaryModel { Year = 2020 },
                new FundingSummaryModel { Year = 2019 },
                new FundingSummaryModel { Year = 2018 }
            };

            var esfValues = EsfValuesDictionary();
            var ilrValues = IlrValuesDictionary();

            var models = NewBuilder().PopulateReportData(conRefNumber, HeaderDictionary(), baseModels, esfValues, ilrValues);

            models.Where(x => x.Year == 2018).FirstOrDefault().YearTotal.Should().Be(8);
            models.Where(x => x.Year == 2018).FirstOrDefault().CumulativeYearTotal.Should().Be(8);
            models.Where(x => x.Year == 2018).FirstOrDefault().PreviousYearCumulativeTotal.Should().BeNull();

            models.Where(x => x.Year == 2019).FirstOrDefault().YearTotal.Should().Be(19);
            models.Where(x => x.Year == 2019).FirstOrDefault().CumulativeYearTotal.Should().Be(27);
            models.Where(x => x.Year == 2019).FirstOrDefault().PreviousYearCumulativeTotal.Should().Be(8);

            models.Where(x => x.Year == 2020).FirstOrDefault().YearTotal.Should().Be(27);
            models.Where(x => x.Year == 2020).FirstOrDefault().CumulativeYearTotal.Should().Be(54);
            models.Where(x => x.Year == 2020).FirstOrDefault().PreviousYearCumulativeTotal.Should().Be(27);
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
        public void PopulateReportHeader_NoPreviousIlr()
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