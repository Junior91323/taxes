using Microsoft.Extensions.DependencyInjection;
using Taxes.Business.Services.Initializers.Abstract;

namespace Taxes.Business.Services.Initializers
{
    public static class DatabaseInitializerRegistration
    {
        public static void RunDatabaseInitialization(this IServiceCollection services)
        {
            services
                .BuildServiceProvider()
                .GetService<IDatabaseInitializer>()
                .InitializeDatabaseAsync()
                .GetAwaiter()
                .GetResult();
        }
    }
}
