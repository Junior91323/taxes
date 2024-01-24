using Taxes.Common.Models.Paging;
using Taxes.Common.Models.Responses;
using Taxes.Contracts.Cities.RequestModels;
using Taxes.Contracts.Cities.ResponseModels;

namespace Taxes.Business.Services.Taxes.Abstract
{
    public interface ICitiesService
    {
        Task<ListResponse<CityResponse>> GetCities(PageSortInfo sortInfo, CancellationToken ct = default);

        Task<CityResponse> AddCity(CreateCityRequest cityRequest, CancellationToken ct = default);

        Task DeleteCity(int id, CancellationToken ct = default);
    }
}
