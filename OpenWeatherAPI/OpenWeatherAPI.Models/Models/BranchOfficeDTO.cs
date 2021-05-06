using OpenWeatherAPI.Business.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.Business.Models
{
    public class BranchOfficeDTO : BaseEntity
    {
        public string Description { get; set; }
        public int CityId { get; set; }
        public CityDTO City { get; set; }
    }
}
