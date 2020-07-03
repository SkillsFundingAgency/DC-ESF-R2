using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using ESFA.DC.ILR.DataService.Interfaces.Repositories;
using ESFA.DC.ILR.DataService.Models;
using Moq;
using Xunit;

namespace ESFA.DC.ILR.DataService.Services.Tests
{
    public class Fm70DataServiceTests
    {
        [Fact]
        public async Task TestDataServiceDoesNotBlowUp()
        {
            var container = new ContainerBuilder();
            container.RegisterModule<DependencyInjectionModule>();

            container.Build();

            const int ukPrn = 10006439;

            IEnumerable<FM70PeriodisedValues> values = new List<FM70PeriodisedValues>();

            var repo1516 = new Mock<IFm701516Repository>();
            repo1516
                .Setup(m => m.Get1516PeriodisedValues(ukPrn, CancellationToken.None))
                .ReturnsAsync(values);

            var repo1617 = new Mock<IFm701617Repository>();
            repo1617
                .Setup(m => m.Get1617PeriodisedValues(ukPrn, CancellationToken.None))
                .ReturnsAsync(values);

            var repo1718 = new Mock<IFm701718Repository>();
            repo1718
                .Setup(m => m.Get1718PeriodisedValues(ukPrn, CancellationToken.None))
                .ReturnsAsync(values);

            var repo1819 = new Mock<IFm701819Repository>();
            repo1819
                .Setup(m => m.GetPeriodisedValues(ukPrn, CancellationToken.None))
                .ReturnsAsync(values);

            var repo1920 = new Mock<IFm701920Repository>();
            repo1920
                .Setup(m => m.GetPeriodisedValues(ukPrn, CancellationToken.None))
                .ReturnsAsync(values);

            var dataService = new Fm70DataService(repo1516.Object, repo1617.Object, repo1718.Object, repo1819.Object, repo1920.Object);

            var result = await dataService.GetPeriodisedValuesAllYears(ukPrn, CancellationToken.None);

            Assert.Equal(result, values);
        }
    }
}