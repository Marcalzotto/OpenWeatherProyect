using OpenWeatherAPI.Business.Models;
using OpenWeatherAPI.BusinessContracts.Services;
using OpenWeatherAPI.DataContracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenWeatherAPI.Business.Services
{   
    /// <summary>
    /// Servicio del repositorio de paises
    /// </summary>
    public class CountryService : ICountryService
    {

        private readonly ICountryRepository _repository;
        /// <summary>
        /// Inyectamos una instancia de el repositorio de paises
        /// </summary>
        /// <param name="repository">La instancia del repositorio de paises</param>
        public CountryService(ICountryRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Metodo para verificar si existe un pais
        /// </summary>
        /// <param name="countryId">Id del pais buscado</param>
        /// <returns>Treu si existe o false en caso contrario</returns>
        public bool CountryExists(int countryId)
        {
            return _repository.CountryExists(countryId);
        }

        /// <summary>
        /// Metodo para obtener todos los paises disponibles
        /// </summary>
        /// <returns>Lista que contiene todos los pasies disponibles</returns>
        public IEnumerable<CountryDTO> GetCountries()
        {
            return _repository.GetCountries();
        }
    }
}
