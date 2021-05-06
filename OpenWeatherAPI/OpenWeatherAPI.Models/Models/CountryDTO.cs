using OpenWeatherAPI.Business.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.Business.Models
{
    public class CountryDTO : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<CityDTO> City { get; set; }
    }
}
