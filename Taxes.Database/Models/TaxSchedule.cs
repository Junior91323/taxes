using System.ComponentModel.DataAnnotations.Schema;
using Taxes.Common.Enums;

namespace Taxes.Database.Models
{
    [Table("TaxSchedules")]
    public class TaxSchedule : BaseEntityModel
    {
        public int CityId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public PeriodType PeriodType { get; set; }

        public double Tax { get; set; }

        public City City { get; set; }
    }
}
