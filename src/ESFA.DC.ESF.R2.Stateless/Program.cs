using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using Autofac;
using Autofac.Integration.ServiceFabric;
using ESFA.DC.ESF.R2.Service.Config;
using ESFA.DC.Logging;
using ESFA.DC.Logging.Config;
using ESFA.DC.Logging.Config.Interfaces;
using ESFA.DC.Logging.Enums;
using ESFA.DC.ServiceFabric.Helpers;
using ESFA.DC.ServiceFabric.Helpers.Interfaces;
using ExecutionContext = ESFA.DC.Logging.ExecutionContext;

namespace ESFA.DC.ESF.R2.Stateless
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            IConfigurationHelper configHelper = new ConfigurationHelper();
            var loggerConfig = configHelper.GetSectionValues<LoggerOptions>("LoggerSection");
            SeriLogger logger = new SeriLogger(
                new ApplicationLoggerSettings
                {
                    ApplicationLoggerOutputSettingsCollection = new List<IApplicationLoggerOutputSettings>
                    {
                        new MsSqlServerApplicationLoggerOutputSettings
                        {
                            MinimumLogLevel = LogLevel.Verbose,
                            ConnectionString = loggerConfig.LoggerConnectionstring,
                            LogsTableName = "Logs"
                        }
                    }
                },
                new ExecutionContext());

            try
            {
                // Licence Aspose.Cells
                SoftwareLicenceSection softwareLicenceSection =
                    configHelper.GetSectionValues<SoftwareLicenceSection>(nameof(SoftwareLicenceSection));
                if (!string.IsNullOrEmpty(softwareLicenceSection.AsposeLicence))
                {
                    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(softwareLicenceSection
                        .AsposeLicence.Replace("&lt;", "<").Replace("&gt;", ">"))))
                    {
                        new Aspose.Cells.License().SetLicense(ms);
                    }
                }

                // Setup Autofac
                ContainerBuilder builder = DIComposition.BuildContainer(configHelper);

                builder.RegisterServiceFabricSupport();

                throw new Exception("testing");

                // Register the stateless service.
                builder.RegisterStatelessService<Stateless>("ESFA.DC.ESF.R2.StatelessType");

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
                logger.LogError(e.Message, e);
                throw;
            }
        }
    }
}
