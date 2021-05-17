using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OpenWeatherAPI.BusinessContracts.Services;
using OpenWeatherAPI.Models.Models;

namespace OpenWeatherAPI.Controllers
{
    /// <summary>
    /// Ruta base para el controlador de autenticacion
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        public AuthController(
                              IConfiguration configuration,
                              IAuthService authService,
                              IMapper mapper
                             )
        {
            _configuration = configuration;
            _authService = authService;
            _mapper = mapper; 
        }

        /// <summary>
        /// Servicio para login, se intenta recuperar al usuario buscandolo por su mail en primer instancia,
        /// luego se hace el proceso de verificacion de la contraseña comparando el hash existente 
        /// contra el hash generado a traves del password enviado y el salt.
        /// </summary>
        /// <param name="user">Los datos del usuario que intenta loguearse en la aplicacion</param>
        /// <returns>Devuelve Status 400 si el usuario enviado no tiene datos, 401 en caso de que no se pueda autenticar y 
        /// 200 en caso de que se logre autenticar al usuario.
        /// </returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginDTO user) 
        {
            if (user == null) 
            {
                return BadRequest();
            }

            var authUser = _authService.AuthUser(user);

            if (authUser != null)
            {
                string hash = authUser.Password;
                string plainPassword = user.Password;
                string salt = authUser.Salt;
                plainPassword += salt; 

                if (Crypto.VerifyHashedPassword(hash, plainPassword))
                    return BuildToken(authUser);
                else
                    return Unauthorized();
            }
            else 
            {
                return Unauthorized();
            }
            
        }

        /// <summary>
        /// Servicio para crear un usuario, se procede a generar un hash con el password enviado por el usuario
        /// aplicando a la formula un salt generado por la libreria Crypto, esto da como resultado un hash,
        /// por ultimo se persiste la informacion del usuario junto con el hash y el salt utilizado.
        /// </summary>
        /// <param name="UserForCreate">Recibe los datos del usuario que intenta registrarse en la aplicacion</param>
        /// <returns>Devuelve Status 400 si el usuario no tiene datos, 400 si ya existe un usuario con 
        /// el mismo correo electronico que se intenta registrar, 500 si hay algun fallo al intentar crear
        /// al usuario y 200 en caso de exito
        /// </returns>
        [HttpPost("user")]
        public IActionResult CreateUser(UserForCreateDTO UserForCreate) 
        {
            if(User == null)
            {
                return BadRequest();
            }

            if (_authService.UserExists(UserForCreate))
            {
                return BadRequest(User);
            }
            else
            {
                
                string salt = Crypto.GenerateSalt();
                string password = UserForCreate.Password + salt;
                string hash = Crypto.HashPassword(password);

                UserForCreate.Password = hash;
                var User = _mapper.Map<UserDTO>(UserForCreate);
                User.Salt = salt;

                if (_authService.Create(User) != null)
                    return Ok();
                else
                    return StatusCode(500, "User could not be created");
            }
        }

        /// <summary>
        /// Este es el metodo ultilizado para generar el JsonWebToken una vez el usuario se ha autenticado
        /// en la aplicacion, se generan los claims que iran como parte del jwt, luego se genera el jwt
        /// utilizando el conjunto de parametros establecidos para la generacion del mismo, por ultimo
        /// se emite un status 200 que devuelve un objeto con el token y su expiracion.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private IActionResult BuildToken(UserDTO user) 
        {
            //informacion confiable del usuario.
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.Name)
            };

            var key = _configuration.GetSection("JwtConfig").GetSection("SecrectScurityKey").Value; 
            var jwtParamConf = _configuration.GetSection("JwtConfig").GetSection("IssuerAudience").Value;

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var singinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(Int32.Parse(_configuration.GetSection("JwtConfig").GetSection("TokenDuration").Value));

            var tokenOptions = new JwtSecurityToken(
                                    issuer: jwtParamConf,
                                    audience: jwtParamConf,
                                    claims: claims,
                                    expires: expiration,
                                    signingCredentials: singinCredentials
                               );

            return Ok( new
            {
                token = new JwtSecurityTokenHandler().WriteToken(tokenOptions),
                expiration = expiration
            });
        }
    }
}
