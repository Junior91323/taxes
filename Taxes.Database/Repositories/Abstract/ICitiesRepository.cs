using Taxes.Common.Models.Paging;
using Taxes.Common.Models.Responses;
using Taxes.Database.Models;

namespace Taxes.Database.Repositories.Abstract
{
    public interface ICitiesRepository : IBaseRepository<City>
    {
        Task<ListResponse<City>> GetCities(PageSortInfo sortInfo, CancellationToken ct = default);

        Task<bool> DoesExist(int id, CancellationToken ct = default);
    }
}
