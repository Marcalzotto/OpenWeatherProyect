using System;
using System.Collections.Generic;

namespace OpenWeatherAPI.Data.DataEntities
{
    public partial class Country
    {
        public Country()
        {
            City = new HashSet<City>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<City> City { get; set; }
    }
}
