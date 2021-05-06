using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using OpenWeatherAPI.BusinessContracts.Services;
using Newtonsoft.Json;
using OpenWeatherAPI.Models.Models;
using System.Net;
using System.IO;

namespace OpenWeatherAPI.Controllers
{
    /// <summary>
    /// Ruta base para el controlador del clima
    /// </summary>
    [Route("api/")]
    [ApiController]
    public class WeatherController : Controller
    {
        private readonly IWeatherConditionService _weatherConditionService;
        private readonly IConfiguration _configuration;
        public WeatherController(
                                IWeatherConditionService weatherConditionService,
                                IConfiguration configuration
                                )
        {
            _weatherConditionService = weatherConditionService;
            _configuration = configuration; 
        }

       /// <summary>
       /// Servicio para obtener las condiciones climaticas actuales 
       /// </summary>
       /// <param name="cities">un arreglo con id de las ciudades para las cuales se consultara a la api del clima la condicion climatica actual</param>
       /// <returns>devuelve un status 200 con la informacion de las condiciones climaticas o 204 si no se obtuvo informacion.</returns>
        [HttpGet("weather-conditions")]
        public IActionResult GetWeatherConditions([FromQuery(Name = "id")] int[] cities) 
        {
            var ApiUrl = _configuration.GetSection("ApiSettings").GetSection("ApiUrl").Value;
            var ApiKey = _configuration.GetSection("ApiSettings").GetSection("ApiKey").Value;
            string responseContent = string.Empty;

            List<WeatherResponseDTO> collection = new List<WeatherResponseDTO>();

            foreach (var cityId in cities)
            {
                var formattedUrl = string.Format(ApiUrl, cityId, ApiKey);
                Uri uri = new Uri(formattedUrl);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream stream = response.GetResponseStream();
                        StreamReader sr = new StreamReader(stream);
                        responseContent = sr.ReadToEnd();
                        stream.Close();
                        sr.Close();
                    }
                }

                var serializedResponse = JsonConvert.DeserializeObject<WeatherResponseDTO>(responseContent);
               
                collection.Add(serializedResponse);
            }
            
            if (collection.Count() > 0)
            {
                var conditions = _weatherConditionService.GetConditions(collection);
                return Ok(conditions);
            }
            else 
            {
                return NoContent();
            }
        }

        /// <summary>
        /// Devuelve las condiciones climaticas historicas por ciudad
        /// </summary>
        /// <param name="cities">arreglo que contiene las ciudades para las cuales se consultara el clima</param>
        /// <param name="dateFrom">Fecha minima de registracion de una condicion climatica</param>
        /// <param name="dateTo">Fecha maxima de registracion de una condicion climatica</param>
        /// <returns></returns>
        [HttpGet("weather-conditions/history")]
        public IActionResult GetWeatherConditionsHist([FromQuery(Name = "id")] int[] cities, [FromQuery(Name = "dateFrom")] DateTime dateFrom, [FromQuery(Name = "dateTo")] DateTime dateTo) 
        {
            var list = _weatherConditionService.Get(cities, dateFrom, dateTo);
            return Ok(list);
        }

    }
}
