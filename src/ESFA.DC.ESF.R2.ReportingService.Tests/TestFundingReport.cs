using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.Config;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;
using ESFA.DC.ESF.R2.Interfaces.Strategies;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.ReportingService.Reports;
using ESFA.DC.ESF.R2.ReportingService.Reports.FundingSummary;
using ESFA.DC.ESF.R2.ReportingService.Services;
using ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.CSVRowHelpers;
using ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.Ilr;
using ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.DataService.Models;
using ESFA.DC.Logging.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ReportingService.Tests
{
    public class TestFundingReport
    {
        [Theory]
        [Trait("Category", "Reports")]
        [InlineData("SUPPDATA-10005752-ESF-2108-20180909-090911.CSV", "ESF1819", 1)]
        [InlineData("ILR-10005752-1819-20181004-152148-02.XML", "ILR1819", 0)]
        private async Task TestFundingReportGeneration(string sourceFileName, string collectionName, int expectedZipEntryCount)
        {
            var dateTime = DateTime.UtcNow;
            var filename = $"10005752/1/ESF-2108 ESF (Round 2) Supplementary Data Funding Report {dateTime:yyyyMMdd-HHmmss}";

            Mock<IDateTimeProvider> dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.GetNowUtc()).Returns(dateTime);
            dateTimeProviderMock.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns(dateTime);

            var testStream = new MemoryStream();

            Mock<IFileService> storage = new Mock<IFileService>();
            storage.Setup(x => x.OpenWriteStreamAsync($"{filename}.csv", It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(testStream);

            Mock<IReferenceDataService> referenceDataService = new Mock<IReferenceDataService>();
            referenceDataService.Setup(m => m.GetLarsVersion(It.IsAny<CancellationToken>())).Returns("123456");
            referenceDataService.Setup(m => m.GetOrganisationVersion(It.IsAny<CancellationToken>())).Returns("234567");
            referenceDataService.Setup(m => m.GetPostcodeVersion(It.IsAny<CancellationToken>())).Returns("345678");
            referenceDataService.Setup(m => m.GetProviderName(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns("Foo College");
            referenceDataService.Setup(m =>
                    m.GetDeliverableUnitCosts(It.IsAny<string>(), It.IsAny<IList<string>>()))
                .Returns(new List<DeliverableUnitCost>());

            Mock<IVersionInfo> versionInfo = new Mock<IVersionInfo>();
            versionInfo.Setup(m => m.ServiceReleaseVersion).Returns("1.2.3.4");
            var valueProvider = new ValueProvider();

            var fundigReport = new FundingReport(
                dateTimeProviderMock.Object,
                valueProvider,
                storage.Object,
                referenceDataService.Object);

            SourceFileModel sourceFile = GetEsfSourceFileModel();
            sourceFile.FileName = sourceFileName;

            JobContextModel jobContextModel = new JobContextModel
            {
                UkPrn = 10005752,
                JobId = 1,
                BlobContainerName = "TestContainer",
                CollectionYear = 1819,
                CollectionName = collectionName
            };

            var supplementaryDataWrapper = new SupplementaryDataWrapper()
            {
                SupplementaryDataLooseModels = GetSupplementaryDataLooseModels(),
                SupplementaryDataModels = GetSupplementaryDataModels(),
                ValidErrorModels = new List<ValidationErrorModel>()
            };

            MemoryStream memoryStream = new MemoryStream();
            using (memoryStream)
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Update, true))
                {
                    await fundigReport.GenerateReport(jobContextModel, sourceFile, supplementaryDataWrapper, archive, CancellationToken.None);

                    storage.Verify(s =>
                        s.OpenWriteStreamAsync($"{filename}.csv", It.IsAny<string>(), It.IsAny<CancellationToken>()));
                    archive.Entries.Count.Equals(expectedZipEntryCount);
                }
            }
        }

        private IEnumerable<ILRFileDetails> GetTestFileDetail()
        {
            return new List<ILRFileDetails>
            {
                new ILRFileDetails
                {
                    FileName = "ILR-10005752-1819-20181004-152148-02.xml",
                    LastSubmission = DateTime.Now
                }
            };
        }

        private SourceFileModel GetEsfSourceFileModel()
        {
            return new SourceFileModel
            {
                UKPRN = "10005752",
                JobId = 1,
                ConRefNumber = "ESF-2108",
                FileName = "SUPPDATA-10005752-ESF-2108-20180909-090911.CSV",
                SuppliedDate = DateTime.Now,
                PreparationDate = DateTime.Now.AddDays(-1)
            };
        }

        private List<SupplementaryDataLooseModel> GetSupplementaryDataLooseModels()
        {
            return new List<SupplementaryDataLooseModel>()
            {
                new SupplementaryDataLooseModel()
                {
                    CalendarMonth = "07",
                    CalendarYear = "2019",
                    ConRefNumber = "ABC123",
                    CostType = "CType",
                    DeliverableCode = "DCode",
                    LearnAimRef = "XYZ1234",
                    ProviderSpecifiedReference = "A",
                    Reference = "TestReference",
                    ReferenceType = "TestReferenceType",
                    SupplementaryDataPanelDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    ULN = "12345678",
                    Value = "100"
                }
            };
        }

        private List<SupplementaryDataModel> GetSupplementaryDataModels()
        {
            return new List<SupplementaryDataModel>()
            {
                new SupplementaryDataModel()
                {
                    CalendarMonth = 7,
                    CalendarYear = 2019,
                    ConRefNumber = "ABC123",
                    CostType = "CType",
                    DeliverableCode = "DCode",
                    LearnAimRef = "XYZ1234",
                    ProviderSpecifiedReference = "A",
                    Reference = "TestReference",
                    SupplementaryDataPanelDate = DateTime.Now,
                    ULN = 12345678,
                    Value = 100
                }
            };
        }
    }
}
