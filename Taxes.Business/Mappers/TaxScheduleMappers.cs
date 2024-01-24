using Taxes.Contracts.TaxSchedules.RequestModels;
using Taxes.Contracts.TaxSchedules.ResponseModels;
using Taxes.Database.Models;

namespace Taxes.Business.Mappers
{
    internal static class TaxScheduleMappers
    {
        public static TaxScheduleResponse Map(TaxSchedule schedule)
        {
            return new TaxScheduleResponse()
            {
                Id = schedule.Id,
                CityId = schedule.CityId,
                CityName = schedule.City?.Name,
                Tax = schedule.Tax,
                StartDate = schedule.StartDate,
                EndDate = schedule.EndDate,
                PeriodType = schedule.PeriodType,
            };
        }

        public static TaxSchedule Map(UpdateTaxScheduleRequest taxScheduleRequest, TaxSchedule taxScheduleModel)
        {
            taxScheduleModel.PeriodType = taxScheduleRequest.PeriodType;
            taxScheduleModel.StartDate = taxScheduleRequest.StartDate;
            taxScheduleModel.Tax = taxScheduleRequest.Tax;
            return taxScheduleModel;
        }

        public static TaxSchedule Map(CreateTaxScheduleRequest schedule)
        {
            return new TaxSchedule
            {
                CityId = schedule.CityId,
                StartDate = schedule.StartDate,
                PeriodType = schedule.PeriodType,
                Tax = schedule.Tax
            };
        }
    }
}
