using System.Threading.Tasks;
using Autofac;
using ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1819;
using ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1920;
using ESFA.DC.ILR.DataService.ILR1819EF.Valid;
using ESFA.DC.ILR.DataService.ILR1920EF.Valid;
using ESFA.DC.ILR.DataService.Models;
using Xunit;

namespace ESFA.DC.ILR.DataService.Services.Tests
{
    public class PeriodEndQueryServiceTests
    {
        [Fact]
        public async Task TestPeriodEndMetrics()
        {
            const int periodId = 12;

            var container = new ContainerBuilder();
            ConfigureModule(container);

            var context = container.Build();

            var queryService1819 = new PeriodEndQueryService1819(() => new ILR1819ValidLearnerContext());
            var queryService1920 = new PeriodEndQueryService1920(() => new ILR1920ValidLearnerContext());

            var result1819 = await queryService1819.GetPeriodEndMetrics(periodId);

            var result1920 = await queryService1920.GetPeriodEndMetrics(periodId);
        }

        private void ConfigureModule(ContainerBuilder container)
        {
            container.RegisterModule(new DependencyInjectionModule
            {
                Configuration = new ILRConfiguration
                {
                    ILR1819ConnectionString = "Server=(local);Database=ILR1819_DataStore;Integrated Security=True;",
                    ILR1920ConnectionString = "Server=(local);Database=ILR1920_DataStore;Integrated Security=True;"
                }
            });
        }
    }
}