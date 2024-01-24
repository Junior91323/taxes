using Moq;
using Taxes.Business.Services.Taxes;
using Taxes.Database.Repositories.Abstract;
using Taxes.Database;
using Taxes.Business.Services.Taxes.Abstract;
using Taxes.Common.Exceptions;
using Taxes.Common.Models.Paging;
using Taxes.Common.Models.Responses;
using Taxes.Contracts.TaxSchedules.RequestModels;
using Taxes.Contracts.TaxSchedules.ResponseModels;
using Taxes.Database.Models;

namespace Taxes.Tests.UnitTests
{
    [TestFixture]
    public class TaxSchedulesTests
    {
        private ITaxSchedulesService taxSchedulesService;
        private Mock<ITaxSchedulesUnitOfWork> mockUnitOfWork;
        private Mock<ITaxSchedulesRepository> mockTaxSchedulesRepository;
        private Mock<ICitiesRepository> mockCitiesRepository;

        [SetUp]
        public void Setup()
        {
            this.mockUnitOfWork = new Mock<ITaxSchedulesUnitOfWork>();
            this.mockTaxSchedulesRepository = new Mock<ITaxSchedulesRepository>();
            this.mockCitiesRepository = new Mock<ICitiesRepository>();
            this.taxSchedulesService = new TaxSchedulesService(this.mockUnitOfWork.Object);
        }

        [Test]
        public async Task GetTaxScheduleForDateByCityId_ReturnsTaxScheduleResponse()
        {
            mockUnitOfWork.Setup(uow => uow.TaxSchedulesRepository)
                .Returns(mockTaxSchedulesRepository.Object);

            var cityId = 1;
            var date = DateTime.Now;

            var mockTaxSchedule = new TaxSchedule
            {
                CityId = cityId,
                StartDate = date,
            };

            mockTaxSchedulesRepository.Setup(repo =>
                repo.GetTaxScheduleForDateByCityId(cityId, date, CancellationToken.None))
                .ReturnsAsync(mockTaxSchedule);

            var result = await taxSchedulesService.GetTaxScheduleForDateByCityId(cityId, date);

            Assert.IsNotNull(result);
            Assert.AreEqual(cityId, result.CityId);
            Assert.AreEqual(date, result.StartDate);
        }

        [Test]
        public async Task GetTaxSchedulesByCityId_ReturnsListResponseOfTaxScheduleResponse()
        {
            mockUnitOfWork.Setup(uow => uow.TaxSchedulesRepository)
                .Returns(mockTaxSchedulesRepository.Object);

            var cityId = 1;
            var sortInfo = new PageSortInfo();

            var mockTaxSchedulesList = new List<TaxSchedule>
        {
            new TaxSchedule { CityId = cityId,},
        };

            mockTaxSchedulesRepository.Setup(repo =>
                repo.GetTaxSchedulesByCityId(cityId, sortInfo, CancellationToken.None))
                .ReturnsAsync(new ListResponse<TaxSchedule> { TotalCount = mockTaxSchedulesList.Count, Data = mockTaxSchedulesList });

            var result = await taxSchedulesService.GetTaxSchedulesByCityId(cityId, sortInfo);

            Assert.IsNotNull(result);
            Assert.AreEqual(mockTaxSchedulesList.Count, result.TotalCount);
            Assert.IsNotNull(result.Data);
            Assert.IsInstanceOf<IEnumerable<TaxScheduleResponse>>(result.Data);
            Assert.AreEqual(mockTaxSchedulesList.Count, result.Data.Count());
        }

        [Test]
        public async Task AddTaxSchedule_ReturnsTaxScheduleResponse()
        {
            mockUnitOfWork.Setup(uow => uow.TaxSchedulesRepository)
                .Returns(mockTaxSchedulesRepository.Object);

            mockUnitOfWork.Setup(uow => uow.CitiesRepository)
                .Returns(mockCitiesRepository.Object);

            var createTaxScheduleRequest = new CreateTaxScheduleRequest
            {
                CityId = 1,
                StartDate = DateTime.Now,
            };

            mockCitiesRepository.Setup(repo => repo.DoesExist(createTaxScheduleRequest.CityId, CancellationToken.None))
                .ReturnsAsync(true);

            mockTaxSchedulesRepository.Setup(repo =>
                    repo.DoesTaxScheduleExist(
                        createTaxScheduleRequest.CityId,
                        createTaxScheduleRequest.StartDate,
                        createTaxScheduleRequest.PeriodType,
                        null,
                        CancellationToken.None))
                .ReturnsAsync(false);

            mockTaxSchedulesRepository.Setup(repo => repo.Add(It.IsAny<TaxSchedule>()))
                .Callback<TaxSchedule>(taxSchedule =>
                {
                    Assert.AreEqual(createTaxScheduleRequest.CityId, taxSchedule.CityId);
                    Assert.AreEqual(createTaxScheduleRequest.StartDate, taxSchedule.StartDate);
                });

            mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(CancellationToken.None))
                .Returns(Task.FromResult(1));

