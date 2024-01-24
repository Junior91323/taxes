namespace Taxes.Database.Repositories.Abstract
{
    public interface IBaseRepository<T>
    {
        Task Add(T entity);

        Task AddRange(IEnumerable<T> entities);

        void Update(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        Task<T> GetById(int id, CancellationToken ct = default);
    }
}
