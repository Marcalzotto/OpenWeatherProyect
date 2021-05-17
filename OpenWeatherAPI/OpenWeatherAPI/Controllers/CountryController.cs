using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using OpenWeatherAPI.BusinessContracts.Services;


namespace OpenWeatherAPI.Controllers
{

    /// <summary>
    /// Ruta base para el controlador de paises
    /// </summary>
    [Route("api/")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IBranchOfficeService _branchOfficeService;

        public CountryController(
                                 ICountryService countryService,
                                 IBranchOfficeService branchOfficeService
                                )
        {
            _countryService = countryService;
            _branchOfficeService = branchOfficeService;
        }

        /// <summary>
        /// Servicio para obtener todos los paises disponibles.
        /// </summary>
        /// <returns>Devuelve Status 200 con una lista con todos los paises disponibles, 204 si no hay paises disponibles</returns>
        [HttpGet("countries")]
        [Authorize]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetCountries()
        {
            var list = _countryService.GetCountries();
            if (list.Count() == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(list);
            }
        }


        /// <summary>
        /// Servicio para obtener todas las ciudades de un pais que no tiene una oficina asignada
        /// </summary>
        /// <param name="countryId">Id del pais al que pertenecen las ciudades.</param>
        /// <returns>Devuelve Status 404 si no existe el pais buscado, 200 con la lista de ciudades, 204 si no hay ciudades para el pais.</returns>
        [HttpGet("countries/{countryId}/cities")]
        [Authorize]
        public IActionResult GetCitiesWithOutOffice(int countryId)
        {
            if (!_countryService.CountryExists(countryId))
            {
                return NotFound();
            }

            var citiesWithOutOffice = _branchOfficeService.GetCitiesWithOutOffice(countryId);

            if (citiesWithOutOffice.Count() > 0)
            {
                return Ok(citiesWithOutOffice);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
