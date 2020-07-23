using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.CsvService.Interface;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports;
using ESFA.DC.ESF.R2.ReportingService.Mappers;
using ESFA.DC.ESF.R2.ReportingService.Reports;
using ESFA.DC.ESF.R2.ReportingService.Services;
using ESFA.DC.ESF.R2.ReportingService.Tests.Builders;
using ESFA.DC.FileService.Interface;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ReportingService.Tests
{
    public sealed class TestAimAndDeliverableReport
    {
        [Fact]
        [Trait("Category", "Reports")]
        public async Task TestAimAndDeliverableReportGeneration()
        {
            var dateTime = DateTime.UtcNow;
            var filename = $"10005752/2/ESF Round 2 Aim and Deliverable Report Legacy {dateTime:yyyyMMdd-HHmmss}";

            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.GetNowUtc()).Returns(dateTime);
            dateTimeProviderMock.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns(dateTime);

            var models = AimAndDeliverableBuilder.BuildAimAndDeliverableModels();

            var aimAndDeliverableService1819Mock = new Mock<IAimAndDeliverableService1819>();
            aimAndDeliverableService1819Mock
                .Setup(m => m.GetAimAndDeliverableModel(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(models);

            var aimAndDeliverableService1920Mock = new Mock<IAimAndDeliverableService1920>();
            aimAndDeliverableService1920Mock
                .Setup(m => m.GetAimAndDeliverableModel(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(models);

            var csvServiceMock = new Mock<ICsvFileService>();
            csvServiceMock.Setup(x => x.WriteAsync<AimAndDeliverableModel, AimAndDeliverableMapper>(models, $"{filename}.csv", It.IsAny<string>(), It.IsAny<CancellationToken>(), null, null))
                .Returns(Task.CompletedTask);

            var aimAndDeliverableReport = new AimAndDeliverableReport(
                dateTimeProviderMock.Object,
                csvServiceMock.Object,
                aimAndDeliverableService1819Mock.Object,
                aimAndDeliverableService1920Mock.Object);

            var esfJobContextMock = new Mock<IEsfJobContext>();
            esfJobContextMock.Setup(x => x.UkPrn).Returns(10005752);
            esfJobContextMock.Setup(x => x.JobId).Returns(2);
            esfJobContextMock.Setup(x => x.BlobContainerName).Returns("TestBlob");
            esfJobContextMock.Setup(x => x.CollectionYear).Returns(1819);

            SourceFileModel sourceFile = GetEsfSourceFileModel();

            var reportName = await aimAndDeliverableReport.GenerateReport(esfJobContextMock.Object, sourceFile, null, CancellationToken.None);

            csvServiceMock.VerifyAll();
        }

        private SourceFileModel GetEsfSourceFileModel()
        {
            return new SourceFileModel
            {
                UKPRN = "10005752",
                JobId = 2,
                ConRefNumber = "ESF-5000",
                FileName = "SUPPDATA-10005752-ESF-2108-20180909-090911.CSV",
                SuppliedDate = DateTime.Now,
                PreparationDate = DateTime.Now.AddDays(-1)
            };
        }
    }
}
