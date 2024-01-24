using Taxes.Database.Repositories;
using Taxes.Database.Repositories.Abstract;

namespace Taxes.Database
{
    public class TaxSchedulesUnitOfWork : ITaxSchedulesUnitOfWork, IDisposable
    {
        private readonly DatabaseContext databaseContext;

        public ICitiesRepository CitiesRepository { get; }

        public ITaxSchedulesRepository TaxSchedulesRepository { get; }

        public TaxSchedulesUnitOfWork(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
            this.CitiesRepository = new CitiesRepository(this.databaseContext);
            this.TaxSchedulesRepository = new TaxSchedulesRepository(this.databaseContext);
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct)
        {
            return await this.databaseContext.SaveChangesAsync(ct);
        }

        public void Dispose()
        {
            databaseContext?.Dispose();
        }
    }
}
