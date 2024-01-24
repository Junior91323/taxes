using Taxes.Database.Repositories.Abstract;

namespace Taxes.Database
{
    public interface ITaxSchedulesUnitOfWork
    {
        ICitiesRepository CitiesRepository { get; }

        ITaxSchedulesRepository TaxSchedulesRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken ct);
    }
}
