using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Stateless;
using ESFA.DC.JobContextManager.Interface;
using ESFA.DC.JobContextManager.Model;
using ESFA.DC.JobContextManager.Model.Interface;
using Xunit;

namespace ESFA.DC.ESF.R2.Service.Stateless.Tests
{
    public sealed class AutoFacTest
    {
        [Fact]
        [Trait("Category", "Stateless")]
        public async Task TestRegistrations()
        {
            JobContextMessage jobContextMessage =
                new JobContextMessage(
                    1,
                    new ITopicItem[] { new TopicItem("SubscriptionName", new List<ITaskItem>()) },
                    0,
                    DateTime.UtcNow);

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
                    //bool ret = await messageHandler. .HandleAsync(jobContextMessage, cts.Token);
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
