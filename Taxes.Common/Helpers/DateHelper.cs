using Taxes.Common.Enums;

namespace Taxes.Common.Helpers
{
    public static class DateHelper
    {
        public static DateTime GetEndDateByPeriodType(DateTime startDate, PeriodType periodType)
        {
            return periodType switch
            {
                PeriodType.Year => startDate.AddYears(1),
                PeriodType.Month => startDate.AddMonths(1),
                PeriodType.Week => startDate.AddDays(7),
                PeriodType.Day => startDate,
                _ => startDate,
            };
        }
    }
}
