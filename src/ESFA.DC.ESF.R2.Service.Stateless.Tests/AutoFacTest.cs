﻿using System.Threading.Tasks;
using Xunit;

namespace ESFA.DC.ESF.R2.Service.Stateless.Tests
{
    public sealed class AutoFacTest
    {
        [Fact]
        public async Task TestRegistrations()
        {
            //JobContextMessage jobContextMessage =
            //    new JobContextMessage(
            //        1,
            //        new ITopicItem[] { new TopicItem("SubscriptionName", new List<ITaskItem>()) },
            //        0,
            //        DateTime.UtcNow);

            //CancellationTokenSource cts = new CancellationTokenSource();
            //cts.Cancel();

            //ContainerBuilder containerBuilder = DIComposition.BuildContainer(new TestConfigurationHelper());
            //IContainer c;
            //try
            //{
            //    c = containerBuilder.Build();

            //    using (var lifeTime = c.BeginLifetimeScope())
            //    {
            //        var messageHandler = lifeTime.Resolve<IJobContextManager<JobContextMessage>>();
            //        //bool ret = await messageHandler. .HandleAsync(jobContextMessage, cts.Token);
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}
        }
    }
}