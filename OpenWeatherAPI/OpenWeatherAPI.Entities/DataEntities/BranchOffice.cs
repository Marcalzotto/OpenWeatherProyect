using System;
using System.Collections.Generic;

namespace OpenWeatherAPI.Data.DataEntities
{
    public partial class BranchOffice
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CityId { get; set; }

        public virtual City City { get; set; }
    }
}
