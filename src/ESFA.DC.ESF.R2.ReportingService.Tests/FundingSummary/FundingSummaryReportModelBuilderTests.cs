using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Config;
using ESFA.DC.ESF.R2.Interfaces.Enum;
using ESFA.DC.ESF.R2.Interfaces.FundingSummary.ESF;
using ESFA.DC.ESF.R2.Interfaces.FundingSummary.ILR;
using ESFA.DC.ESF.R2.Interfaces.ReferenceData;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.ESF;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.ILR;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ReportingService.Tests.FundingSummary
{
    public class FundingSummaryReportModelBuilderTests
    {
        [Fact]
        public void BuildHeader()
        {
            var expectedHeader = new FundingSummaryHeaderModel
            {
                Ukprn = 12345678,
                ProviderName = "Organisation",
                SecurityClassification = "OFFICIAL-SENSITIVE",
                ContractReferenceNumber = "ConRefNumber1",
                SupplementaryDataFile = "ESF-SUPPDATA-1",
                LastSupplementaryDataFileUpdate = new DateTime(2020, 4, 1),
                IlrHeader = new Dictionary<CollectionYear, FundingSummaryIlrHeaderModel>
                {
                    {
                        CollectionYear.Year2021, new FundingSummaryIlrHeaderModel
                        {
                            IlrFileLastUpdated = new DateTime(2020, 8, 1),
                            IlrFilePrepDate = new DateTime(2020, 8, 1),
                            IlrFileName = "ILR-12345678-20200801-090000.xml",
                            CollectionYear = "2020/21"
                        }
                    }
                }
            };

            var ilrData = new IlrFileData
            {
                CollectionYear = CollectionYear.Year2021,
                IlrFile = new IlrFile
                {
                    FileName = "ILR-12345678-20200801-090000.xml",
                    SubmittedDateTime = new DateTime(2020, 8, 1),
                    FilePrepDate = new DateTime(2020, 8, 1)
                }
            };

            var ilrFiles = new Dictionary<CollectionYear, IIlrFileData>
            {
                {
                    CollectionYear.Year2021,  ilrData
                }
            };

            var esfFile = new EsfFile
            {
                SubmittedDateTime = new DateTime(2020, 4, 1),
                FileName = "ESF-SUPPDATA-1"
            };

            NewBuilder().BuildHeader(12345678, "ConRefNumber1", "Organisation", esfFile, ilrFiles).Should().BeEquivalentTo(expectedHeader);
        }

        [Fact]
        public void BuildIlrHeader()
        {
            var ilrData1 = new IlrFileData
            {
                CollectionYear = CollectionYear.Year2021,
                IlrFile = new IlrFile
                {
                    FileName = "ILR-12345678-20200801-090000.xml",
                    SubmittedDateTime = new DateTime(2020, 8, 1),
                    FilePrepDate = new DateTime(2020, 8, 1)
                }
            };

            var ilrData2 = new IlrFileData
            {
                CollectionYear = CollectionYear.Year1920,
                IlrFile = new IlrFile
                {
                    FileName = "ILR-12345678-20190730-090000.xml",
                    SubmittedDateTime = new DateTime(2019, 7, 30),
                    FilePrepDate = new DateTime(2019, 7, 30)
                }
            };

            var ilrData3 = new IlrFileData
            {
                CollectionYear = CollectionYear.Year1819,
                IlrFile = new IlrFile
                {
                    FileName = "ILR-12345678-20180730-090000.xml",
                    SubmittedDateTime = new DateTime(2018, 7, 30),
                    FilePrepDate = new DateTime(2018, 7, 30)
                }
            };

            var ilrFiles = new Dictionary<CollectionYear, IIlrFileData>
            {
                { CollectionYear.Year2021,  ilrData1 },
                { CollectionYear.Year1920,  ilrData2 },
                { CollectionYear.Year1819,  ilrData3 }
            };

            var expectedHeader = new Dictionary<CollectionYear, FundingSummaryIlrHeaderModel>
            {
                {
                    CollectionYear.Year2021, new FundingSummaryIlrHeaderModel
                    {
                        IlrFileLastUpdated = new DateTime(2020, 8, 1),
                        IlrFilePrepDate = new DateTime(2020, 8, 1),
                        IlrFileName = "ILR-12345678-20200801-090000.xml",
                        CollectionYear = "2020/21",
                    }
                },
                {
                    CollectionYear.Year1920, new FundingSummaryIlrHeaderModel
                    {
                        IlrFileLastUpdated = new DateTime(2019, 7, 30),
                        IlrFilePrepDate = new DateTime(2019, 7, 30),
                        IlrFileName = "ILR-12345678-20190730-090000.xml",
                        CollectionYear = "2019/20",
                        CollectionClosedMessage = "(most recent closed collection for year)"
                    }
                },
                {
                    CollectionYear.Year1819, new FundingSummaryIlrHeaderModel
                    {
                        IlrFileLastUpdated = new DateTime(2018, 7, 30),
                        IlrFilePrepDate = new DateTime(2018, 7, 30),
                        IlrFileName = "ILR-12345678-20180730-090000.xml",
                        CollectionYear = "2018/19",
                        CollectionClosedMessage = "(most recent closed collection for year)"
                    }
                }
            };

            NewBuilder().BuildIlrHeader(ilrFiles).Should().BeEquivalentTo(expectedHeader);
        }

        [Fact]
        public void BuildFooter()
        {
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var versionInfo = new Mock<IVersionInfo>();
            var referenceDataVersions = new Mock<IReferenceDataVersions>();

            dateTimeProvider.Setup(x => x.GetNowUtc()).Returns(new DateTime(2020, 8, 1));
            dateTimeProvider.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns(new DateTime(2020, 8, 1, 9, 0, 0));
            versionInfo.Setup(x => x.ServiceReleaseVersion).Returns("1.0.0");
            referenceDataVersions.Setup(x => x.LarsVersion).Returns("Version 1");
            referenceDataVersions.Setup(x => x.PostcodeVersion).Returns("Version 2");
            referenceDataVersions.Setup(x => x.OrganisationVersion).Returns("Version 3");

            var expectedFooter = new FundingSummaryFooterModel
            {
                ReportGeneratedAt = "09:00:00 on 01/08/2020",
                ApplicationVersion = "1.0.0",
                LarsData = "Version 1",
                PostcodeData = "Version 2",
                OrganisationData = "Version 3",
            };

            NewBuilder(dateTimeProvider.Object, versionInfo.Object).BuildFooter(referenceDataVersions.Object).Should().BeEquivalentTo(expectedFooter);
        }

        [Fact]
        public async Task Build()
        {
            var cancellationToken = CancellationToken.None;
            var ukprn = 12345678;
            var collectionYear = 2021;

            var ilrData = new IlrFileData
            {
                CollectionYear = CollectionYear.Year2021,
                IlrFile = new IlrFile
                {
                    FileName = "ILR-12345678-20200801-090000.xml",
                    SubmittedDateTime = new DateTime(2020, 8, 1),
                    FilePrepDate = new DateTime(2020, 8, 1)
                }
            };

            var ilrFiles = new Dictionary<CollectionYear, IIlrFileData>
            {
                {
                    CollectionYear.Year2021,  ilrData
                }
            };

            var esfFile1 = new EsfFile
            {
                SubmittedDateTime = new DateTime(2020, 8, 1),
                FileName = "ESF-SUPPDATA-1"
            };

            var esfFile2 = new EsfFile
            {
                SubmittedDateTime = new DateTime(2020, 4, 1),
                FileName = "ESF-SUPPDATA-2"
            };

            var esfFiles = new Dictionary<string, IDictionary<CollectionYear, IEsfFileData>>
            {
                {
                    "ConRef1", new Dictionary<CollectionYear, IEsfFileData>
                    {
                        { CollectionYear.Year2021, new EsfFileData { CollectionYear = CollectionYear.Year2021, EsfFile = esfFile1 } }
                    }
                },
                {
                    "ConRef2", new Dictionary<CollectionYear, IEsfFileData>
                    {
                        { CollectionYear.Year1920, new EsfFileData { CollectionYear = CollectionYear.Year1920, EsfFile = esfFile2 } }
                    }
                }
            };

            var jobContext = new Mock<IEsfJobContext>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var versionInfo = new Mock<IVersionInfo>();
            var referenceDataVersions = new Mock<IReferenceDataVersions>();
            var dataProvider = new Mock<IFundingSummaryReportDataProvider>();
            var organisationsReferenceData = new Mock<IOrganisationReferenceData>();

            jobContext.Setup(x => x.UkPrn).Returns(ukprn);
            jobContext.Setup(x => x.CollectionYear).Returns(collectionYear);
            dateTimeProvider.Setup(x => x.GetNowUtc()).Returns(new DateTime(2020, 8, 1));
            dateTimeProvider.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns(new DateTime(2020, 8, 1, 9, 0, 0));
            versionInfo.Setup(x => x.ServiceReleaseVersion).Returns("1.0.0");
            referenceDataVersions.Setup(x => x.LarsVersion).Returns("Version 1");
            referenceDataVersions.Setup(x => x.PostcodeVersion).Returns("Version 2");
            referenceDataVersions.Setup(x => x.OrganisationVersion).Returns("Version 3");

            organisationsReferenceData.Setup(x => x.Ukprn).Returns(12345678);
            organisationsReferenceData.Setup(x => x.Name).Returns("Provider Name");
            organisationsReferenceData.Setup(x => x.ConRefNumbers).Returns(new List<string> { "ConRef1", "ConRef2" });

            dataProvider.Setup(x => x.ProvideReferenceDataVersionsAsync(cancellationToken)).ReturnsAsync(referenceDataVersions.Object);
            dataProvider.Setup(x => x.ProvideIlrDataAsync(ukprn, collectionYear, cancellationToken)).ReturnsAsync(ilrFiles);
            dataProvider.Setup(x => x.ProvideEsfSuppDataAsync(ukprn, collectionYear, cancellationToken)).ReturnsAsync(esfFiles);
            dataProvider.Setup(x => x.ProvideOrganisationReferenceDataAsync(ukprn, cancellationToken)).ReturnsAsync(organisationsReferenceData.Object);

            var expectedModel = new List<FundingSummaryReportTab>
            {
                new FundingSummaryReportTab
                {
                    TabName = "ConRef1",
                    Header = new FundingSummaryHeaderModel
                    {
                        Ukprn = 12345678,
                        ProviderName = "Provider Name",
                        ContractReferenceNumber = "ConRef1",
                        LastSupplementaryDataFileUpdate = new DateTime(2020, 8, 1),
                        SupplementaryDataFile = "ESF-SUPPDATA-1",
                        SecurityClassification = "OFFICIAL-SENSITIVE",
                        IlrHeader = new Dictionary<CollectionYear, FundingSummaryIlrHeaderModel>
                        {
                            {
                                CollectionYear.Year2021, new FundingSummaryIlrHeaderModel
                                {
                                    IlrFileLastUpdated = new DateTime(2020, 8, 1),
                                    IlrFilePrepDate = new DateTime(2020, 8, 1),
                                    IlrFileName = "ILR-12345678-20200801-090000.xml",
                                    CollectionYear = "2020/21"
                                }
                            }
                        }
                    },
                    Footer = new FundingSummaryFooterModel
                    {
                        ReportGeneratedAt = "09:00:00 on 01/08/2020",
                        ApplicationVersion = "1.0.0",
                        LarsData = "Version 1",
                        PostcodeData = "Version 2",
                        OrganisationData = "Version 3",
                    },
                    Body = new Dictionary<CollectionYear, IFundingCategory>()
                },
                new FundingSummaryReportTab
                {
                    TabName = "ConRef2",
                    Header = new FundingSummaryHeaderModel
                    {
                        Ukprn = 12345678,
                        ProviderName = "Provider Name",
                        ContractReferenceNumber = "ConRef2",
                        LastSupplementaryDataFileUpdate = new DateTime(2020, 4, 1),
                        SupplementaryDataFile = "ESF-SUPPDATA-2",
                        SecurityClassification = "OFFICIAL-SENSITIVE",
                        IlrHeader = new Dictionary<CollectionYear, FundingSummaryIlrHeaderModel>
                        {
                            {
                                CollectionYear.Year2021, new FundingSummaryIlrHeaderModel
                                {
                                    IlrFileLastUpdated = new DateTime(2020, 8, 1),
                                    IlrFilePrepDate = new DateTime(2020, 8, 1),
                                    IlrFileName = "ILR-12345678-20200801-090000.xml",
                                    CollectionYear = "2020/21"
                                }
                            }
                        }
                    },
                    Footer = new FundingSummaryFooterModel
                    {
                        ReportGeneratedAt = "09:00:00 on 01/08/2020",
                        ApplicationVersion = "1.0.0",
                        LarsData = "Version 1",
                        PostcodeData = "Version 2",
                        OrganisationData = "Version 3",
                    },
                    Body = new Dictionary<CollectionYear, IFundingCategory>()
                }
            };

            var result = await NewBuilder(dateTimeProvider.Object, versionInfo.Object, dataProvider.Object).Build(jobContext.Object, cancellationToken);
            result.Should().BeEquivalentTo(expectedModel);
        }

        private FundingSummaryReportModelBuilder NewBuilder(
            IDateTimeProvider dateTimeProvider = null,
            IVersionInfo versionInfo = null,
            IFundingSummaryReportDataProvider provider = null)
        {
            return new FundingSummaryReportModelBuilder(dateTimeProvider, versionInfo, provider);
        }
    }
}