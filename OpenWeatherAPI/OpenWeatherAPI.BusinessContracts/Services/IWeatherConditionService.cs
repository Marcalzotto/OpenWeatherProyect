using OpenWeatherAPI.Business.Models;
using OpenWeatherAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherAPI.BusinessContracts.Services
{
    public interface IWeatherConditionService
    {
        public IEnumerable<WeatherConditionDTO> Get(IEnumerable<int> cities, DateTime dateFrom, DateTime dateTo);

        public IEnumerable<WeatherConditionDTO> GetConditions(IEnumerable<WeatherResponseDTO> conditions);
    }
}
