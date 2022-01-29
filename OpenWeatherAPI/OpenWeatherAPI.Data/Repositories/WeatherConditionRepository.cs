using OpenWeatherAPI.Business.Models;
using OpenWeatherAPI.Data.Base;
using OpenWeatherAPI.Data.DataEntities;
using OpenWeatherAPI.DataContracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OpenWeatherAPI.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OpenWeatherAPI.Data.Repositories
{
    /// <summary>
    /// Repositorio de clima
    /// </summary>
    public class WeatherConditionRepository : BaseRepository<WeatherCondition>,IWeatherConditionRepository
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _country;

        /// <summary>
        /// Constructor del repositorio del clima
        /// </summary>
        /// <param name="context">Instancia del DbContext a inyectar en el constructor de la base padre</param>
        /// <param name="mapper">Instancia de automapper para realizar los mapping de entidades de datos a DTO</param>
        public WeatherConditionRepository(OpenWeatherDBContext context,
                                          IMapper mapper,
                                          ICountryRepository country
                                          ) :base(context)
        {
            _mapper = mapper;
            _country = country;
        }

        /// <summary>
        /// Metodo para obtener las condiciones climaticas.
        /// </summary>
        /// <param name="weatherConditions">Recibe una lista de condiciones climaticas obtenidas de la aplicacion del clima, que se registraran en la base de datos</param>
        /// <returns>Devuelve la lista de condiciones climaticas actuales</returns>
        public IEnumerable<WeatherConditionDTO> GetWeatherConditions(IEnumerable<WeatherResponseDTO> weather)
        {
            List<WeatherCondition> collection = new List<WeatherCondition>();
            var weatherCollection = _mapper.Map<IEnumerable<WeatherCondition>>(weather);


            foreach (var item in weatherCollection)
            {
                _context.WeatherConditions.Add(item);
                this.CommitChanges();

                var id = item.Id;
                var foundedItem = _context.WeatherConditions.Include(c => c.WeatherTypes)
                                                            .Include(c => c.City)
                                                            .ThenInclude(ci => ci.BranchOffice)
                                                            .Where(c => c.Id == id)
                                                            .OrderBy(c => c.City.BranchOffice.Description).ToList();

                collection = collection.Concat(foundedItem).ToList();
            }

            return _mapper.Map<IEnumerable<WeatherConditionDTO>>(collection);

        }

        /// <summary>
        /// Metodo para obtener las condiciones climaticas historicas
        /// </summary>
        /// <param name="cities">Lista con ids de ciudades de las cuales se buscara una condicion climatica</param>
        /// <param name="dateFrom">Fecha minima de registracion de una condicion climatica</param>
        /// <param name="dateTo">Fecha maxima de registracion de una condicion climatica</param>
        /// <returns>Devuelve la lista de condiciones climaticas registradas historicamente.</returns>
        public IEnumerable<WeatherConditionDTO> GetWeatherConditionsHist(IEnumerable<int> cities, DateTime dateFrom, DateTime dateTo)
        {
            List<WeatherCondition> collection = new List<WeatherCondition>();

            dateTo = dateTo.AddDays(1);

            foreach (var cityId in cities)
            {
                var conditions = _context.WeatherConditions.Include(c => c.WeatherTypes)
                                                          .Include(c => c.City)
                                                          .ThenInclude(ci => ci.BranchOffice)
                                                          .Where(c => c.CityId == cityId)
                                                          .Where(c => c.RegDate >= dateFrom)
                                                          .Where(c => c.RegDate <= dateTo)
                                                          .OrderBy(c => c.RegDate).ToList();


                collection = collection.Concat(conditions).ToList();
            }

            return _mapper.Map<IEnumerable<WeatherConditionDTO>>(collection);
        }
    }
}
