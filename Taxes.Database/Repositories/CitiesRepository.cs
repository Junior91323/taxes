using Microsoft.EntityFrameworkCore;
using Taxes.Common.Extensions;
using Taxes.Common.Models.Paging;
using Taxes.Common.Models.Responses;
using Taxes.Database.Models;
using Taxes.Database.Repositories.Abstract;

namespace Taxes.Database.Repositories
{
    public class CitiesRepository : BaseRepository<City>, ICitiesRepository
    {
        public CitiesRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<ListResponse<City>> GetCities(PageSortInfo sortInfo, CancellationToken ct = default(CancellationToken))
        {
            var totalCount = await this.Context.Cities.CountAsync(ct);

            var items = await this.Context.Cities.SortAndFilter(sortInfo).ToListAsync(ct);

            return new ListResponse<City> { TotalCount = totalCount, Data = items };
        }

        public async Task<bool> DoesExist(int id, CancellationToken ct = default(CancellationToken))
        {
            return await this.Context.Cities.AnyAsync(x => x.Id == id, ct);
        }
    }
}
