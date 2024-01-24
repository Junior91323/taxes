using Taxes.Common.Models.Settings;

namespace Taxes.Infrastructure.Settings
{
    public static class SettingsModelsExtensions
    {
        public static ConnectionStrings GetConnectionStringsModel(this IConfiguration configuration)
        {
            return configuration.GetConnectionString().Get<ConnectionStrings>();
        }
    }
}
