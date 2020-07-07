using System;
using System.Collections.Generic;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Config;
using ESFA.DC.ESF.R2.Interfaces.Enum;
using ESFA.DC.ESF.R2.Interfaces.ReferenceData;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model;
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

            var ilrFile1 = new Mock<IIlrFile>();
            ilrFile1.Setup(x => x.FileName).Returns("ILR-12345678-20200801-090000.xml");
            ilrFile1.Setup(x => x.SubmittedDateTime).Returns(new DateTime(2020, 8, 1));
            ilrFile1.Setup(x => x.FilePrepDate).Returns(new DateTime(2020, 8, 1));

            var ilrFiles = new Dictionary<CollectionYear, IIlrFile>
            {
                {
                    CollectionYear.Year2021,  ilrFile1.Object
                }
            };

            var esfSuppDataFile = new Mock<IEsfSuppDataFile>();
            esfSuppDataFile.Setup(x => x.SubmittedDateTime).Returns(new DateTime(2020, 4, 1));
            esfSuppDataFile.Setup(x => x.FileName).Returns("ESF-SUPPDATA-1");

            NewBuilder().BuildHeader(12345678, "ConRefNumber1", "Organisation", esfSuppDataFile.Object, ilrFiles).Should().BeEquivalentTo(expectedHeader);
        }

        [Fact]
        public void BuildIlrHeader()
        {
            var ilrFile1 = new Mock<IIlrFile>();
            ilrFile1.Setup(x => x.FileName).Returns("ILR-12345678-20200801-090000.xml");
            ilrFile1.Setup(x => x.SubmittedDateTime).Returns(new DateTime(2020, 8, 1));
            ilrFile1.Setup(x => x.FilePrepDate).Returns(new DateTime(2020, 8, 1));

            var ilrFile2 = new Mock<IIlrFile>();
            ilrFile2.Setup(x => x.FileName).Returns("ILR-12345678-20190730-090000.xml");
            ilrFile2.Setup(x => x.SubmittedDateTime).Returns(new DateTime(2019, 7, 30));
            ilrFile2.Setup(x => x.FilePrepDate).Returns(new DateTime(2019, 7, 30));

            var ilrFile3 = new Mock<IIlrFile>();
            ilrFile3.Setup(x => x.FileName).Returns("ILR-12345678-20180730-090000.xml");
            ilrFile3.Setup(x => x.SubmittedDateTime).Returns(new DateTime(2018, 7, 30));
            ilrFile3.Setup(x => x.FilePrepDate).Returns(new DateTime(2018, 7, 30));

            var ilrFiles = new Dictionary<CollectionYear, IIlrFile>
            {
                { CollectionYear.Year2021,  ilrFile1.Object },
                { CollectionYear.Year1920,  ilrFile2.Object },
                { CollectionYear.Year1819,  ilrFile3.Object },
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
        public void Build()
        {
            var ilrFile1 = new Mock<IIlrFile>();
            ilrFile1.Setup(x => x.FileName).Returns("ILR-12345678-20200801-090000.xml");
            ilrFile1.Setup(x => x.SubmittedDateTime).Returns(new DateTime(2020, 8, 1));
            ilrFile1.Setup(x => x.FilePrepDate).Returns(new DateTime(2020, 8, 1));

            var ilrFiles = new Dictionary<CollectionYear, IIlrFile>
            {
                {
                    CollectionYear.Year2021,  ilrFile1.Object
                }
            };

            var esfFile1 = new Mock<IEsfSuppDataFile>();
            esfFile1.Setup(x => x.FileName).Returns("ESF-SUPPDATA-1");
            esfFile1.Setup(x => x.SubmittedDateTime).Returns(new DateTime(2020, 8, 1));

            var esfFile2 = new Mock<IEsfSuppDataFile>();
            esfFile2.Setup(x => x.FileName).Returns("ESF-SUPPDATA-2");
            esfFile2.Setup(x => x.SubmittedDateTime).Returns(new DateTime(2020, 8, 1));

            var esfFiles = new Dictionary<string, IEsfSuppDataFile>
            {
                {
                   "ConRef1", esfFile1.Object
                },
                {
                    "ConRef2",  esfFile2.Object
                }
            };

            var jobContext = new Mock<IEsfJobContext>();
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            var versionInfo = new Mock<IVersionInfo>();
            var referenceDataVersions = new Mock<IReferenceDataVersions>();
            var referenceDataRoot = new Mock<IReferenceDataRoot>();
            var organisationsReferenceData = new Mock<IOrganisationReferenceData>();

            jobContext.Setup(x => x.UkPrn).Returns(12345678);
            dateTimeProvider.Setup(x => x.GetNowUtc()).Returns(new DateTime(2020, 8, 1));
            dateTimeProvider.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns(new DateTime(2020, 8, 1, 9, 0, 0));
            versionInfo.Setup(x => x.ServiceReleaseVersion).Returns("1.0.0");
            referenceDataVersions.Setup(x => x.LarsVersion).Returns("Version 1");
            referenceDataVersions.Setup(x => x.PostcodeVersion).Returns("Version 2");
            referenceDataVersions.Setup(x => x.OrganisationVersion).Returns("Version 3");

            organisationsReferenceData.Setup(x => x.Ukprn).Returns(12345678);
            organisationsReferenceData.Setup(x => x.Name).Returns("Provider Name");
            organisationsReferenceData.Setup(x => x.ConRefNumbers).Returns(new List<string> { "ConRef1", "ConRef2" });

            referenceDataRoot.Setup(x => x.ReferenceDataVersions).Returns(referenceDataVersions.Object);
            referenceDataRoot.Setup(x => x.IlrFileForCollectionYear).Returns(ilrFiles);
            referenceDataRoot.Setup(x => x.EsfSuppDataFileForConRefNumbers).Returns(esfFiles);
            referenceDataRoot.Setup(x => x.OrganisationReferenceData).Returns(organisationsReferenceData.Object);

            var expectedModel = new Dictionary<string, FundingSummaryReportModel>
            {
                {
                    "ConRef1", new FundingSummaryReportModel
                    {
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
                    }
                },
                {
                    "ConRef2", new FundingSummaryReportModel
                    {
                        Header = new FundingSummaryHeaderModel
                        {
                            Ukprn = 12345678,
                            ProviderName = "Provider Name",
                            ContractReferenceNumber = "ConRef2",
                            LastSupplementaryDataFileUpdate = new DateTime(2020, 8, 1),
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
                }
            };

            NewBuilder(dateTimeProvider.Object, versionInfo.Object, referenceDataRoot.Object).Build(jobContext.Object).Should().BeEquivalentTo(expectedModel);
        }

        private FundingSummaryReportModelBuilder NewBuilder(
            IDateTimeProvider dateTimeProvider = null,
            IVersionInfo versionInfo = null,
            IReferenceDataRoot referenceDataRoot = null)
        {
            return new FundingSummaryReportModelBuilder(dateTimeProvider, versionInfo, referenceDataRoot);
        }
    }
}
