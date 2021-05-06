using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OpenWeatherAPI.BusinessContracts.Services;
using OpenWeatherAPI.Business.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace OpenWeatherAPI.Controllers
{
    /// <summary>
    /// Ruta base del controlador de oficinas
    /// </summary>
    [Route("api/")]
    [ApiController]
    
    public class OfficesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IBranchOfficeService _branchOfficeService;
       
        public OfficesController(
                                 ICountryService countryService,
                                 IBranchOfficeService branchOfficeService
                                )
        {
            _countryService = countryService;
            _branchOfficeService = branchOfficeService;
        }

        /// <summary>
        /// Servicio para traer las oficinas de un pais.
        /// </summary>
        /// <param name="countryId">Id del pais al que pertenecen las oficinas</param>
        /// <returns>Devuelve status 200 con las oficinas, 404 si no exixte el pais buscado, 204 si no hay oficinas registradas para ese pais.</returns>
        [HttpGet("offices")]
        public IActionResult GetOfficesByCountry([FromQuery(Name ="countryId")]int countryId, [FromQuery(Name="includeCities")]bool includeCities)
        {
            var exists = _countryService.CountryExists(countryId);
            if (exists)
            {
                var officesForCountry = _branchOfficeService.GetByCountryId(countryId, includeCities);
                if (officesForCountry.Count() > 0)
                    return Ok(officesForCountry);
                else
                    return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Servicio para obtener la informacion de una oficina
        /// </summary>
        /// <param name="id">Id de la oficina a buscar</param>
        /// <returns>Devuelve Status 200 con la informacion de la oficina, 404 si no existe la oficina buscada.</returns>
        [HttpGet("offices/{id}")]
        public IActionResult GetOfficeById(int id)
        {
            var exists = _branchOfficeService.OfficeExists(id);
            if (exists)
            {
                var office = _branchOfficeService.GetById(id);
                return Ok(office);
            }
            else
               return NotFound();
            
        }

        /// <summary>
        /// Servicio para crear una nueva oficina
        /// </summary>
        /// <param name="office">Recibe la nueva oficina a asignar a una ciudad</param>
        /// <returns>Devuelve Status 400 si la oficina a crear no tiene datos, 404 si no existe la ciudad o 200 con la informacion de la nueva oficina.</returns>
        [HttpPost("offices")]
        public IActionResult CreateOffice([FromBody] BranchOfficeDTO office) 
        {
            if (office == null)
                return BadRequest();

            var cityId = office.CityId;

            if (!_branchOfficeService.CityExists(cityId)) 
            {
                return NotFound();
            }

            if (_branchOfficeService.OfficeExistsByCityId(cityId)) 
            {
                return BadRequest();
            }
            else
            {
                var newOffice = _branchOfficeService.Create(office);
                return Ok(newOffice);
            }
        }

        /// <summary>
        /// Servicio para actualizar parcialmente la informacion de una oficina
        /// </summary>
        /// <param name="officeId">Id de la oficina a actualizar</param>
        /// <param name="patchDocument">El array con objetos json que determinan las operaciones a realizar sobre determinados campos del registro</param>
        /// <returns>status 400 el objeto de operacion patch es nulo, 404 si no se encuentra la oficina, 200 si la operacion es exitosa, 500 si hay algun error al intentar actualizar el registro.</returns>
        [HttpPatch("offices/{officeId}")]
        public IActionResult UpdateOffice(int officeId, [FromBody] JsonPatchDocument<BranchOfficeForUpdateDTO> patchDocument)
        {
            
            if(patchDocument == null) 
            {
                 return BadRequest();
            }


            if (!_branchOfficeService.OfficeExists(officeId)) 
            {
                return NotFound();
            }

            var officeToUpdate = this._branchOfficeService.GetForUpdateById(officeId);

            patchDocument.ApplyTo(officeToUpdate, ModelState);

            if (_branchOfficeService.Update(officeId ,officeToUpdate) >= 0)
            {
                return Ok();
            }
            else 
            {
                return StatusCode(500, "Error trying to update office");
            }

            
        }

        /// <summary>
        /// Servicio utilizado para eliminar una oficina
        /// </summary>
        /// <param name="officeId">Recibe el Id de oficina de la oficina a eliminar</param>
        /// <returns>Devuelve Status 404 si la oficina no existe, o 500 si hay algun error al intentar borrar el registro, 200 si la operacion fue exitosa.</returns>
        [HttpDelete("offices/{officeId}")]
        public IActionResult DeleteOffice([FromRoute] int officeId) 
        {
            if (!_branchOfficeService.OfficeExists(officeId)) 
            {
                return NotFound();
            }

            if (!(_branchOfficeService.Delete(officeId)>=0)) 
            {
                return StatusCode(500, "Please verify your data");
            }

            return Ok();
        }
    }
}
