using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.Models.Models
{
    public class WindDTO
    {
        public double Speed { get; set; }

        public int Deg { get; set; }

        public double Gust { get; set; }
    }
}
