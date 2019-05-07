using System;
using System.Collections.Generic;
using System.IO;
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
using ESFA.DC.ESF.R2.ReportingService.Reports.FundingSummary;
using ESFA.DC.ESF.R2.ReportingService.Services;
using ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.CSVRowHelpers;
using ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.Ilr;
using ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.DataService.Models;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ReportingService.Tests
{
    public class TestFundingSummaryReport
    {
        [Fact]
        public async Task TestFundingSummaryReportGeneration()
        {
            var dateTime = DateTime.UtcNow;
            var filename = $"10005752/1/ESF R2 Funding Summary Report {dateTime:yyyyMMdd-HHmmss}";

            Mock<IDateTimeProvider> dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.GetNowUtc()).Returns(dateTime);
            dateTimeProviderMock.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns(dateTime);

            var testStream = new MemoryStream();

            Mock<IFileService> storage = new Mock<IFileService>();
            storage.Setup(x => x.OpenWriteStreamAsync($"{filename}.xlsx", It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(testStream);

            var supplementaryDataService = new Mock<ISupplementaryDataService>();
            supplementaryDataService
                .Setup(s => s.GetSupplementaryData(It.IsAny<IEnumerable<SourceFileModel>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Dictionary<int, IEnumerable<SupplementaryDataYearlyModel>>());
            supplementaryDataService
                .Setup(s => s.GetImportFiles(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<SourceFileModel>());

            Mock<IReferenceDataService> referenceDataService = new Mock<IReferenceDataService>();
            referenceDataService.Setup(m => m.GetLarsVersion(It.IsAny<CancellationToken>())).Returns("123456");
            referenceDataService.Setup(m => m.GetOrganisationVersion(It.IsAny<CancellationToken>())).Returns("234567");
            referenceDataService.Setup(m => m.GetPostcodeVersion(It.IsAny<CancellationToken>())).Returns("345678");
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
            ilrMock.Setup(m => m.GetYearlyIlrData(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(periodisedValues);
            ilrMock.Setup(m => m.GetIlrFileDetails(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetTestFileDetail());

            var fundingSummaryReport = new FundingSummaryReport(
                dateTimeProviderMock.Object,
                valueProvider,
                storage.Object,
                ilrMock.Object,
                supplementaryDataService.Object,
                rowHelpers,
                referenceDataService.Object,
                excelStyleProvider,
                versionInfo.Object);

            SourceFileModel sourceFile = GetEsfSourceFileModel();

            JobContextModel wrapper = new JobContextModel
            {
                UkPrn = 10005752,
                JobId = 1,
                BlobContainerName = string.Empty
            };

            await fundingSummaryReport.GenerateReport(wrapper, sourceFile, null, null, CancellationToken.None);

            storage.Verify(s =>
                s.OpenWriteStreamAsync($"{filename}.xlsx", It.IsAny<string>(), It.IsAny<CancellationToken>()));
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
