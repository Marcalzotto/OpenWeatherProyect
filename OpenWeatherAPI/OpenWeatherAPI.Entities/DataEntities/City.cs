using System;
using System.Collections.Generic;

namespace OpenWeatherAPI.Data.DataEntities
{
    public partial class City
    {
        public City()
        {
            WeatherCondition = new HashSet<WeatherCondition>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual BranchOffice BranchOffice { get; set; }
        public virtual ICollection<WeatherCondition> WeatherCondition { get; set; }
    }
}
