using Taxes.Business.Mappers;
using Taxes.Business.Services.Taxes.Abstract;
using Taxes.Common.Enums;
using Taxes.Common.Exceptions;
using Taxes.Common.Helpers;
using Taxes.Common.Models.Paging;
using Taxes.Common.Models.Responses;
using Taxes.Contracts.TaxSchedules.RequestModels;
using Taxes.Contracts.TaxSchedules.ResponseModels;
using Taxes.Database;

namespace Taxes.Business.Services.Taxes
{
    public class TaxSchedulesService : ITaxSchedulesService
    {
        private readonly ITaxSchedulesUnitOfWork taxSchedulesUnitOfWork;

        public TaxSchedulesService(ITaxSchedulesUnitOfWork taxSchedulesUnitOfWork)
        {
            this.taxSchedulesUnitOfWork = taxSchedulesUnitOfWork;
        }

        public async Task<TaxScheduleResponse> GetTaxScheduleForDateByCityId(int cityId, DateTime date, CancellationToken ct = default)
        {
            var taxSchedule =
              await this.taxSchedulesUnitOfWork.TaxSchedulesRepository.GetTaxScheduleForDateByCityId(cityId, date, ct);

            if (taxSchedule == null)
                return null;

            return TaxScheduleMappers.Map(taxSchedule);
        }

        public async Task<ListResponse<TaxScheduleResponse>> GetTaxSchedulesByCityId(int cityId, PageSortInfo sortInfo, CancellationToken ct = default)
        {
            sortInfo ??= new PageSortInfo();

            var taxSchedules =
                await this.taxSchedulesUnitOfWork.TaxSchedulesRepository.GetTaxSchedulesByCityId(cityId, sortInfo, ct);

            return new ListResponse<TaxScheduleResponse> { TotalCount = taxSchedules.TotalCount, Data = taxSchedules.Data.Select(TaxScheduleMappers.Map) };
        }

        public async Task<TaxScheduleResponse> AddTaxSchedule(CreateTaxScheduleRequest taxScheduleRequest, CancellationToken ct = default)
        {
            await ValidateCreating(taxScheduleRequest, ct);

            var taxSchedule = TaxScheduleMappers.Map(taxScheduleRequest);

            taxSchedule.EndDate = DateHelper.GetEndDateByPeriodType(taxSchedule.StartDate, taxSchedule.PeriodType);

            await this.taxSchedulesUnitOfWork.TaxSchedulesRepository.Add(taxSchedule);

            await this.taxSchedulesUnitOfWork.SaveChangesAsync(ct);

            return TaxScheduleMappers.Map(taxSchedule);
        }

        public async Task<TaxScheduleResponse> UpdateTaxSchedule(UpdateTaxScheduleRequest taxScheduleRequest, CancellationToken ct = default)
        {
            await ValidateUpdating(taxScheduleRequest, ct);

            var taxSchedule = await this.taxSchedulesUnitOfWork.TaxSchedulesRepository.GetById(taxScheduleRequest.Id, ct);

            TaxScheduleMappers.Map(taxScheduleRequest, taxSchedule);

            taxSchedule.EndDate = DateHelper.GetEndDateByPeriodType(taxSchedule.StartDate, taxSchedule.PeriodType);
            taxSchedule.UpdatedDate = DateTime.Now;

            this.taxSchedulesUnitOfWork.TaxSchedulesRepository.Update(taxSchedule);
            await this.taxSchedulesUnitOfWork.SaveChangesAsync(ct);

            return TaxScheduleMappers.Map(taxSchedule);
        }

        public async Task DeleteTaxSchedule(int id, CancellationToken ct = default)
        {
            var taxSchedule = await this.taxSchedulesUnitOfWork.TaxSchedulesRepository.GetById(id, ct);

            if (taxSchedule != null)
                this.taxSchedulesUnitOfWork.TaxSchedulesRepository.Remove(taxSchedule);

            await this.taxSchedulesUnitOfWork.SaveChangesAsync(ct);
        }

        private async Task ValidateCreating(CreateTaxScheduleRequest taxScheduleRequest, CancellationToken ct = default)
        {
            if (taxScheduleRequest == null)
                throw new ArgumentNullException(nameof(taxScheduleRequest));

            await Validate(taxScheduleRequest.CityId, taxScheduleRequest.StartDate,
                taxScheduleRequest.PeriodType, null, ct);
        }

        private async Task ValidateUpdating(UpdateTaxScheduleRequest taxScheduleRequest, CancellationToken ct = default)
        {
            if (taxScheduleRequest == null)
                throw new ArgumentNullException(nameof(taxScheduleRequest));

            // Check if tax schedule doesn't exist
            var existingTaxSchedule = await this.taxSchedulesUnitOfWork.TaxSchedulesRepository.GetById(taxScheduleRequest.Id, ct);

            if (existingTaxSchedule == null)
                throw new NotFoundException("Tax schedule is not found!");

            await Validate(taxScheduleRequest.CityId, taxScheduleRequest.StartDate,
                taxScheduleRequest.PeriodType, taxScheduleRequest.Id, ct);
        }


        private async Task Validate(int cityId, DateTime startDate, PeriodType periodType, int? taxScheduleId = null, CancellationToken ct = default)
        {
            // Check if city doesn't exist
            if (!await this.taxSchedulesUnitOfWork.CitiesRepository.DoesExist(cityId, ct))
            {
                throw new NotFoundException($"City with id: {cityId} doesn't exist!");
            }

            // Check if tax schedule already exist for the same date, city and period
            if (await this.taxSchedulesUnitOfWork.TaxSchedulesRepository.DoesTaxScheduleExist(cityId, startDate, periodType, taxScheduleId, ct))
            {
                throw new DuplicateException($"Tax schedule for city: {cityId} " +
                                             $"on: {startDate} " +
                                             $"with period: {periodType} already exist!");
            }
        }
    }
}
