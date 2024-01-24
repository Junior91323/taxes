using Taxes.Database.Models;
using Taxes.Database.Repositories.Abstract;

namespace Taxes.Database.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntityModel
    {
        protected DatabaseContext Context { get; }

        protected BaseRepository(DatabaseContext context)
        {
            this.Context = context;
        }

        public async Task Add(T entity)
        {
            await this.Context.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await this.Context.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            this.Context.Update<T>(entity);
        }

        public void Remove(T entity)
        {
            this.Context.Remove<T>(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            this.Context.RemoveRange(entities);
        }

        public virtual async Task<T> GetById(int id, CancellationToken ct = default)
        {
            return await this.Context.FindAsync<T>(id, ct);
        }
    }
}
