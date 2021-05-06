using Newtonsoft.Json;


namespace OpenWeatherAPI.Models.Models
{
    public class RainDTO
    {
        [JsonProperty(PropertyName = "1h")]
        public double RainVolumeFor1h {get;set; }

        [JsonProperty(PropertyName = "3h")]
        public double RainVolumeFor3h { get; set; }
    }
}
