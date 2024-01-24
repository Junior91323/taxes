using Microsoft.Extensions.DependencyInjection;
using Taxes.Business.Services.Initializers;
using Taxes.Business.Services.Initializers.Abstract;
using Taxes.Business.Services.Taxes;
using Taxes.Business.Services.Taxes.Abstract;

namespace Taxes.Business.Services
{
    public static class DependenciesRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
            services.AddScoped<ICitiesService, CitiesService>();
            services.AddScoped<ITaxSchedulesService, TaxSchedulesService>();

            return services;
        }
    }
}
