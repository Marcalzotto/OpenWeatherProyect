using OpenWeatherAPI.Business.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.Models.Models
{
    public class SysDTO : BaseEntity
    {
        public string Type { get; set; }
        public string Country { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }
}