            var result = await taxSchedulesService.AddTaxSchedule(createTaxScheduleRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual(createTaxScheduleRequest.CityId, result.CityId);
            Assert.AreEqual(createTaxScheduleRequest.StartDate, result.StartDate);
        }

        [Test]
        public async Task UpdateTaxSchedule_ReturnsTaxScheduleResponse()
        {
            mockUnitOfWork.Setup(uow => uow.TaxSchedulesRepository)
                .Returns(mockTaxSchedulesRepository.Object);

            mockUnitOfWork.Setup(uow => uow.CitiesRepository)
                .Returns(mockCitiesRepository.Object);

            var updateTaxScheduleRequest = new UpdateTaxScheduleRequest
            {
                Id = 1,
                CityId = 1,
                StartDate = DateTime.Now,
            };

            mockCitiesRepository.Setup(repo => repo.DoesExist(updateTaxScheduleRequest.CityId, CancellationToken.None))
                .ReturnsAsync(true);

            mockTaxSchedulesRepository.Setup(repo =>
                    repo.DoesTaxScheduleExist(
                        updateTaxScheduleRequest.CityId,
                        updateTaxScheduleRequest.StartDate,
                        updateTaxScheduleRequest.PeriodType,
                        updateTaxScheduleRequest.Id,
                        CancellationToken.None))
                .ReturnsAsync(false);

            var existingTaxSchedule = new TaxSchedule
            {
                Id = 1,
                CityId = 1,
            };

            mockTaxSchedulesRepository.Setup(repo => repo.GetById(updateTaxScheduleRequest.Id, CancellationToken.None))
                .ReturnsAsync(existingTaxSchedule);

            mockTaxSchedulesRepository.Setup(repo => repo.Update(It.IsAny<TaxSchedule>()))
                .Callback<TaxSchedule>(taxSchedule =>
                {
                    Assert.AreEqual(updateTaxScheduleRequest.CityId, taxSchedule.CityId);
                    Assert.AreEqual(updateTaxScheduleRequest.StartDate, taxSchedule.StartDate);
                });

            mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(CancellationToken.None))
                .Returns(Task.FromResult(updateTaxScheduleRequest.Id));

            var result = await taxSchedulesService.UpdateTaxSchedule(updateTaxScheduleRequest);

            Assert.IsNotNull(result);
            Assert.AreEqual(updateTaxScheduleRequest.CityId, result.CityId);
            Assert.AreEqual(updateTaxScheduleRequest.StartDate, result.StartDate);
        }

        [Test]
        public async Task DeleteTaxSchedule_Succeeds()
        {
            mockUnitOfWork.Setup(uow => uow.TaxSchedulesRepository)
                .Returns(mockTaxSchedulesRepository.Object);

            const int taxScheduleId = 1;

            var existingTaxSchedule = new TaxSchedule
            {
                Id = taxScheduleId,
            };

            mockTaxSchedulesRepository.Setup(repo => repo.GetById(taxScheduleId, CancellationToken.None))
                .ReturnsAsync(existingTaxSchedule);

            mockTaxSchedulesRepository.Setup(repo => repo.Remove(It.IsAny<TaxSchedule>()));

            mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(CancellationToken.None))
                .Returns(Task.FromResult(taxScheduleId));

