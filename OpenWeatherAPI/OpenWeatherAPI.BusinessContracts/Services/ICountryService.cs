using OpenWeatherAPI.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.BusinessContracts.Services
{
    /// <summary>
    /// Contrato para el servicio del repositorio de paises
    /// </summary>
    public interface ICountryService
    {

        /// <summary>
        /// Metodo para obtener todos los paises disponibles
        /// </summary>
        /// <returns>Lista que contiene todos los pasies disponibles</returns>
        public IEnumerable<CountryDTO> GetCountries();

        /// <summary>
        /// Metodo para verificar si existe un pais
        /// </summary>
        /// <param name="countryId">Id del pais buscado</param>
        /// <returns>Treu si existe o false en caso contrario</returns>
        public bool CountryExists(int countryId);
    }
}
