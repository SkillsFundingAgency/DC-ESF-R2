using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.CsvService.Interface;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Config;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.ReportingService.Mappers;
using ESFA.DC.ESF.R2.ReportingService.Reports;
using ESFA.DC.ESF.R2.ReportingService.Services;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.DataService.Models;
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

            var supplementaryDataWrapper = new SupplementaryDataWrapper()
            {
                SupplementaryDataLooseModels = GetSupplementaryDataLooseModels(),
                SupplementaryDataModels = GetSupplementaryDataModels(),
                ValidErrorModels = new List<ValidationErrorModel>()
            };

            Mock<IDateTimeProvider> dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.GetNowUtc()).Returns(dateTime);
            dateTimeProviderMock.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns(dateTime);

            var testStream = new MemoryStream();

            var csvServiceMock = new Mock<ICsvFileService>();
            csvServiceMock.Setup(x => x.WriteAsync<FundingReportModel, FundingReportMapper>(It.IsAny<List<FundingReportModel>>(), $"{filename}.csv", It.IsAny<string>(), It.IsAny<CancellationToken>(), null, null))
                .Returns(Task.CompletedTask);

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
                Mock.Of<IFileService>(),
                csvServiceMock.Object,
                referenceDataService.Object);

            SourceFileModel sourceFile = GetEsfSourceFileModel();
            sourceFile.FileName = sourceFileName;

            var esfJobContextMock = new Mock<IEsfJobContext>();
            esfJobContextMock.Setup(x => x.UkPrn).Returns(10005752);
            esfJobContextMock.Setup(x => x.JobId).Returns(1);
            esfJobContextMock.Setup(x => x.BlobContainerName).Returns("TestContainer");
            esfJobContextMock.Setup(x => x.CollectionYear).Returns(1819);
            esfJobContextMock.Setup(x => x.CollectionName).Returns(collectionName);

            await fundigReport.GenerateReport(esfJobContextMock.Object, sourceFile, supplementaryDataWrapper, CancellationToken.None);

            csvServiceMock.VerifyAll();
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
