using AutoMapper;
using OpenWeatherAPI.Data.Base;
using OpenWeatherAPI.Data.DataEntities;
using OpenWeatherAPI.DataContracts.Repositories;
using OpenWeatherAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenWeatherAPI.Data.Repositories
{
    public class AuthRepository : BaseRepository<User>,IAuthRepository
    {

        private readonly IMapper _mapper;
        public AuthRepository(OpenWeatherDBContext context, IMapper mapper):base(context)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Metodo para autenticar al usuario, verifica si existe el usuario buscando el email,
        /// si existe recupera todos los datos del usuario.
        /// </summary>
        /// <param name="user">Los datos del usuario que se intenta loguear</param>
        /// <returns>Devuelve los datos del usuario que intenta loguearse o null si no se encuentra.</returns>
        public UserDTO AuthUser(LoginDTO user)
        {
            var exists = _context.Users.Any(x => x.Email.Equals(user.Email));
            if (exists)
            {
                var authUser = this.Get(x => x.Email.Equals(user.Email));
                return _mapper.Map<UserDTO>(authUser);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Metodo para registrar al usuario
        /// </summary>
        /// <param name="user">La informacion del usuario que se registra.</param>
        /// <returns>Los datos del usuario registrado.</returns>
        public UserDTO Create(UserDTO user)
        {
            var userToAdd = _mapper.Map<User>(user);
            var addedUser = this.Add(userToAdd);
            this.CommitChanges();
            return _mapper.Map<UserDTO>(addedUser);
        }

        /// <summary>
        /// Metodo para verificar si existe un usuario, buscando su correo.
        /// </summary>
        /// <param name="user">Los datos del usuario que intenta registrarse en la aplicacion.</param>
        /// <returns>Devuelve true si existe el usuario o false caso contrario.</returns>
        public bool UserExists(UserForCreateDTO user)
        {
            return _context.Users.Any(x => x.Email.Equals(user.Email));
        }
    }
}
