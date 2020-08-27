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
    public class FundingSummaryReportModelBuilderTests : AbstractFundingSummaryReportTests
    {
        [Fact]
        public void BuildPeriodisedReportValue()
        {
            var title = "Title";
            IEnumerable<PeriodisedValue> periodisedValues = new List<PeriodisedValue>
            {
                new PeriodisedValue("ConRef1", "Code1", "StartEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 0m, 1m, 1m, 1m, 1m, 1m }),
                new PeriodisedValue("ConRef1", "Code1", "AchievementEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
            };

            var expectedValue = new PeriodisedReportValue("Title", new decimal[] { 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 1m, 2m, 1m, 2m });

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
                        new PeriodisedValue("ConRef1", "SD01", "StartEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 0m, 1m, 1m, 1m, 1m, 1m }),
                        new PeriodisedValue("ConRef1", "SD01", "AchievementEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m })
                    }
                },
                {
                    "SD02", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "SD02", "AchievementEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
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
                            new PeriodisedReportValue("SUPPDATA SD01 Progression Within Work (£)", new decimal[] { 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m }),
                            new PeriodisedReportValue("SUPPDATA SD02 LEP Agreed Delivery Plan (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
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
                        new PeriodisedValue("ConRef1", "CG01", "StartEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 0m, 1m, 1m, 1m, 1m, 1m }),
                        new PeriodisedValue("ConRef1", "CG01", "AchievementEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m })
                    }
                },
                {
                    "CG02", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "CG02", "AchievementEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m }) }
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
                            new PeriodisedReportValue("SUPPDATA CG01 Community Grant Payment (£)", new decimal[] { 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m }),
                            new PeriodisedReportValue("SUPPDATA CG02 Community Grant Management Cost (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
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
                        new PeriodisedValue("ConRef1", "NR01", "Authorised Claims", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
                    }
                }
            };

            IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues = new Dictionary<string, IEnumerable<PeriodisedValue>>
            {
                {
                    "NR01", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "NR01", "StartEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 0m, 1m, 1m, 1m, 1m, 1m }),
                        new PeriodisedValue("ConRef1", "NR01", "AchievementEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m })
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
                            new PeriodisedReportValue("ILR NR01 Non Regulated Activity - Start Funding (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 0m, 1m, 1m, 1m, 1m, 1m }),
                            new PeriodisedReportValue("ILR NR01 Non Regulated Activity - Achievement Funding (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m })
                        }
                    },
                    new DeliverableSubCategory("Default Category should not render", false)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            new PeriodisedReportValue("SUPPDATA NR01 Non Regulated Activity Authorised Claims (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
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
                        new PeriodisedValue("ConRef1", "RQ01", "Authorised Claims", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
                    }
                }
            };

            IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues = new Dictionary<string, IEnumerable<PeriodisedValue>>
            {
                {
                    "RQ01", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "RQ01", "StartEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 0m, 1m, 1m, 1m, 1m, 1m }),
                        new PeriodisedValue("ConRef1", "RQ01", "AchievementEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m })
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
                            new PeriodisedReportValue("ILR RQ01 Regulated Learning - Start Funding (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 0m, 1m, 1m, 1m, 1m, 1m }),
                            new PeriodisedReportValue("ILR RQ01 Regulated Learning - Achievement Funding (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m }),
                        }
                    },
                    new DeliverableSubCategory("Default Category should not render", false)
                    {
                        ReportValues = new List<IPeriodisedReportValue>
                        {
                            new PeriodisedReportValue("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
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
                        new PeriodisedValue("ConRef1", "PG01", "Authorised Claims", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m }),
                        new PeriodisedValue("ConRef1", "PG01", "StartEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
                    }
                },
                {
                    "PG03", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "PG03", "Authorised Claims", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m }),
                    }
                },
                {
                    "PG04", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "PG04", "Authorised Claims", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m }),
                    }
                },
                {
                    "PG05", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "PG05", "Authorised Claims", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m }),
                    }
                }
            };

            IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues = new Dictionary<string, IEnumerable<PeriodisedValue>>
            {
                {
                    "PG01", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "PG01", "StartEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 0m, 1m, 1m, 1m, 1m, 1m }),
                        new PeriodisedValue("ConRef1", "PG01", "AchievementEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m })
                    }
                },
                {
                    "PG03", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "PG03", "StartEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m }),
                    }
                },
                {
                    "PG04", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "PG04", "StartEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m }),
                    }
                },
                {
                    "PG05", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "PG05", "AchievementEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m }),
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
                            new PeriodisedReportValue("ILR PG01 Progression Paid Employment (£)", new decimal[] { 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m }),
                            new PeriodisedReportValue("SUPPDATA PG01 Progression Paid Employment Adjustments (£)", new decimal[] { 2m, 2m, 2m, 0m, 0m, 2m, 2m, 2m, 0m, 2m, 0m, 2m })
                        }
                    },
                    new DeliverableSubCategory("Total Education Progression (£)", true)
                    {
                        ReportValues = new List<IPeriodisedReportValue>()
                        {
                            new PeriodisedReportValue("ILR PG03 Progression Education (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m }),
                            new PeriodisedReportValue("SUPPDATA PG03 Progression Education Adjustments (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
                        }
                    },
                    new DeliverableSubCategory("Total Apprenticeship Progression (£)", true)
                    {
                        ReportValues = new List<IPeriodisedReportValue>()
                        {
                            new PeriodisedReportValue("ILR PG04 Progression Apprenticeship (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m }),
                            new PeriodisedReportValue("SUPPDATA PG04 Progression Apprenticeship Adjustments (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
                        }
                    },
                    new DeliverableSubCategory("Total Traineeship Progression (£)", true)
                    {
                        ReportValues = new List<IPeriodisedReportValue>()
                        {
                            new PeriodisedReportValue("ILR PG05 Progression Traineeship (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m }),
                            new PeriodisedReportValue("SUPPDATA PG05 Progression Traineeship Adjustments (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
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
                        new PeriodisedValue("ConRef1", "ST01", "Authorised Claims", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
                    }
                }
            };

            IDictionary<string, IEnumerable<PeriodisedValue>> ilrValues = new Dictionary<string, IEnumerable<PeriodisedValue>>
            {
                {
                    "ST01", new List<PeriodisedValue>
                    {
                        new PeriodisedValue("ConRef1", "ST01", "StartEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 0m, 1m, 1m, 1m, 1m, 1m }),
                        new PeriodisedValue("ConRef1", "ST01", "AchievementEarnings", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m })
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
                            new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", new decimal[] { 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m }),
                            new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
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
            var baseModels = new List<FundingSummaryReportEarnings>
            {
                new FundingSummaryReportEarnings { Year = 2020 },
                new FundingSummaryReportEarnings { Year = 2019 },
                new FundingSummaryReportEarnings { Year = 2018 }
            };

            var esfValues = EsfValuesDictionary();
            var ilrValues = IlrValuesDictionary();

            var models = NewBuilder().PopulateReportData(conRefNumber, HeaderDictionary(), baseModels, esfValues, ilrValues);
            var expectedModels = new List<FundingSummaryReportEarnings>
            {
                new FundingSummaryReportEarnings
                {
                    Year = 2020,
                    YearTotal = 27M,
                    CumulativeYearTotal = 48M,
                    PreviousYearCumulativeTotal = 21m,
                    MonthlyTotals = new PeriodisedReportValue(" Total (£)", new decimal[] { 3m, 3m, 3m, 0m, 0m, 3m, 2m, 3m, 2m, 3m, 2m, 3m }),
                    CumulativeMonthlyTotals = new PeriodisedReportValue(" Cumulative (£)",  new decimal[] { 24m, 27m, 30m, 30m, 30m, 33m, 35m, 38m, 40m, 43m, 45m, 48m }),
                    DeliverableCategories = new List<IDeliverableCategory>
                    {
                        new DeliverableCategory("Total Learner Assessment and Plan (£)")
                        {
                            GroupHeader = Year2020GroupHeader("Learner Assessment and Plan"),
                            DeliverableSubCategories = new List<IDeliverableSubCategory>
                            {
                                new DeliverableSubCategory("Default Category should not render", false)
                                {
                                    ReportValues = new List<IPeriodisedReportValue>
                                    {
                                        new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)",  new decimal[] { 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m }),
                                        new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)",  new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
                                    }
                                }
                            }
                        },
                        new DeliverableCategory("Total Regulated Learning (£)")
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
                        new DeliverableCategory("Total Non Regulated Activity (£)")
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
                        new DeliverableCategory("Total Community Grant (£)")
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
                        new DeliverableCategory("Total Specification Defined (£)")
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
                        new DeliverableCategory("Total Progression and Sustained Progression (£)")
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
                    }
                },
                new FundingSummaryReportEarnings
                {
                    Year = 2019,
                    YearTotal = 19M,
                    CumulativeYearTotal = 21M,
                    PreviousYearCumulativeTotal = 2m,
                    MonthlyTotals = new PeriodisedReportValue(" Total (£)", new decimal[] { 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m }),
                    CumulativeMonthlyTotals = new PeriodisedReportValue(" Cumulative (£)", new decimal[] { 4m, 6m, 8m, 8m, 8m, 10m, 11m, 13m, 15m, 17m, 19m, 21m }),
                    DeliverableCategories = new List<IDeliverableCategory>
                    {
                        new DeliverableCategory("Total Learner Assessment and Plan (£)")
                        {
                            GroupHeader = Year2019GroupHeader("Learner Assessment and Plan"),
                            DeliverableSubCategories = new List<IDeliverableSubCategory>
                            {
                                new DeliverableSubCategory("Default Category should not render", false)
                                {
                                    ReportValues = new List<IPeriodisedReportValue>
                                    {
                                        new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", new decimal[] { 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m }),
                                        new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", new decimal[] { 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m })
                                    }
                                }
                            }
                        },
                        new DeliverableCategory("Total Regulated Learning (£)")
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
                        new DeliverableCategory("Total Non Regulated Activity (£)")
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
                        new DeliverableCategory("Total Community Grant (£)")
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
                        new DeliverableCategory("Total Specification Defined (£)")
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
                        new DeliverableCategory("Total Progression and Sustained Progression (£)")
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
                    }
                },
                new FundingSummaryReportEarnings
                {
                    Year = 2018,
                    YearTotal = 2M,
                    CumulativeYearTotal = 2M,
                    MonthlyTotals = new PeriodisedReportValue(" Total (£)", new decimal[] { 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 1m, 0m, 1m }),
                    CumulativeMonthlyTotals = new PeriodisedReportValue(" Cumulative (£)", new decimal[] { 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 1m, 1m, 2m }),
                    DeliverableCategories = new List<IDeliverableCategory>
                    {
                        new DeliverableCategory("Total Learner Assessment and Plan (£)")
                        {
                            GroupHeader = Year2018GroupHeader("Learner Assessment and Plan"),
                            DeliverableSubCategories = new List<IDeliverableSubCategory>
                            {
                                new DeliverableSubCategory("Default Category should not render", false)
                                {
                                    ReportValues = new List<IPeriodisedReportValue>
                                    {
                                        new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", new decimal[] { 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m }),
                                        new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", new decimal[] { 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m })
                                    }
                                }
                            }
                        },
                        new DeliverableCategory("Total Regulated Learning (£)")
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
                        new DeliverableCategory("Total Non Regulated Activity (£)")
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
                        new DeliverableCategory("Total Community Grant (£)")
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
                        new DeliverableCategory("Total Specification Defined (£)")
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
                        new DeliverableCategory("Total Progression and Sustained Progression (£)")
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
                }
            };

            models.Should().BeEquivalentTo(expectedModels);
        }

        [Fact]
        public void PopulateReportData_CheckYearlyTotals()
        {
            string conRefNumber = null;
            var baseModels = new List<FundingSummaryReportEarnings>
            {
                new FundingSummaryReportEarnings { Year = 2020 },
                new FundingSummaryReportEarnings { Year = 2019 },
                new FundingSummaryReportEarnings { Year = 2018 }
            };

            var esfValues = EsfValuesDictionary();
            var ilrValues = IlrValuesDictionary();

            var models = NewBuilder().PopulateReportData(conRefNumber, HeaderDictionary(), baseModels, esfValues, ilrValues);

            models.Where(x => x.Year == 2018).FirstOrDefault().YearTotal.Should().Be(2);
            models.Where(x => x.Year == 2018).FirstOrDefault().CumulativeYearTotal.Should().Be(2);
            models.Where(x => x.Year == 2018).FirstOrDefault().PreviousYearCumulativeTotal.Should().BeNull();

            models.Where(x => x.Year == 2019).FirstOrDefault().YearTotal.Should().Be(19);
            models.Where(x => x.Year == 2019).FirstOrDefault().CumulativeYearTotal.Should().Be(21);
            models.Where(x => x.Year == 2019).FirstOrDefault().PreviousYearCumulativeTotal.Should().Be(2);

            models.Where(x => x.Year == 2020).FirstOrDefault().YearTotal.Should().Be(27);
            models.Where(x => x.Year == 2020).FirstOrDefault().CumulativeYearTotal.Should().Be(48);
            models.Where(x => x.Year == 2020).FirstOrDefault().PreviousYearCumulativeTotal.Should().Be(21);
        }

        [Fact]
        public void PopulateReportHeader()
        {
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            dateTimeProviderMock.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns<DateTime>(x => x);

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

            NewBuilder(dateTimeProviderMock.Object).PopulateReportHeader(esfFile, ilrFileDetails, 12345678, "OrgName", "ConRef", 2020, 2018, yearToAcademicYearDictionary)
                .Should().BeEquivalentTo(expectedHeader);
        }

        [Fact]
        public void PopulateReportHeader_1920()
        {
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            dateTimeProviderMock.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns<DateTime>(x => x);

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

            NewBuilder(dateTimeProviderMock.Object).PopulateReportHeader(esfFile, ilrFileDetails, 12345678, "OrgName", "ConRef", 2019, 2018, yearToAcademicYearDictionary)
                .Should().BeEquivalentTo(expectedHeader);
        }

        [Fact]
        public void PopulateReportHeader_NoPreviousIlr()
        {
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            dateTimeProviderMock.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns<DateTime>(x => x);

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

            NewBuilder(dateTimeProviderMock.Object).PopulateReportHeader(esfFile, ilrFileDetails, 12345678, "OrgName", "ConRef", 2020, 2018, yearToAcademicYearDictionary)
                .Should().BeEquivalentTo(expectedHeader);
        }

        [Fact]
        public void PopulateReportHeader_NoEsf()
        {
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();

            dateTimeProviderMock.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns<DateTime>(x => x);

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

            NewBuilder(dateTimeProviderMock.Object).PopulateReportHeader(null, ilrFileDetails, 12345678, "OrgName", "Not Applicable", 2020, 2018, yearToAcademicYearDictionary)
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
        public void PeriodiseIlr_EmptyILRData()
        {
            var conRefNumbers = new List<string> { "ConRef1", "ConRef2" };
            var fm70Data = new List<FM70PeriodisedValues>();

            var result = NewBuilder().PeriodiseIlr(conRefNumbers, fm70Data);

            result.Should().ContainKey("Not Applicable");
            result.Values.Should().HaveCount(1);
        }

        [Fact]
        public void PeriodiseIlr_NoValidContract()
        {
            var conRefNumbers = new List<string> { "Not Applicable" };
            var fm70Data = new List<FM70PeriodisedValues>();

            var result = NewBuilder().PeriodiseIlr(conRefNumbers, fm70Data);

            result.Should().ContainKey("Not Applicable");
            result.Values.Should().HaveCount(1);
        }

        [Fact]
        public void PeriodiseIlr_NoContractMatch()
        {
            var conRefNumbers = new List<string> { "ConRef1", "ConRef2" };
            var fm70Data = new List<FM70PeriodisedValues>
            {
                new FM70PeriodisedValues
                {
                    AimSeqNumber = 1,
                    ConRefNumber = "ConRef3",
                    DeliverableCode = "Code3"
                },
                new FM70PeriodisedValues
                {
                    AimSeqNumber = 1,
                    ConRefNumber = "ConRef4",
                    DeliverableCode = "Code3"
                }
            };

            var result = NewBuilder().PeriodiseIlr(conRefNumbers, fm70Data);

            result.Should().ContainKey("Not Applicable");
            result.Values.Should().HaveCount(1);
        }

        [Fact]
        public void PeriodiseIlr_ContractMatch()
        {
            var conRefNumbers = new List<string> { "ConRef1", "ConRef2" };
            var fm70Data = new List<FM70PeriodisedValues>
            {
                new FM70PeriodisedValues
                {
                    AimSeqNumber = 1,
                    ConRefNumber = "ConRef1",
                    DeliverableCode = "Code3"
                },
                new FM70PeriodisedValues
                {
                    AimSeqNumber = 1,
                    ConRefNumber = "ConRef2",
                    DeliverableCode = "Code3"
                }
            };

            var result = NewBuilder().PeriodiseIlr(conRefNumbers, fm70Data);

            result.Should().ContainKeys("ConRef1", "ConRef2");
            result.Values.Should().HaveCount(2);
        }

        [Fact]
        public void PeriodiseEsfSuppData_EmptyEsfData()
        {
            var conRefNumbers = new List<string> { "ConRef1", "ConRef2" };
            var esfData = new Dictionary<string, IEnumerable<SupplementaryDataYearlyModel>>();

            var result = NewBuilder().PeriodiseEsfSuppData(conRefNumbers, esfData);

            result.Should().ContainKeys("ConRef1", "ConRef2");
        }

        [Fact]
        public void PeriodiseEsfSuppData_NoValidContract()
        {
            var conRefNumbers = new List<string> { "Not Applicable" };
            var esfData = new Dictionary<string, IEnumerable<SupplementaryDataYearlyModel>>
            {
                { "ConRef1", new List<SupplementaryDataYearlyModel>() }
            };

            var result = NewBuilder().PeriodiseEsfSuppData(conRefNumbers, esfData);

            result.Should().ContainKey("Not Applicable");
            result.Values.Should().HaveCount(1);
        }

        [Fact]
        public void PeriodiseEsfSuppData_NoContractMatch()
        {
            var emptyResultValue = new Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>();

            var conRefNumbers = new List<string> { "ConRef1", "ConRef2" };
            var esfData = new Dictionary<string, IEnumerable<SupplementaryDataYearlyModel>>
            {
                { "ConRef3", new List<SupplementaryDataYearlyModel>() },
                { "ConRef4", new List<SupplementaryDataYearlyModel>() }
            };

            var result = NewBuilder().PeriodiseEsfSuppData(conRefNumbers, esfData);

            result.Should().ContainKeys("ConRef1", "ConRef2");
            result.Values.Should().HaveCount(2);
            result["ConRef1"].Values.Should().BeEquivalentTo(emptyResultValue);
            result["ConRef2"].Values.Should().BeEquivalentTo(emptyResultValue);
        }

        [Fact]
        public void PeriodiseEsfSuppData_ContractMatch()
        {
            var suppDataList1 = new List<SupplementaryDataYearlyModel>
            {
                new SupplementaryDataYearlyModel
                {
                    FundingYear = 2019,
                    SupplementaryData = new List<SupplementaryDataModel>
                    {
                        new SupplementaryDataModel
                        {
                            DeliverableCode = "Code1"
                        }
                    }
                }
            };

            var suppDataList2 = new List<SupplementaryDataYearlyModel>
            {
                new SupplementaryDataYearlyModel
                {
                    FundingYear = 2020,
                    SupplementaryData = new List<SupplementaryDataModel>
                    {
                        new SupplementaryDataModel
                        {
                            DeliverableCode = "Code1"
                        }
                    }
                }
            };

            var conRefNumbers = new List<string> { "ConRef1", "ConRef2" };
            var esfData = new Dictionary<string, IEnumerable<SupplementaryDataYearlyModel>>
            {
                { "ConRef1", suppDataList1 },
                { "ConRef2", suppDataList2 }
            };

            var result = NewBuilder().PeriodiseEsfSuppData(conRefNumbers, esfData);

            result.Should().ContainKeys("ConRef1", "ConRef2");
            result.Values.Should().HaveCount(2);
            result["ConRef1"].Keys.Contains(2019);
            result["ConRef2"].Keys.Contains(2020);
        }

        [Fact]
        public void PeriodiseEsfSuppData_MixedContractMatch()
        {
            var emptyResultValue = new Dictionary<int, Dictionary<string, IEnumerable<PeriodisedValue>>>();
            var suppDataList = new List<SupplementaryDataYearlyModel>
            {
                new SupplementaryDataYearlyModel
                {
                    FundingYear = 2019,
                    SupplementaryData = new List<SupplementaryDataModel>
                    {
                        new SupplementaryDataModel
                        {
                            DeliverableCode = "Code1"
                        }
                    }
                }
            };

            var conRefNumbers = new List<string> { "ConRef1", "ConRef2" };
            var esfData = new Dictionary<string, IEnumerable<SupplementaryDataYearlyModel>>
            {
                { "ConRef1", suppDataList },
                { "ConRef3", new List<SupplementaryDataYearlyModel>() }
            };

            var result = NewBuilder().PeriodiseEsfSuppData(conRefNumbers, esfData);

            result.Should().ContainKeys("ConRef1", "ConRef2");
            result.Values.Should().HaveCount(2);
            result["ConRef1"].Keys.Contains(2019);
            result["ConRef2"].Values.Should().BeEquivalentTo(emptyResultValue);
        }

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