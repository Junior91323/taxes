using Moq;
using Taxes.Business.Services.Taxes;
using Taxes.Business.Services.Taxes.Abstract;
using Taxes.Common.Models.Paging;
using Taxes.Common.Models.Responses;
using Taxes.Contracts.Cities.RequestModels;
using Taxes.Contracts.Cities.ResponseModels;
using Taxes.Database;
using Taxes.Database.Models;
using Taxes.Database.Repositories.Abstract;

namespace Taxes.Tests.UnitTests
{
    public class Tests
    {
        private ICitiesService citiesService;
        private Mock<ITaxSchedulesUnitOfWork> mockUnitOfWork;
        private Mock<ICitiesRepository> mockCitiesRepository;

        [SetUp]
        public void Setup()
        {
            mockUnitOfWork = new Mock<ITaxSchedulesUnitOfWork>();
            mockCitiesRepository = new Mock<ICitiesRepository>();
            citiesService = new CitiesService(mockUnitOfWork.Object);
        }

        [Test]
        public async Task AddCity_ReturnsCityResponse()
        {
            mockUnitOfWork.Setup(uow => uow.CitiesRepository)
                .Returns(mockCitiesRepository.Object);

            var createCityRequest = new CreateCityRequest
            {
                Name = "Test City",
            };

            mockCitiesRepository.Setup(repo => repo.Add(It.IsAny<City>()))
                .Callback<City>(city =>
                {
                    Assert.AreEqual(createCityRequest.Name, city.Name);
                });

            mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(CancellationToken.None))
                .Returns(Task.FromResult(1));

            var result = await citiesService.AddCity(createCityRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual(createCityRequest.Name, result.Name);
        }

        [Test]
        public async Task GetCities_ReturnsListResponseOfCityResponse()
        {
            mockUnitOfWork.Setup(uow => uow.CitiesRepository)
                .Returns(mockCitiesRepository.Object);

            var sortInfo = new PageSortInfo();

            var mockCitiesList = new List<City>
        {
            new City { Name = "City1", },
        };

            mockCitiesRepository.Setup(repo =>
                repo.GetCities(sortInfo, CancellationToken.None))
                .ReturnsAsync(new ListResponse<City> { TotalCount = mockCitiesList.Count, Data = mockCitiesList });

            var result = await citiesService.GetCities(sortInfo);

            Assert.IsNotNull(result);
            Assert.AreEqual(mockCitiesList.Count, result.TotalCount);
            Assert.IsNotNull(result.Data);
            Assert.IsInstanceOf<IEnumerable<CityResponse>>(result.Data);
            Assert.AreEqual(mockCitiesList.Count, result.Data.Count());
        }

        [Test]
        public async Task DeleteCity_Succeeds()
        {
            mockUnitOfWork.Setup(uow => uow.CitiesRepository)
                .Returns(mockCitiesRepository.Object);

            const int cityId = 1;

            var existingCity = new City
            {
                Id = cityId,
            };

            mockCitiesRepository.Setup(repo => repo.GetById(cityId, CancellationToken.None))
                .ReturnsAsync(existingCity);

            mockCitiesRepository.Setup(repo => repo.Remove(It.IsAny<City>()));

            mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(CancellationToken.None))
                .Returns(Task.FromResult(cityId));

            await citiesService.DeleteCity(cityId);
        }
    }
}