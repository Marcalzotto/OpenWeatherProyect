using Newtonsoft.Json;


namespace OpenWeatherAPI.Models.Models
{
    public class SnowDTO
    {
        [JsonProperty(PropertyName = "1h")]
        public double SnowVolumeFor1h { get; set; }

        [JsonProperty(PropertyName = "3h")]
        public double SnowVolumeFor3h { get; set; }
    }
}
