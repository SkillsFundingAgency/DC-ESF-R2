﻿using System;
using System.Diagnostics;
using System.Threading;
using Autofac.Integration.ServiceFabric;
using ESFA.DC.ServiceFabric.Helpers;

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
                // The ServiceManifest.XML file defines one or more service type names.
                // Registering a service maps a service type name to a .NET type.
                // When Service Fabric creates an instance of this service type,
                // an instance of the class is created in this host process.

                var builder = DIComposition.BuildContainer(new ConfigurationHelper());

                builder.RegisterServiceFabricSupport();

                // Register the stateless service.
                builder.RegisterStatelessService<Stateless>("ESFA.DC.ESF.Service.StatelessType");

                using (var container = builder.Build())
                {
                    ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(Stateless).Name);

                    // Prevents this host process from terminating so services keep running.
                    Thread.Sleep(Timeout.Infinite);
                }
            }
            catch (Exception e)
            {
                ServiceEventSource.Current.ServiceHostInitializationFailed(e.ToString());
                throw;
            }
        }
    }
}
