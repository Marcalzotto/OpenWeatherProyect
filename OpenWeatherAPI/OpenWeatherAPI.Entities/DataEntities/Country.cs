using System;
using System.Collections.Generic;

#nullable disable

namespace OpenWeatherAPI.Data.DataEntities
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
