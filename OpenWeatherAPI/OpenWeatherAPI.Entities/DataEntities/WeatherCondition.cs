using System;
using System.Collections.Generic;

#nullable disable

namespace OpenWeatherAPI.Data.DataEntities
{
    public partial class WeatherCondition
    {
        public WeatherCondition()
        {
            WeatherTypes = new HashSet<WeatherType>();
        }

        public int Id { get; set; }
        public string Base { get; set; }
        public double Temperature { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public double FeelsLike { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int? SeaLevel { get; set; }
        public int? GroundLevel { get; set; }
        public double WindSpeed { get; set; }
        public int WindDegrees { get; set; }
        public double? WindGust { get; set; }
        public int Clouds { get; set; }
        public double? RainVolume1h { get; set; }
        public double? RainVolume3h { get; set; }
        public double? SnowVolume1h { get; set; }
        public double? SnowVolume3h { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
        public int Timezone { get; set; }
        public int Dt { get; set; }
        public int CityId { get; set; }
        public int StatusCode { get; set; }
        public DateTime RegDate { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<WeatherType> WeatherTypes { get; set; }
    }
}
