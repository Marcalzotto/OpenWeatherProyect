using OpenWeatherAPI.Business.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.Business.Models
{
    public class CityDTO : BaseEntity
    {
        public string Name { get; set; }
        public string State { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int CountryId { get; set; }
        public CountryDTO Country { get; set; }
        public BranchOfficeDTO BranchOffice { get; set; }
        //public ICollection<WeatherConditionDTO> WeatherCondition { get; set; }

    }
}
