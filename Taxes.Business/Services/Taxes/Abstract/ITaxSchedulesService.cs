using Taxes.Common.Models.Paging;
using Taxes.Common.Models.Responses;
using Taxes.Contracts.TaxSchedules.RequestModels;
using Taxes.Contracts.TaxSchedules.ResponseModels;

namespace Taxes.Business.Services.Taxes.Abstract
{
    public interface ITaxSchedulesService
    {
        Task<ListResponse<TaxScheduleResponse>> GetTaxSchedulesByCityId(int cityId, PageSortInfo sortInfo, CancellationToken ct = default);

        Task<TaxScheduleResponse> GetTaxScheduleForDateByCityId(int cityId, DateTime date, CancellationToken ct = default);

        Task<TaxScheduleResponse> AddTaxSchedule(CreateTaxScheduleRequest taxScheduleRequest, CancellationToken ct = default);

        Task<TaxScheduleResponse> UpdateTaxSchedule(UpdateTaxScheduleRequest taxScheduleRequest, CancellationToken ct = default);

        Task DeleteTaxSchedule(int id, CancellationToken ct = default);
    }
}
