using System;
using System.Collections.Generic;
using System.Text;
using OpenWeatherAPI.Business.Base;


namespace OpenWeatherAPI.Models.Models
{
    public class UserDTO:BaseEntity
    {
         public string Name { get; set; }
         public string Surname { get; set; }
         public string Email { get; set; }
         public DateTime BirthDate { get; set; }
         public string Password { get; set; }
         public string Salt { get; set; }
    }
}
