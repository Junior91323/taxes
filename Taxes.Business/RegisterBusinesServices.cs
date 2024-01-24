using Microsoft.Extensions.DependencyInjection;
using Taxes.Business.Services;
using Taxes.Business.Services.Initializers;

namespace Taxes.Business
{
    public static class DependenciesRegistration
    {
        public static IServiceCollection RegisterBusinesServices(this IServiceCollection services)
        {
            services.RegisterServices();
            services.RunDatabaseInitialization();

            return services;
        }
    }
}
