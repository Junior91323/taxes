using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Taxes.Contracts.Cities.RequestModels
{
    public class CreateCityRequest
    {
        [Required]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
