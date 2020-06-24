using System;
using System.Diagnostics;
using System.Threading;
using Autofac;
using Autofac.Integration.ServiceFabric;
using ESFA.DC.ServiceFabric.Common.Config;

namespace ESFA.DC.ESF.R2.Stateless
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            try
            {
                // Setup Autofac
                ContainerBuilder builder = DIComposition.BuildContainer(new ServiceFabricConfigurationService());

                builder.RegisterServiceFabricSupport();

                // Register the stateless service.
                builder.RegisterStatelessService<ServiceFabric.Common.Stateless>("ESFA.DC.ESF.R2.StatelessType");

                using (var container = builder.Build())
                {
                    ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(ServiceFabric.Common.Stateless).Name);

                    // Prevents this host process from terminating so services keep running.
                    Thread.Sleep(Timeout.Infinite);
                }
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e + Environment.NewLine + (e.InnerException?.ToString() ?? "No inner exception"));
                throw;
            }
        }
    }
}
