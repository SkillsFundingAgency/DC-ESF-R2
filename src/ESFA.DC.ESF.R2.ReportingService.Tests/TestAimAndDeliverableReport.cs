﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ReportingService.Reports;
using ESFA.DC.ESF.R2.ReportingService.Services;
using ESFA.DC.ESF.R2.ReportingService.Tests.Builders;
using ESFA.DC.IO.Interfaces;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ReportingService.Tests
{
    public sealed class TestAimAndDeliverableReport
    {
        [Fact]
        public async Task TestAimAndDeliverableReportGeneration()
        {
            var csv = string.Empty;
            var dateTime = DateTime.UtcNow;
            var filename = $"10005752_2_ESF Round 2 Aim and Deliverable Report {dateTime:yyyyMMdd-HHmmss}";

            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.GetNowUtc()).Returns(dateTime);
            dateTimeProviderMock.Setup(x => x.ConvertUtcToUk(It.IsAny<DateTime>())).Returns(dateTime);

            var storage = new Mock<IStreamableKeyValuePersistenceService>();
            storage.Setup(x => x.SaveAsync($"{filename}.csv", It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Callback<string, string, CancellationToken>((key, value, ct) => csv = value)
                .Returns(Task.CompletedTask);

            var aimAndDeliverableServiceMock = new Mock<IAimAndDeliverableService>();
            aimAndDeliverableServiceMock
                .Setup(m => m.GetAimAndDeliverableModel(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(AimAndDeliverableBuilder.BuildAimAndDeliverableModels());

            var valueProvider = new ValueProvider();

            var aimAndDeliverableReport = new AimAndDeliverableReport(
                dateTimeProviderMock.Object,
                storage.Object,
                valueProvider,
                aimAndDeliverableServiceMock.Object);

            var wrapper = new SupplementaryDataWrapper
            {
                SupplementaryDataModels = new List<SupplementaryDataModel>()
            };
            SourceFileModel sourceFile = GetEsfSourceFileModel();

            await aimAndDeliverableReport.GenerateReport(wrapper, sourceFile, null, CancellationToken.None);

            Assert.True(!string.IsNullOrEmpty(csv));
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