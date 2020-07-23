using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable;
using FluentAssertions;
using Xunit;

namespace ESFA.DC.ESF.R2.ReportingService.Tests.AimAndDeliverable
{
    public class AimAndDeliverableModelBuilderTests
    {
        [Fact]
        public void BuildPeriodReportMonthLookup()
        {
            var lookup = NewBuilder().BuildPeriodReportMonthLookup("20", "21");

            lookup.Should().HaveCount(12);

            lookup[1].Should().Be("Aug-20");
            lookup[2].Should().Be("Sep-20");
            lookup[3].Should().Be("Oct-20");
            lookup[4].Should().Be("Nov-20");
            lookup[5].Should().Be("Dec-20");
            lookup[6].Should().Be("Jan-21");
            lookup[7].Should().Be("Feb-21");
            lookup[8].Should().Be("Mar-21");
            lookup[9].Should().Be("Apr-21");
            lookup[10].Should().Be("May-21");
            lookup[11].Should().Be("Jun-21");
            lookup[12].Should().Be("Jul-21");
        }

        [Theory]
        [InlineData("ESF-5000")]
        [InlineData("ESF-10000")]
        public void IsEsfRound2Contract_True(string conRefNumber)
        {
            NewBuilder().IsRoundTwoContract(conRefNumber).Should().BeTrue();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("ESF-4999")]
        [InlineData("ESF-0")]
        [InlineData("ESF-")]
        public void IsEsfRound2Contract_False(string conRefNumber)
        {
            NewBuilder().IsRoundTwoContract(conRefNumber).Should().BeFalse();
        }

        private AimAndDeliverableModelBuilder NewBuilder()
        {
            return new AimAndDeliverableModelBuilder();
        }
    }
}
