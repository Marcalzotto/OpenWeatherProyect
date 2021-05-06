using OpenWeatherAPI.Business.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.Business.Models
{
    public class WeatherTypeDTO : BaseEntity
    {
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int ConditionId { get; set; }

        public WeatherConditionDTO Condition { get; set; }
    }
}
