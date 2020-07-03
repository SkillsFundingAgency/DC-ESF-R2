using Autofac;
using ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1516;
using ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1617;
using ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1718;
using ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1819;
using ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1920;
using ESFA.DC.ILR.DataService.ILR1516EF;
using ESFA.DC.ILR.DataService.ILR1617EF;
using ESFA.DC.ILR.DataService.ILR1718EF;
using ESFA.DC.ILR.DataService.ILR1819EF.Rulebase;
using ESFA.DC.ILR.DataService.ILR1819EF.Valid;
using ESFA.DC.ILR.DataService.ILR1920EF.Rulebase;
using ESFA.DC.ILR.DataService.ILR1920EF.Valid;
using ESFA.DC.ILR.DataService.Interfaces.Repositories;
using ESFA.DC.ILR.DataService.Interfaces.Services;
using ESFA.DC.ILR.DataService.Models;
using ESFA.DC.ILR.DataService.Services.PeriodEnd;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ILR.DataService.Services
{
    public class DependencyInjectionModule : Module
    {
        public ILRConfiguration Configuration { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterContexts(builder);

            builder.RegisterType<FileDetails1516Repository>().As<IFileDetails1516Repository>().InstancePerLifetimeScope();
            builder.RegisterType<FileDetails1617Repository>().As<IFileDetails1617Repository>().InstancePerLifetimeScope();
            builder.RegisterType<FileDetails1718Repository>().As<IFileDetails1718Repository>().InstancePerLifetimeScope();
            builder.RegisterType<FileDetails1819Repository>().As<IFileDetails1819Repository>().InstancePerLifetimeScope();
            builder.RegisterType<FileDetails1920Repository>().As<IFileDetails1920Repository>().InstancePerLifetimeScope();

            builder.RegisterType<Fm701516Repository>().As<IFm701516Repository>().InstancePerLifetimeScope();
            builder.RegisterType<Fm701617Repository>().As<IFm701617Repository>().InstancePerLifetimeScope();
            builder.RegisterType<Fm701718Repository>().As<IFm701718Repository>().InstancePerLifetimeScope();
            builder.RegisterType<Fm701819Repository>().As<IFm701819Repository>().InstancePerLifetimeScope();
            builder.RegisterType<Fm701920Repository>().As<IFm701920Repository>().InstancePerLifetimeScope();

            builder.RegisterType<Valid1819Repository>().As<IValid1819Repository>().InstancePerLifetimeScope();
            builder.RegisterType<Valid1920Repository>().As<IValid1920Repository>().InstancePerLifetimeScope();

            builder.RegisterType<PeriodEndQueryService1819>().As<IPeriodEndQueryService1819>().InstancePerLifetimeScope();
            builder.RegisterType<PeriodEndQueryService1920>().As<IPeriodEndQueryService1920>().InstancePerLifetimeScope();

            builder.RegisterType<PeriodEndMetricsService1819>().As<IPeriodEndMetricsService1819>().InstancePerLifetimeScope();
            builder.RegisterType<PeriodEndMetricsService1920>().As<IPeriodEndMetricsService1920>().InstancePerLifetimeScope();

            builder.RegisterType<FileDetailsDataService>().As<IFileDetailsDataService>().InstancePerLifetimeScope();
            builder.RegisterType<Fm70DataService>().As<IFm70DataService>().InstancePerLifetimeScope();

            builder.RegisterType<ValidLearnerDataService1819>().As<IValidLearnerDataService1819>().InstancePerLifetimeScope();
            builder.RegisterType<ValidLearnerDataService1920>().As<IValidLearnerDataService1920>().InstancePerLifetimeScope();
        }

        private void RegisterContexts(ContainerBuilder builder)
        {
            builder.Register(c =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ILR1516Context>();
                    optionsBuilder.UseSqlServer(
                        Configuration.ILR1516ConnectionString,
                        providerOptions => providerOptions.CommandTimeout(60));
                    return new ILR1516Context(optionsBuilder.Options);
                })
                .As<ILR1516Context>();

            builder.Register(c =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ILR1617Context>();
                    optionsBuilder.UseSqlServer(
                        Configuration.ILR1617ConnectionString,
                        providerOptions => providerOptions.CommandTimeout(60));
                    return new ILR1617Context(optionsBuilder.Options);
                })
                .As<ILR1617Context>();

            builder.Register(c =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ILR1718Context>();
                    optionsBuilder.UseSqlServer(
                        Configuration.ILR1718ConnectionString,
                        providerOptions => providerOptions.CommandTimeout(60));
                    return new ILR1718Context(optionsBuilder.Options);
                })
                .As<ILR1718Context>();

            builder.Register(c =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ILR1819RuleBaseContext>();
                    optionsBuilder.UseSqlServer(
                        Configuration.ILR1819ConnectionString,
                        providerOptions => providerOptions.CommandTimeout(60));
                    return new ILR1819RuleBaseContext(optionsBuilder.Options);
                })
                .As<ILR1819RuleBaseContext>();

            builder.Register(c =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ILR1819ValidLearnerContext>();
                    optionsBuilder.UseSqlServer(
                        Configuration.ILR1819ConnectionString,
                        providerOptions => providerOptions.CommandTimeout(60));
                    return new ILR1819ValidLearnerContext(optionsBuilder.Options);
                })
                .As<ILR1819ValidLearnerContext>();

            builder.Register(c =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ILR1920RulebaseContext>();
                    optionsBuilder.UseSqlServer(
                        Configuration.ILR1920ConnectionString,
                        providerOptions => providerOptions.CommandTimeout(60));
                    return new ILR1920RulebaseContext(optionsBuilder.Options);
                })
                .As<ILR1920RulebaseContext>();

            builder.Register(c =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<ILR1920ValidLearnerContext>();
                    optionsBuilder.UseSqlServer(
                        Configuration.ILR1920ConnectionString,
                        providerOptions => providerOptions.CommandTimeout(60));
                    return new ILR1920ValidLearnerContext(optionsBuilder.Options);
                })
                .As<ILR1920ValidLearnerContext>();
        }
    }
}
