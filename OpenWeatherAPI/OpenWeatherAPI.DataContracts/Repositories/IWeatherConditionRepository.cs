using OpenWeatherAPI.Business.Models;
using OpenWeatherAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherAPI.DataContracts.Repositories
{
    /// <summary>
    /// Contrato para el repositorio del clima
    /// </summary>
    public interface IWeatherConditionRepository
    {
        /// <summary>
        /// Metodo para obtener las condiciones climaticas historicas
        /// </summary>
        /// <param name="cities">Lista con ids de ciudades de las cuales se buscara una condicion climatica</param>
        /// <param name="dateFrom">Fecha minima de registracion de una condicion climatica</param>
        /// <param name="dateTo">Fecha maxima de registracion de una condicion climatica</param>
        /// <returns>Devuelve la lista de condiciones climaticas registradas historicamente.</returns>
        public IEnumerable<WeatherConditionDTO> GetWeatherConditionsHist(IEnumerable<int> cities, DateTime dateFrom, DateTime dateTo);

        /// <summary>
        /// Metodo para obtener las condiciones climaticas.
        /// </summary>
        /// <param name="weatherConditions">Recibe una lista de condiciones climaticas obtenidas de la aplicacion del clima, que se registraran en la base de datos</param>
        /// <returns>Devuelve la lista de condiciones climaticas actuales</returns>
        public IEnumerable<WeatherConditionDTO> GetWeatherConditions(IEnumerable<WeatherResponseDTO> weatherConditions);
   
    }
}
