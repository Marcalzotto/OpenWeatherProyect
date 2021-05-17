using OpenWeatherAPI.BusinessContracts.Services;
using OpenWeatherAPI.Data.Repositories;
using OpenWeatherAPI.DataContracts.Repositories;
using OpenWeatherAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.Business.Services
{
    /// <summary>
    /// Servicio del repositorio de autenticacion
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        /// <summary>
        /// Inyectamos una instancia de el repositorio de autenticacion
        /// </summary>
        /// <param name="repository">La instancia del repositorio de autenticacion</param>
        public AuthService(IAuthRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Metodo para autenticar al usuario, verifica si existe el usuario buscando el email.
        /// </summary>
        /// <param name="user">Los datos del usuario que se intenta loguear</param>
        /// <returns>Devuelve los datos del usuario que intenta loguearse.</returns>
        public UserDTO AuthUser(LoginDTO user)
        {
            return _repository.AuthUser(user);
        }

        /// <summary>
        /// Metodo para registrar al usuario
        /// </summary>
        /// <param name="user">La informacion del usuario que se registra.</param>
        /// <returns>Los datos del usuario registrado.</returns>
        public UserDTO Create(UserDTO user)
        {
           return  _repository.Create(user);
        }

        /// <summary>
        /// Metodo para verificar si existe un usuario, buscando su correo.
        /// </summary>
        /// <param name="user">Los datos del usuario que intenta registrarse en la aplicacion.</param>
        /// <returns>Devuelve true si existe el usuario o false caso contrario.</returns>
        public bool UserExists(UserForCreateDTO user)
        {
            return _repository.UserExists(user);
        }
    }
}
