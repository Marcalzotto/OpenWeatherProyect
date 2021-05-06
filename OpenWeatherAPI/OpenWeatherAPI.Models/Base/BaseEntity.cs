using OpenWeatherAPI.ModelContracts.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.Business.Base
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
