using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Taxes.Common.Enums;

namespace Taxes.Contracts.TaxSchedules.ResponseModels
{
    public class TaxScheduleResponse
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "cityId")]
        public int CityId { get; set; }

        [JsonProperty(PropertyName = "city")]
        public string CityName { get; set; }

        [JsonProperty(PropertyName = "tax")]
        public double Tax { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty(PropertyName = "endDate")]
        public DateTime EndDate { get; set; }

        [JsonProperty(PropertyName = "periodType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PeriodType PeriodType { get; set; }
    }
}
