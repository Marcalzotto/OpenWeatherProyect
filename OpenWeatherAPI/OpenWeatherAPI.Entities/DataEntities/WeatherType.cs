using System;
using System.Collections.Generic;

namespace OpenWeatherAPI.Data.DataEntities
{
    public partial class WeatherType
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public int ConditionId { get; set; }
        public int TypeId { get; set; }

        public virtual WeatherCondition Condition { get; set; }
    }
}
