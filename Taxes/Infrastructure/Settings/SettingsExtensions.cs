namespace Taxes.Infrastructure.Settings
{
    public static class SettingsExtensions
    {
        private const string ConnectionString = "ConnectionStrings";

        public static IConfigurationSection GetConnectionString(this IConfiguration configuration)
        {
            return configuration.GetSection(ConnectionString);
        }
    }
}
