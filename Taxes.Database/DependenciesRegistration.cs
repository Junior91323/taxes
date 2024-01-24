using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Taxes.Database.Repositories;
using Taxes.Database.Repositories.Abstract;

namespace Taxes.Database
{
    public static class DependenciesRegistration
    {
        public static IServiceCollection AddMssqlDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddEntityFrameworkSqlServer().AddDbContext<DatabaseContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });

            services.AddScoped<ICitiesRepository, CitiesRepository>();
            services.AddScoped<ITaxSchedulesRepository, TaxSchedulesRepository>();
            services.AddScoped<ITaxSchedulesUnitOfWork, TaxSchedulesUnitOfWork>();

            return services;
        }
    }
}
