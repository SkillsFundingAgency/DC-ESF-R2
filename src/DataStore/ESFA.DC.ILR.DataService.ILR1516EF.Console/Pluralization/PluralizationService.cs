using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Pluralization
{
    public class PluralizationService : IDesignTimeServices
    {
        public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IPluralizer, Pluralizer>();
        }
    }
}
