using ESFA.DC.ESF.R2.EF.Console.Pluralization;
using ESFA.DC.ILR.DataStore.EF.Console.DesignTime;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace ESFA.DC.ESF.R2.EF.Console
{
    public class DesignTimeServices : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IPluralizer, Pluralizer>();
            serviceCollection.AddSingleton<ICandidateNamingService, FundingDataCandidateNamingService>();
        }
    }
}