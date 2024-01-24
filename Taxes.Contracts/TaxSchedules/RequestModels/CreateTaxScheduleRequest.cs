using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Taxes.Common.Enums;

namespace Taxes.Contracts.TaxSchedules.RequestModels
{
    public class CreateTaxScheduleRequest
    {
        [Required]
        [JsonProperty(PropertyName = "cityId")]
        public int CityId { get; set; }

        [Required]
        [JsonProperty(PropertyName = "startDate")]
        public DateTime StartDate { get; set; }

        [Required]
        [JsonProperty(PropertyName = "tax")]
        public double Tax { get; set; }

        [Required]
        [JsonProperty(PropertyName = "periodType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PeriodType PeriodType { get; set; }
    }
}
