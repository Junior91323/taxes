using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Taxes.Business.Services.Initializers.Abstract;
using Taxes.Database;

namespace Taxes.Business.Services.Initializers
{
    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly DatabaseContext databaseContext;

        private readonly ILogger<DatabaseInitializer> logger;

        public DatabaseInitializer(DatabaseContext databaseContext, ILogger<DatabaseInitializer> logger)
        {
            this.databaseContext = databaseContext;
            this.logger = logger;
        }

        public async Task InitializeDatabaseAsync()
        {
            try
            {
                await this.databaseContext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
            }
            finally
            {
                this.databaseContext.Dispose();
            }
        }
    }
}
