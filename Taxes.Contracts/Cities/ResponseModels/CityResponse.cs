using Newtonsoft.Json;

namespace Taxes.Contracts.Cities.ResponseModels
{
    public class CityResponse
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
