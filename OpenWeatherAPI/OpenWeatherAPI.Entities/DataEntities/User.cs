using System;
using System.Collections.Generic;

#nullable disable

namespace OpenWeatherAPI.Data.DataEntities
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