            await taxSchedulesService.DeleteTaxSchedule(taxScheduleId);
        }

        [Test]
        public void AddTaxSchedule_ThrowsArgumentNullException_WhenCreateTaxScheduleRequestIsNull()
        {
            CreateTaxScheduleRequest createTaxScheduleRequest = null;

            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => taxSchedulesService.AddTaxSchedule(createTaxScheduleRequest));
            Assert.AreEqual("taxScheduleRequest", exception.ParamName);
        }

        [Test]
        public void UpdateTaxSchedule_ThrowsArgumentNullException_WhenUpdateTaxScheduleRequestIsNull()
        {
            UpdateTaxScheduleRequest updateTaxScheduleRequest = null;

            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => taxSchedulesService.UpdateTaxSchedule(updateTaxScheduleRequest));
            Assert.AreEqual("taxScheduleRequest", exception.ParamName);
        }

        [Test]
        public async Task AddTaxSchedule_ThrowsNotFoundException_WhenCityDoesNotExist()
        {
            mockUnitOfWork.Setup(uow => uow.CitiesRepository)
                .Returns(mockCitiesRepository.Object);

            var createTaxScheduleRequest = new CreateTaxScheduleRequest
            {
                CityId = 1,
                StartDate = DateTime.Now,
            };

            mockCitiesRepository.Setup(repo => repo.DoesExist(createTaxScheduleRequest.CityId, CancellationToken.None))
                .ReturnsAsync(false);

            var exception = Assert.ThrowsAsync<NotFoundException>(() => taxSchedulesService.AddTaxSchedule(createTaxScheduleRequest));
            StringAssert.Contains($"City with id: {createTaxScheduleRequest.CityId} doesn't exist!", exception.Message);
        }

        [Test]
        public async Task AddTaxSchedule_ThrowsDuplicateException_WhenTaxScheduleAlreadyExists()
        {
            mockUnitOfWork.Setup(uow => uow.CitiesRepository)
                .Returns(mockCitiesRepository.Object);

            mockUnitOfWork.Setup(uow => uow.TaxSchedulesRepository)
                .Returns(mockTaxSchedulesRepository.Object);

            var createTaxScheduleRequest = new CreateTaxScheduleRequest
            {
                CityId = 1,
                StartDate = DateTime.Now,
            };

            mockCitiesRepository.Setup(repo => repo.DoesExist(createTaxScheduleRequest.CityId, CancellationToken.None))
                .ReturnsAsync(true);

            mockTaxSchedulesRepository.Setup(repo =>
                    repo.DoesTaxScheduleExist(
                        createTaxScheduleRequest.CityId,
                        createTaxScheduleRequest.StartDate,
                        createTaxScheduleRequest.PeriodType,
                        null,
                        CancellationToken.None))
                .ReturnsAsync(true);

            var exception = Assert.ThrowsAsync<DuplicateException>(() => taxSchedulesService.AddTaxSchedule(createTaxScheduleRequest));
            StringAssert.Contains($"Tax schedule for city: {createTaxScheduleRequest.CityId} " +
                                 $"on: {createTaxScheduleRequest.StartDate} " +
                                 $"with period: {createTaxScheduleRequest.PeriodType} already exist!", exception.Message);
        }

        [Test]
        public async Task UpdateTaxSchedule_ThrowsNotFoundException_WhenTaxScheduleDoesNotExist()
        {
            mockUnitOfWork.Setup(uow => uow.CitiesRepository)
                .Returns(mockCitiesRepository.Object);

            mockUnitOfWork.Setup(uow => uow.TaxSchedulesRepository)
                .Returns(mockTaxSchedulesRepository.Object);

            var updateTaxScheduleRequest = new UpdateTaxScheduleRequest
            {
                Id = 1,
                CityId = 1,
                StartDate = DateTime.Now,
            };

            mockCitiesRepository.Setup(repo => repo.DoesExist(updateTaxScheduleRequest.CityId, CancellationToken.None))
                .ReturnsAsync(true);

            mockTaxSchedulesRepository.Setup(repo =>
                    repo.DoesTaxScheduleExist(
                        updateTaxScheduleRequest.CityId,
                        updateTaxScheduleRequest.StartDate,
                        updateTaxScheduleRequest.PeriodType,
                        updateTaxScheduleRequest.Id,
                        CancellationToken.None))
                .ReturnsAsync(false);

            mockTaxSchedulesRepository.Setup(repo => repo.GetById(updateTaxScheduleRequest.Id, CancellationToken.None))
                .ReturnsAsync((TaxSchedule)null);

            var exception = Assert.ThrowsAsync<NotFoundException>(() => taxSchedulesService.UpdateTaxSchedule(updateTaxScheduleRequest));
            StringAssert.Contains("Tax schedule is not found!", exception.Message);
        }
    }
}
