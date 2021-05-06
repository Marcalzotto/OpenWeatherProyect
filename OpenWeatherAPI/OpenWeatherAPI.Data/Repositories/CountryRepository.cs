using OpenWeatherAPI.Data.Base;
using OpenWeatherAPI.Data.DataEntities;
using OpenWeatherAPI.DataContracts.Repositories;
using System.Collections.Generic;
using OpenWeatherAPI.Business.Models;
using AutoMapper;

namespace OpenWeatherAPI.Data.Repositories
{
    /// <summary>
    /// Repositorio de paises
    /// </summary>
    public class CountryRepository : BaseRepository<Country>,ICountryRepository
    {
        
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor del repositorio de paises
        /// </summary>
        /// <param name="context">Instancia del DbContext a inyectar en el constructor de la base padre</param>
        /// <param name="mapper">Instancia de automapper para realizar los mapping de entidades de datos a DTO</param>
        public CountryRepository(OpenWeatherDBContext context, IMapper mapper):base(context)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Metodo para obtener todos los paises disponibles del context
        /// </summary>
        /// <returns>Devuelve una lista entidades de datos de pais mapeada a DTO de pais</returns>
        public IEnumerable<CountryDTO> GetCountries()
        {
            var countries = this.GetAll();
            return _mapper.Map<IEnumerable<CountryDTO>>(countries);
        }

        /// <summary>
        /// Metodo para verificar si existe un pais
        /// </summary>
        /// <param name="countryId">Id del pais a buscar.</param>
        /// <returns>Devuelve true si existe o false en caso contrario.</returns>
        public bool CountryExists(int countryId)
        {
            var exists = this.Any(x => x.Id == countryId);
            return exists;
        }
    }
}
