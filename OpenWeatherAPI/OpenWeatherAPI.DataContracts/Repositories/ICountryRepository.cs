using OpenWeatherAPI.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.DataContracts.Repositories
{
    /// <summary>
    /// Contrato del repositorio de paises
    /// </summary>
    public interface ICountryRepository
    {
        /// <summary>
        /// Metodo para obtener todos los paises disponibles del context
        /// </summary>
        /// <returns>Devuelve una lista entidades de datos de pais mapeada a DTO de pais</returns>
        public IEnumerable<CountryDTO> GetCountries();

        /// <summary>
        /// Metodo para verificar si existe un pais
        /// </summary>
        /// <param name="countryId">Id del pais a buscar.</param>
        /// <returns>Devuelve true si existe o false en caso contrario.</returns>
        public bool CountryExists(int countryId);
    }
}
