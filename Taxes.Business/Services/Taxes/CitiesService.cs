using Taxes.Business.Mappers;
using Taxes.Business.Services.Taxes.Abstract;
using Taxes.Common.Models.Paging;
using Taxes.Common.Models.Responses;
using Taxes.Contracts.Cities.RequestModels;
using Taxes.Contracts.Cities.ResponseModels;
using Taxes.Database;

namespace Taxes.Business.Services.Taxes
{
    public class CitiesService : ICitiesService
    {
        private readonly ITaxSchedulesUnitOfWork taxSchedulesUnitOfWork;

        public CitiesService(ITaxSchedulesUnitOfWork taxSchedulesUnitOfWork)
        {
            this.taxSchedulesUnitOfWork = taxSchedulesUnitOfWork;
        }

        public async Task<CityResponse> AddCity(CreateCityRequest cityRequest, CancellationToken ct = default)
        {
            Validate(cityRequest);

            var city = CityMapper.Map(cityRequest);

            await this.taxSchedulesUnitOfWork.CitiesRepository.Add(city);

            await this.taxSchedulesUnitOfWork.SaveChangesAsync(ct);

            return CityMapper.Map(city);
        }

        public async Task<ListResponse<CityResponse>> GetCities(PageSortInfo sortInfo, CancellationToken ct = default)
        {
            sortInfo ??= new PageSortInfo();

            var cities = await this.taxSchedulesUnitOfWork.CitiesRepository.GetCities(sortInfo, ct);

            return new ListResponse<CityResponse> { TotalCount = cities.TotalCount, Data = cities.Data.Select(CityMapper.Map) };
        }

        public async Task DeleteCity(int id, CancellationToken ct = default)
        {
            var city = await this.taxSchedulesUnitOfWork.CitiesRepository.GetById(id, ct);

            if (city != null)
                this.taxSchedulesUnitOfWork.CitiesRepository.Remove(city);

            await this.taxSchedulesUnitOfWork.SaveChangesAsync(ct);
        }

        private void Validate(CreateCityRequest cityRequest)
        {
            if (cityRequest == null)
                throw new ArgumentNullException(nameof(cityRequest));
        }
    }
}
