using System.ComponentModel.DataAnnotations.Schema;

namespace Taxes.Database.Models
{
    [Table("Cities")]
    public class City : BaseEntityModel
    {
        public string Name { get; set; }

        public IEnumerable<TaxSchedule> TaxSchedules { get; set; } = new List<TaxSchedule>();
    }
}
