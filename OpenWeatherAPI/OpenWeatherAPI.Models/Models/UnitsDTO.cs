using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.Models.Models
{
    public class UnitsDTO
    {
        public double TempDefault { get; set; }
        public double TempMetrics { get; set; }
        public double TempImperial { get; set; }
        public double TempMinDefault { get; set; }
        public double TempMinMetrics { get; set; }
        public double TempMinImperial { get; set; }
        public double TempMaxDefault { get; set; }
        public double TempMaxMetrics { get; set; }
        public double TempMaxImperial { get; set; }
        public double FeelsLikeDefault { get; set; }
        public double FeelsLikeMetrics { get; set; }
        public double FeelsLikeImperial { get; set; }

    }
}
