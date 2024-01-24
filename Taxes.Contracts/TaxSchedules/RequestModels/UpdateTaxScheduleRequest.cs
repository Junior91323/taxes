using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Taxes.Common.Enums;

namespace Taxes.Contracts.TaxSchedules.RequestModels
{
    public class UpdateTaxScheduleRequest
    {
        [Required]
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "cityId")]
        public int CityId { get; set; }

        [Required]
        [JsonProperty(PropertyName = "startDate")]
        public DateTime StartDate { get; set; }
        [Required]
        [JsonProperty(PropertyName = "tax")]
        public double Tax { get; set; }

        [JsonProperty(PropertyName = "periodType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PeriodType PeriodType { get; set; }
    }
}
