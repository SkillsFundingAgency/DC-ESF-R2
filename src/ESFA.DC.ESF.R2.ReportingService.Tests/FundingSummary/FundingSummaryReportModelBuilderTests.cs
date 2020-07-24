using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Config;
using ESFA.DC.ESF.R2.Interfaces.ReferenceData;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ReportingService.Constants;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model;
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
            var year = 2020;

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
                GroupHeader = new GroupHeader(
                    "Specification Defined",
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
                    "July 2021"),
                EsfSD01 = new PeriodisedReportValue("SUPPDATA SD01 Progression Within Work (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                EsfSD02 = new PeriodisedReportValue("SUPPDATA SD02 LEP Agreed Delivery Plan (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
            };

            NewBuilder().BuildSpecificationDefined(year, esfValues).Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void BuildCommunityGrants()
        {
            var year = 2020;

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
                GroupHeader = new GroupHeader(
                    "Community Grant",
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
                    "July 2021"),
                EsfCG01 = new PeriodisedReportValue("SUPPDATA CG01 Community Grant Payment (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                EsfCG02 = new PeriodisedReportValue("SUPPDATA CG02 Community Grant Management Cost (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
            };

            NewBuilder().BuildCommunityGrants(year, esfValues).Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void BuildNonRegulatedLearning()
        {
            var year = 2020;

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
                GroupHeader = new GroupHeader(
                    "Non Regulated Learning",
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
                    "July 2021"),
                IlrNR01StartFunding = new PeriodisedReportValue("ILR NR01 Non Regulated Learning - Start Funding (£)", 1m, 1m, 1m, 0m, 0m, 1m, 0m, 1m, 1m, 1m, 1m, 1m),
                IlrNR01AchFunding = new PeriodisedReportValue("ILR NR01 Non Regulated Learning - Achievement Funding (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m),
                EsfNR01AuthClaims = new PeriodisedReportValue("SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
            };

            NewBuilder().BuildNonRegulatedLearning(year, esfValues, ilrValues).Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void BuildRegulatedLearning()
        {
            var year = 2020;

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
                GroupHeader = new GroupHeader(
                    "Regulated Learning",
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
                    "July 2021"),
                IlrRQ01StartFunding = new PeriodisedReportValue("ILR RQ01 Regulated Learning - Start Funding (£)", 1m, 1m, 1m, 0m, 0m, 1m, 0m, 1m, 1m, 1m, 1m, 1m),
                IlrRQ01AchFunding = new PeriodisedReportValue("ILR RQ01 Regulated Learning - Achievement Funding (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 1m, 1m, 1m, 1m),
                EsfRQ01AuthClaims = new PeriodisedReportValue("SUPPDATA RQ01 Regulated Learning Authorised Claims (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
            };

            NewBuilder().BuildRegulatedLearning(year, esfValues, ilrValues).Should().BeEquivalentTo(expectedValue);
        }

        public void BuildProgressions()
        {
            var year = 2020;

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
                GroupHeader = new GroupHeader(
                    "Progression and Sustained Progression",
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
                    "July 2021"),
                IlrPG01 = new PeriodisedReportValue("ILR PG01 Progression Paid Employment(£)", 2m, 2m, 2m, 1m, 0m, 1m, 1m, 2m, 2m, 2m, 2m, 2m),
                EsfPG01 = new PeriodisedReportValue("SUPPDATA PG01 Progression Paid Employment Adjustments (£)", 2m, 2m, 2m, 0m, 0m, 2m, 2m, 2m, 0m, 2m, 0m, 2m),
                IlrPG03 = new PeriodisedReportValue("ILR PG03 Progression Education (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m),
                EsfPG03 = new PeriodisedReportValue("SUPPDATA PG03 Progression Education Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m),
                IlrPG04 = new PeriodisedReportValue("ILR PG04 Progression Apprenticeship (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m),
                EsfPG04 = new PeriodisedReportValue("SUPPDATA PG04 Progression Apprenticeship Adjustments(£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m),
                IlrPG05 = new PeriodisedReportValue("ILR PG05 Progression Traineeship (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m),
                EsfPG05 = new PeriodisedReportValue("SUPPDATA PG05 Progression Traineeship Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
            };

            NewBuilder().BuildProgressions(year, esfValues, ilrValues).Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void BuildLearnerAssessmentPlans()
        {
            var year = 2020;

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
                GroupHeader = new GroupHeader(
                    "Learner Assessment and Plan",
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
                    "July 2021"),
                IlrST01 = new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
            };

            NewBuilder().BuildLearnerAssessmentPlans(year, esfValues, ilrValues).Should().BeEquivalentTo(expectedValue);
        }

        [Fact]
        public void PopulateReportData()
        {
            var esfValues = EsfValuesDictionary();
            var ilrValues = IlrValuesDictionary();

            var models = NewBuilder().PopulateReportData(esfValues, ilrValues);
            var expectedModels = new List<FundingSummaryModel>
            {
                new FundingSummaryModel
                {
                    Year = 2020,
                    LearnerAssessmentPlans = new LearnerAssessmentPlan
                    {
                        GroupHeader = new GroupHeader(
                            "Learner Assessment and Plan",
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
                            "July 2021"),
                        IlrST01 = new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                        EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                    }
                },
                new FundingSummaryModel
                {
                    Year = 2019,
                    LearnerAssessmentPlans = new LearnerAssessmentPlan
                    {
                        GroupHeader = new GroupHeader(
                            "Learner Assessment and Plan",
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
                            "July 2021"),
                        IlrST01 = new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                        EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m)
                    }
                },
                new FundingSummaryModel
                {
                    Year = 20118,
                    LearnerAssessmentPlans = new LearnerAssessmentPlan
                    {
                        GroupHeader = new GroupHeader(
                            "Learner Assessment and Plan",
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
                            "July 2021"),
                        IlrST01 = new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m),
                        EsfST01 = new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
                    }
                }
            };

            expectedModels.Should().BeEquivalentTo(models);
        }

        [Fact]
        public async Task Build()
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

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(x => x.GetNowUtc()).Returns(reportDateTime);
            dateTimeProvider.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns(reportDateTime);

            var versionInfo = new Mock<IVersionInfo>();
            versionInfo.Setup(x => x.ServiceReleaseVersion).Returns("1.0.0");

            var referenceDataVersions = new Mock<IReferenceDataVersions>();
            var orgRefData = new Mock<IOrganisationReferenceData>();
            var suppData = new Mock<IDictionary<string, IEnumerable<SupplementaryDataYearlyModel>>>();
            var ilrFileDetails = new Mock<IEnumerable<ILRFileDetails>>();
            var ilrPeriodisedValues = new Mock<IEnumerable<FM70PeriodisedValuesYearly>>();

            var dataProvider = new Mock<IFundingSummaryReportDataProvider>();
            dataProvider.Setup(x => x.ProvideReferenceDataVersionsAsync(cancellationToken)).ReturnsAsync(referenceDataVersions.Object);
            dataProvider.Setup(x => x.ProvideOrganisationReferenceDataAsync(ukprn, cancellationToken)).ReturnsAsync(orgRefData.Object);
            dataProvider.Setup(x => x.GetImportFilesAsync(ukprn, cancellationToken)).ReturnsAsync(esfSourceFiles);
            dataProvider.Setup(x => x.GetSupplementaryDataAsync(collectionYear, esfSourceFiles, cancellationToken)).ReturnsAsync(suppData.Object);
            dataProvider.Setup(x => x.GetIlrFileDetailsAsync(ukprn, collectionYear, cancellationToken)).ReturnsAsync(ilrFileDetails.Object);
            dataProvider.Setup(x => x.GetYearlyIlrDataAsync(ukprn, collectionName, collectionYear, returnPeriod, cancellationToken)).ReturnsAsync(ilrPeriodisedValues.Object);

            var expectedTabs = new List<FundingSummaryReportTab>();

            var reportTabs = await NewBuilder(dateTimeProvider.Object, dataProvider.Object, versionInfo.Object).Build(esfJobContext.Object, cancellationToken);
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

        private FundingSummaryReportModelBuilder NewBuilder(
            IDateTimeProvider dateTimeProvider = null,
            IFundingSummaryReportDataProvider dataProvider = null,
            IVersionInfo versionInfo = null)
        {
            return new FundingSummaryReportModelBuilder(dateTimeProvider, dataProvider, versionInfo, Mock.Of<ILogger>());
        }
    }
}