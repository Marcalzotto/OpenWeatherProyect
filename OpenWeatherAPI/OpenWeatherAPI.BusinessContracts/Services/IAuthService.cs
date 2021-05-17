using OpenWeatherAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.BusinessContracts.Services
{
    public interface IAuthService
    {
        public bool UserExists(UserForCreateDTO user);

        public UserDTO AuthUser(LoginDTO user);

        public UserDTO Create(UserDTO user);
    }
}
