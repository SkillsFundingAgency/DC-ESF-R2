using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.Config;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model;
using ESFA.DC.Logging.Interfaces;
using Moq;
using System.Collections.Generic;
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