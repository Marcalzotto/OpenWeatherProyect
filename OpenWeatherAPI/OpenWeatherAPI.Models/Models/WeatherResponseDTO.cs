using OpenWeatherAPI.Business.Base;
using OpenWeatherAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.Models.Models
{
    public class WeatherResponseDTO : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<WeatherDescriptionDTO> Weather { get; set; }
        public MainDTO Main { get; set; }
        public CoordsDTO Coord { get; set; }
        public string Base { get; set; }
        public float Visibility { get; set; }
        public CloudsDTO Clouds { get; set; }
        public int Dt { get; set; }
        public SysDTO Sys { get; set; }
        public int TimeZone { get; set; }
        public int Cod { get; set; }
        public RainDTO Rain { get; set; }
        public SnowDTO Snow {get;set;}
        public WindDTO Wind { get; set; }
    }
}
