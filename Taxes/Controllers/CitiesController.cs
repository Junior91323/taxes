using Microsoft.AspNetCore.Mvc;
using Taxes.Business.Services.Taxes.Abstract;
using Taxes.Common.Enums.Auth;
using Taxes.Common.Helpers.Paging;
using Taxes.Common.Models.Paging;
using Taxes.Common.Models.Responses;
using Taxes.Contracts.Cities.RequestModels;
using Taxes.Contracts.Cities.ResponseModels;
using Taxes.Infrastructure.Filters;

namespace Taxes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICitiesService citiesService;

        public CitiesController(ICitiesService citiesService)
        {
            this.citiesService = citiesService;
        }

        [Auth(Roles.User)]
        [HttpGet]
        [Route("list")]
        public async Task<ListResponse<CityResponse>> GetCities(
            [FromQuery] string sortField = "Id",
            [FromQuery] string sortOrder = "desc",
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            return await this.citiesService.GetCities(new PageSortInfo(sortField, sortOrder.ConvertToSortOrder(), pageIndex, pageSize), ct);
        }

        [Auth(Roles.Admin)]
        [HttpPost]
        public async Task<CityResponse> AddCity([FromBody] CreateCityRequest createCityRequest,
            CancellationToken ct = default)
        {
            return await this.citiesService.AddCity(createCityRequest, ct);
        }

        [Auth(Roles.Admin)]
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteCity([FromRoute] int id, CancellationToken ct = default)
        {
            await this.citiesService.DeleteCity(id, ct);
        }
    }
}
