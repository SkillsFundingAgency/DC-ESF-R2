using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Cells;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;
using ESFA.DC.ESF.R2.Interfaces.Strategies;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.ReportingService.Reports.FundingSummary;
using ESFA.DC.ESF.R2.ReportingService.Services;
using ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.CSVRowHelpers;
using ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.Ilr;
using ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData;
using ESFA.DC.ESF.R2.Service.Config.Interfaces;
using ESFA.DC.ExcelService.Interface;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.DataService.Models;
using ESFA.DC.Logging.Interfaces;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ReportingService.Tests
{
    public class TestFundingSummaryReport
    {
        [Fact]
        [Trait("Category", "Reports")]
        public async Task TestFundingSummaryReportGeneration()
        {
            var dateTime = DateTime.UtcNow;
            var filename = $"10005752/1/ESF Round 2 Funding Summary Report {dateTime:yyyyMMdd-HHmmss}";
            var conRefNumber = "ESF-5234";

            Mock<IDateTimeProvider> dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.GetNowUtc()).Returns(dateTime);
            dateTimeProviderMock.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns(dateTime);

            var testStream = new MemoryStream();

            var esfJobContextMock = new Mock<IEsfJobContext>();
            esfJobContextMock.Setup(x => x.UkPrn).Returns(10005752);
            esfJobContextMock.Setup(x => x.JobId).Returns(1);
            esfJobContextMock.Setup(x => x.BlobContainerName).Returns(string.Empty);
            esfJobContextMock.Setup(x => x.CollectionYear).Returns(1819);

            var excelFileServiceMock = new Mock<IExcelFileService>();
            excelFileServiceMock.Setup(x => x.SaveWorkbookAsync(It.IsAny<Workbook>(), $"{filename}.xlsx", esfJobContextMock.Object.BlobContainerName, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var supplementaryDataService = new Mock<ISupplementaryDataService>();
            supplementaryDataService
                .Setup(s => s.GetSupplementaryData(It.IsAny<int>(), It.IsAny<IEnumerable<SourceFileModel>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Dictionary<string, IEnumerable<SupplementaryDataYearlyModel>>
                {
                    [conRefNumber] = new List<SupplementaryDataYearlyModel>
                    {
                        new SupplementaryDataYearlyModel
                        {
                            FundingYear = 1819,
                            SupplementaryData = new List<SupplementaryDataModel>()
                        }
                    }
                });
            supplementaryDataService
                .Setup(s => s.GetImportFiles(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<SourceFileModel>
                {
                    new SourceFileModel
                    {
                        ConRefNumber = conRefNumber,
                        FileName = filename,
                        UKPRN = "10005752",
                        SourceFileId = 1,
                        PreparationDate = DateTime.Now,
                        SuppliedDate = DateTime.Now,
                        JobId = 1
                    }
                });

            Mock<IReferenceDataService> referenceDataService = new Mock<IReferenceDataService>();
            referenceDataService.Setup(m => m.GetLarsVersion(It.IsAny<CancellationToken>())).ReturnsAsync("123456");
            referenceDataService.Setup(m => m.GetOrganisationVersion(It.IsAny<CancellationToken>())).ReturnsAsync("234567");
            referenceDataService.Setup(m => m.GetPostcodeVersion(It.IsAny<CancellationToken>())).ReturnsAsync("345678");
            referenceDataService.Setup(m => m.GetProviderName(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns("Foo College");
            referenceDataService.Setup(m =>
                    m.GetDeliverableUnitCosts(It.IsAny<string>(), It.IsAny<IList<string>>()))
                .Returns(new List<DeliverableUnitCost>());

            IList<IRowHelper> rowHelpers = GenerateRowHelpersWithStrategies(referenceDataService.Object);

            Mock<IVersionInfo> versionInfo = new Mock<IVersionInfo>();
            versionInfo.Setup(m => m.ServiceReleaseVersion).Returns("1.2.3.4");
            var valueProvider = new ValueProvider();
            var excelStyleProvider = new ExcelStyleProvider();

            IEnumerable<FM70PeriodisedValuesYearly> periodisedValues = new List<FM70PeriodisedValuesYearly>();
            var ilrMock = new Mock<IILRService>();
            ilrMock.Setup(m => m.GetYearlyIlrData(10005752, "ILR1819", 1819, It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(periodisedValues);

            ilrMock.Setup(m => m.GetIlrFileDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetTestFileDetail());

            var logger = new Mock<ILogger>();

            var fundingSummaryReport = new FundingSummaryReport(
                dateTimeProviderMock.Object,
                valueProvider,
                Mock.Of<IFileService>(),
                excelFileServiceMock.Object,
                ilrMock.Object,
                supplementaryDataService.Object,
                rowHelpers,
                referenceDataService.Object,
                excelStyleProvider,
                versionInfo.Object,
                logger.Object);

            SourceFileModel sourceFile = GetEsfSourceFileModel();

            await fundingSummaryReport.GenerateReport(esfJobContextMock.Object, sourceFile, null, CancellationToken.None);

            excelFileServiceMock.VerifyAll();
        }

        [Fact]
        [Trait("Category", "Reports")]
        public async Task Generate_FundingSummaryReport_WithCorrectFormat()
        {
            var dateTime = DateTime.UtcNow;
            var filename = $"10005752/1/ESF Round 2 Funding Summary Report {dateTime:yyyyMMdd-HHmmss}";
            var conRefNumber = "ESF-5234";

            Mock<IDateTimeProvider> dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.GetNowUtc()).Returns(dateTime);
            dateTimeProviderMock.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns(dateTime);

            var esfJobContextMock = new Mock<IEsfJobContext>();
            esfJobContextMock.Setup(x => x.UkPrn).Returns(10005752);
            esfJobContextMock.Setup(x => x.JobId).Returns(1);
            esfJobContextMock.Setup(x => x.BlobContainerName).Returns(string.Empty);
            esfJobContextMock.Setup(x => x.CollectionYear).Returns(2018);

            var testStream = new MemoryStream();

            var excelFileServiceMock = new Mock<IExcelFileService>();
            excelFileServiceMock.Setup(x => x.SaveWorkbookAsync(It.IsAny<Workbook>(), $"{filename}.xlsx", esfJobContextMock.Object.BlobContainerName, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            var supplementaryDataService = new Mock<ISupplementaryDataService>();
            supplementaryDataService
                .Setup(s => s.GetSupplementaryData(It.IsAny<int>(), It.IsAny<IEnumerable<SourceFileModel>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Dictionary<string, IEnumerable<SupplementaryDataYearlyModel>>
                {
                    [conRefNumber] = new List<SupplementaryDataYearlyModel>
                    {
                        new SupplementaryDataYearlyModel
                        {
                            FundingYear = 2018,
                            SupplementaryData = new List<SupplementaryDataModel>()
                                    { new SupplementaryDataModel() { DeliverableCode = "ST01", ConRefNumber = "ESF-5234", CostType = "TestCost", Value = 29.36M, CalendarYear = 2018, CalendarMonth = 9 } }
                        }
                    }
                });
            supplementaryDataService
                .Setup(s => s.GetImportFiles(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<SourceFileModel>
                {
                    new SourceFileModel
                    {
                        ConRefNumber = conRefNumber,
                        FileName = filename,
                        UKPRN = "10005752",
                        SourceFileId = 1,
                        PreparationDate = DateTime.Now,
                        SuppliedDate = DateTime.Now,
                        JobId = 1
                    }
                });

            var deliverableCost = new List<DeliverableUnitCost>()
            {
                new DeliverableUnitCost() { ConRefNum = "ESF-5234", UkPrn = 10005752, DeliverableCode = "ST01", UnitCost = 55.56M }
            };

            Mock<IReferenceDataService> referenceDataService = new Mock<IReferenceDataService>();
            referenceDataService.Setup(m => m.GetLarsVersion(It.IsAny<CancellationToken>())).ReturnsAsync("Version 3.0.0 : 08 December 2018 08:21:34:970");
            referenceDataService.Setup(m => m.GetOrganisationVersion(It.IsAny<CancellationToken>())).ReturnsAsync("Version 1.0.0 : 04 April 2019 09:56:25:653");
            referenceDataService.Setup(m => m.GetPostcodeVersion(It.IsAny<CancellationToken>())).ReturnsAsync("Version 2.0.0 : 09 March 2019 08:32:09:230");
            referenceDataService.Setup(m => m.GetProviderName(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns("Test Data College");
            referenceDataService.Setup(m =>
                    m.GetDeliverableUnitCosts(It.IsAny<string>(), It.IsAny<IList<string>>()))
                .Returns(deliverableCost);

            IList<IRowHelper> rowHelpers = GenerateRowHelpersWithStrategies(referenceDataService.Object);

            Mock<IVersionInfo> versionInfo = new Mock<IVersionInfo>();
            versionInfo.Setup(m => m.ServiceReleaseVersion).Returns("1.2.3.4");
            var valueProvider = new ValueProvider();
            var excelStyleProvider = new ExcelStyleProvider();

            FM70PeriodisedValues periodiseData = new FM70PeriodisedValues()
            {
                FundingYear = 2018,
                AimSeqNumber = 1,
                Period1 = 25.29M,
                UKPRN = 10005752,
                ConRefNumber = "ESF-5234",
                DeliverableCode = "ILR"
            };

            FM70PeriodisedValuesYearly fM70 = new FM70PeriodisedValuesYearly()
            {
                FundingYear = 2018,
                Fm70PeriodisedValues = new List<FM70PeriodisedValues> { periodiseData }
            };

            var periodisedValues = new List<FM70PeriodisedValuesYearly>
            {
                fM70
            };

            var ilrMock = new Mock<IILRService>();
            ilrMock.Setup(m => m.GetYearlyIlrData(10005752, "ILR1819", 2018, It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(periodisedValues);

            ilrMock.Setup(m => m.GetIlrFileDetails(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetTestFileDetail());

            var logger = new Mock<ILogger>();

            var fundingSummaryReport = new FundingSummaryReport(
                dateTimeProviderMock.Object,
                valueProvider,
                Mock.Of<IFileService>(),
                excelFileServiceMock.Object,
                ilrMock.Object,
                supplementaryDataService.Object,
                rowHelpers,
                referenceDataService.Object,
                excelStyleProvider,
                versionInfo.Object,
                logger.Object);

            SourceFileModel sourceFile = GetEsfSourceFileModel();

            await fundingSummaryReport.GenerateReport(esfJobContextMock.Object, sourceFile, null, CancellationToken.None);

            excelFileServiceMock.VerifyAll();

            // uncomment following line to generate the file
            // File.WriteAllBytes($"{filename}.xlsx", testStream.GetBuffer());
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

        private IList<IRowHelper> GenerateRowHelpersWithStrategies(IReferenceDataService referenceDataService)
        {
            return new List<IRowHelper>
            {
                new SpacerRowHelper(),
                new TitleRowHelper(),
                new TotalRowHelper(),
                new CumulativeRowHelper(),
                new MainTitleRowHelper(),
                new DataRowHelper(
                    new List<ISupplementaryDataStrategy>
                    {
                        new CG01CommunityGrantPayment(referenceDataService),
                        new CG02CommunityGrantManagementCost(referenceDataService),
                        new NR01NonRegulatedActivityAuthorisedClaims(referenceDataService),
                        new PG01ProgressionPaidEmploymentAdjustments(referenceDataService),
                        new PG03ProgressionEducationAdjustments(referenceDataService),
                        new PG04ProgressionApprenticeshipAdjustments(referenceDataService),
                        new PG05ProgressionTraineeshipAdjustments(referenceDataService),
                        new RQ01RegulatedLearningAuthorisedClaims(referenceDataService),
                        new SD01FCSDeliverableDescription(referenceDataService),
                        new SD02FCSDeliverableDescription(referenceDataService),
                        new ST01LearnerAssessmentAndPlanAdjustments(referenceDataService)
                    },
                    new List<IILRDataStrategy>
                    {
                        new NR01NonRegulatedActivityAchievementEarnings(),
                        new NR01NonRegulatedActivityStartFunding(),
                        new PG01ProgressionPaidEmployment(),
                        new PG03ProgressionEducation(),
                        new PG04ProgressionApprenticeship(),
                        new PG05ProgressionTraineeship(),
                        new RQ01RegulatedLearningAchievementEarnings(),
                        new RQ01RegulatedLearningStartFunding(),
                        new ST01LearnerAssessmentAndPlan()
                    })
            };
        }
    }
}
