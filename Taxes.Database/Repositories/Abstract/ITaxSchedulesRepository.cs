using Taxes.Common.Enums;
using Taxes.Common.Models.Paging;
using Taxes.Common.Models.Responses;
using Taxes.Database.Models;

namespace Taxes.Database.Repositories.Abstract
{
    public interface ITaxSchedulesRepository : IBaseRepository<TaxSchedule>
    {
        Task<TaxSchedule> GetTaxScheduleForDateByCityId(int cityId, DateTime date, CancellationToken ct = default);

        Task<bool> DoesTaxScheduleExist(int cityId, DateTime date, PeriodType periodType, int? id = null, CancellationToken ct = default);

        Task<ListResponse<TaxSchedule>> GetTaxSchedulesByCityId(int cityId, PageSortInfo sortInfo, CancellationToken ct = default);
    }
}
