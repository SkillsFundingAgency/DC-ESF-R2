using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Stateless;
using ESFA.DC.JobContextManager.Interface;
using ESFA.DC.JobContextManager.Model;
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
    }
}
