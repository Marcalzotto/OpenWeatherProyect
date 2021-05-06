using Newtonsoft.Json;
using OpenWeatherAPI.Business.Models;
using OpenWeatherAPI.BusinessContracts.Services;
using OpenWeatherAPI.DataContracts.Repositories;
using OpenWeatherAPI.Models.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherAPI.Business.Services
{
    public class WeatherConditionService : IWeatherConditionService
    {
        private readonly IWeatherConditionRepository _repository;

        public WeatherConditionService(IWeatherConditionRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<WeatherConditionDTO> Get(IEnumerable<int> cities, DateTime dateFrom, DateTime dateTo)
        {
            return _repository.GetWeatherConditionsHist(cities, dateFrom, dateTo);
        }

        public IEnumerable<WeatherConditionDTO> GetConditions(IEnumerable<WeatherResponseDTO> conditions)
        {
            return _repository.GetWeatherConditions(conditions);
        }
    }
}
