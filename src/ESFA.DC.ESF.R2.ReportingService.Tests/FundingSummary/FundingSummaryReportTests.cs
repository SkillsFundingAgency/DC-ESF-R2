using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Constants;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Interface;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;
using ESFA.DC.ExcelService;
using ESFA.DC.ExcelService.Interface;
using ESFA.DC.FileService;
using ESFA.DC.Logging.Interfaces;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ReportingService.Tests.FundingSummary
{
    public class FundingSummaryReportTests : AbstractFundingSummaryReportTests
    {
        [Fact]
        public async Task GenerateReport()
        {
            var ukprn = 12345678;
            var jobid = 1;
            var reportFolder = "FundingSummary";

            TestFixture(reportFolder, ukprn, jobid);

            var esfJobContext = new Mock<IEsfJobContext>();
            var sourceFile = new Mock<ISourceFileModel>();
            var wrapper = new SupplementaryDataWrapper();
            var cancellationToken = CancellationToken.None;

            var testModels = new List<IFundingSummaryReportTab>
            {
                new FundingSummaryReportTab
                {
                    Title = FundingSummaryReportConstants.BodyTitle,
                    TabName = "ConRef1",
                    Header = TestHeader("ConRef1"),
                    Body = TestBodyModels(),
                    Footer = TestFooter()
                },
                new FundingSummaryReportTab
                {
                    Title = FundingSummaryReportConstants.BodyTitle,
                    TabName = "ConRef2",
                    Header = TestHeader("ConRef2"),
                    Body = TestBodyModels(),
                    Footer = TestFooter()
                }
            };

            esfJobContext.Setup(x => x.UkPrn).Returns(ukprn);
            esfJobContext.Setup(x => x.JobId).Returns(jobid);
            esfJobContext.Setup(x => x.StartCollectionYearAbbreviation).Returns("21");
            esfJobContext.Setup(x => x.ReturnPeriod).Returns("R01");
            esfJobContext.Setup(x => x.CurrentPeriod).Returns(1);
            esfJobContext.Setup(x => x.BlobContainerName).Returns(reportFolder);

            var modelBuilder = new Mock<IFundingSummaryReportModelBuilder>();
            var renderService = new FundingSummaryReportRenderService();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var fileService = new FileSystemFileService();
            var excelFileService = new ExcelFileService(fileService);

            modelBuilder.Setup(x => x.Build(esfJobContext.Object, cancellationToken)).ReturnsAsync(testModels);

            await NewReport(modelBuilder.Object, renderService, dateTimeProvider.Object, excelFileService)
                .GenerateReport(esfJobContext.Object, sourceFile.Object, wrapper, cancellationToken);
        }

        private void TestFixture(string reportFolder, int ukprn, int jobid)
        {
            var directory = new DirectoryInfo(string.Concat(reportFolder, @"\", ukprn, @"\", jobid));
            if (!directory.Exists)
            {
                directory.Create();
            }
        }

        private FundingSummaryReportFooterModel TestFooter()
        {
            return new FundingSummaryReportFooterModel
            {
                ApplicationVersion = "1.0",
                LarsData = "1.0",
                OrganisationData = "1.0",
                PostcodeData = "1.0",
                ReportGeneratedAt = "1.0"
            };
        }

        private FundingSummaryReportHeaderModel TestHeader(string conRefNumber)
        {
            return new FundingSummaryReportHeaderModel
            {
                Ukprn = "12345678",
                ContractReferenceNumber = conRefNumber,
                ProviderName = "Provider",
                SecurityClassification = "OFFICIAL - SENSITIVE",
                LastSupplementaryDataFileUpdate = "01/08/2020 09:00",
                SupplementaryDataFile = "SUPPDATA_01",
                IlrFileDetails = new List<IlrFileDetail>
                {
                    new IlrFileDetail
                    {
                        Year = 2018,
                        AcademicYear = "2018/19",
                        IlrFile = "Ilr1819.xml",
                        MostRecent = "(most recent closed collection for year)",
                        LastIlrFileUpdate = "01/08/2018 09:00",
                        FilePrepDate = "01/08/2018 09:00",
                    },
                    new IlrFileDetail
                    {
                        Year = 2019,
                        AcademicYear = "2019/20",
                        IlrFile = "Ilr1920.xml",
                        MostRecent = "(most recent closed collection for year)",
                        LastIlrFileUpdate = "01/08/2019 09:00",
                        FilePrepDate = "01/08/2019 09:00",
                    },
                    new IlrFileDetail
                    {
                        Year = 2020,
                        AcademicYear = "2020/21",
                        IlrFile = "Ilr2021.xml",
                        LastIlrFileUpdate = "01/08/2020 09:00",
                        FilePrepDate = "01/08/2020 09:00",
                    }
                }
            };
        }

        private ICollection<FundingSummaryReportEarnings> TestBodyModels()
        {
            return new List<FundingSummaryReportEarnings>
            {
                new FundingSummaryReportEarnings
                {
                    Year = 2020,
                    YearTotal = 27M,
                    CumulativeYearTotal = 48M,
                    PreviousYearCumulativeTotal = 21m,
                    MonthlyTotals = new PeriodisedReportValue(" Total (£)", 3m, 3m, 3m, 0m, 0m, 3m, 2m, 3m, 2m, 3m, 2m, 3m),
                    CumulativeMonthlyTotals = new PeriodisedReportValue(" Cumulative (£)", 24m, 27m, 30m, 30m, 30m, 33m, 35m, 38m, 40m, 43m, 45m, 48m),
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
                                        new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                                        new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
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
                    MonthlyTotals = new PeriodisedReportValue(" Total (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                    CumulativeMonthlyTotals = new PeriodisedReportValue(" Cumulative (£)", 4m, 6m, 8m, 8m, 8m, 10m, 11m, 13m, 15m, 17m, 19m, 21m),
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
                                        new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 2m, 2m, 2m, 0m, 0m, 2m, 1m, 2m, 2m, 2m, 2m, 2m),
                                        new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m)
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
                    MonthlyTotals = new PeriodisedReportValue(" Total (£)", 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 1m, 0m, 1m),
                    CumulativeMonthlyTotals = new PeriodisedReportValue(" Cumulative (£)", 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 1m, 1m, 2m),
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
                                        new PeriodisedReportValue("ILR ST01 Learner Assessment and Plan (£)", 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m, 0m),
                                        new PeriodisedReportValue("SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)", 1m, 1m, 1m, 0m, 0m, 1m, 1m, 1m, 0m, 1m, 0m, 1m)
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
        }

        FundingSummaryReport NewReport(
            IFundingSummaryReportModelBuilder modelBuilder,
            IFundingSummaryReportRenderService renderService,
            IDateTimeProvider dateTimeProvider,
            IExcelFileService excelFileService)
        {
            return new FundingSummaryReport(modelBuilder, renderService, dateTimeProvider, excelFileService, Mock.Of<ILogger>());
        }
    }
}
