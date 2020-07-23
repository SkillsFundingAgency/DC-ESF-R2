using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Interfaces.Reports.AimAndDeliverable;
using ESFA.DC.ESF.R2.Stateless;
using ESFA.DC.JobContextManager.Interface;
using ESFA.DC.JobContextManager.Model;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.Service.Stateless.Tests
{
    public sealed class AutoFacTest
    {
        [Fact]
        [Trait("Category", "Stateless")]
        public async Task TestRegistrations()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.Cancel();

            ContainerBuilder containerBuilder = DIComposition.BuildContainer(new TestConfigurationHelper());

            IContainer c;
            try
            {
                RegisterYearSpecificServices(containerBuilder);
                c = containerBuilder.Build();

                using (var lifeTime = c.BeginLifetimeScope())
                {
                    var messageHandler = lifeTime.Resolve<IJobContextManager<JobContextMessage>>();
                    var serviceController = lifeTime.Resolve<IServiceController>();
                    var storageController = lifeTime.Resolve<IStorageController>();
                    var validationController = lifeTime.Resolve<IValidationController>();
                    var reportingController = lifeTime.Resolve<IReportingController>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void RegisterYearSpecificServices(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterInstance(Mock.Of<IIlrDataProvider>()).As<IIlrDataProvider>();
        }
    }
}
