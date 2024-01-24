using Microsoft.EntityFrameworkCore;
using Taxes.Common.Enums;
using Taxes.Common.Extensions;
using Taxes.Common.Models.Paging;
using Taxes.Common.Models.Responses;
using Taxes.Database.Models;
using Taxes.Database.Repositories.Abstract;

namespace Taxes.Database.Repositories
{
    public class TaxSchedulesRepository : BaseRepository<TaxSchedule>, ITaxSchedulesRepository
    {
        public TaxSchedulesRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<TaxSchedule> GetTaxScheduleForDateByCityId(int cityId, DateTime date, CancellationToken ct = default)
        {
            return await this.Context.TaxSchedules.Where(x => x.CityId == cityId
                                                              && date.Date >= x.StartDate.Date
                                                              && date.Date <= x.EndDate.Date)
                .OrderBy(x => x.PeriodType)
                .FirstOrDefaultAsync(ct);
        }

        public async Task<bool> DoesTaxScheduleExist(int cityId, DateTime date, PeriodType periodType, int? id = null, CancellationToken ct = default)
        {
            return await this.Context.TaxSchedules.Where(x =>
                (id == null || x.Id != id)
                && x.CityId == cityId
                && x.StartDate.Date == date.Date
                && x.PeriodType == periodType).AnyAsync(ct);
        }

        public async Task<ListResponse<TaxSchedule>> GetTaxSchedulesByCityId(int cityId, PageSortInfo sortInfo, CancellationToken ct = default)
        {
            var query = this.Context.TaxSchedules.Where(x => x.CityId == cityId);

            var totalCount = await query.CountAsync(ct);

            var list = await query.SortAndFilter(sortInfo).ToListAsync(ct);

            return new ListResponse<TaxSchedule>() { TotalCount = totalCount, Data = list };
        }
    }
}
