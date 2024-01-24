using Microsoft.AspNetCore.Mvc;
using Taxes.Business.Services.Taxes.Abstract;
using Taxes.Common.Enums.Auth;
using Taxes.Common.Helpers.Paging;
using Taxes.Common.Models.Paging;
using Taxes.Common.Models.Responses;
using Taxes.Contracts.TaxSchedules.RequestModels;
using Taxes.Contracts.TaxSchedules.ResponseModels;
using Taxes.Infrastructure.Filters;

namespace Taxes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxSchedulesController : ControllerBase
    {
        private readonly ITaxSchedulesService taxSchedulesService;

        public TaxSchedulesController(ITaxSchedulesService taxSchedulesService)
        {
            this.taxSchedulesService = taxSchedulesService;
        }

        [Auth(Roles.User)]
        [HttpGet]
        [Route("taxSchedules")]
        public async Task<ListResponse<TaxScheduleResponse>> GetTaxSchedules(
            [FromQuery] int cityId,
            [FromQuery] string sortField = "Id",
            [FromQuery] string sortOrder = "desc",
            [FromQuery] int pageIndex = 0,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            return await this.taxSchedulesService.GetTaxSchedulesByCityId(cityId, new PageSortInfo(sortField, sortOrder.ConvertToSortOrder(), pageIndex, pageSize), ct);
        }

        [Auth(Roles.User)]
        [HttpGet]
        [Route("taxSchedulesByDate")]
        public async Task<TaxScheduleResponse> GetTaxSchedulesByDate([FromQuery] int cityId, [FromQuery] DateTime date, CancellationToken ct = default)
        {
            return await this.taxSchedulesService.GetTaxScheduleForDateByCityId(cityId, date, ct);
        }

        [Auth(Roles.Admin)]
        [HttpPost]
        public async Task<TaxScheduleResponse> Add([FromBody] CreateTaxScheduleRequest taxScheduleRequest, CancellationToken ct = default)
        {
            return await this.taxSchedulesService.AddTaxSchedule(taxScheduleRequest, ct);
        }

        [Auth(Roles.Admin)]
        [HttpPut]
        public async Task<TaxScheduleResponse> Update([FromBody] UpdateTaxScheduleRequest taxScheduleRequest, CancellationToken ct = default)
        {
            return await this.taxSchedulesService.UpdateTaxSchedule(taxScheduleRequest, ct);
        }

        [Auth(Roles.Admin)]
        [HttpDelete]
        [Route("{id}")]
        public async Task Delete([FromRoute] int id, CancellationToken ct = default)
        {
            await this.taxSchedulesService.DeleteTaxSchedule(id, ct);
        }
    }
}
