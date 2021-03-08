using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherOrNot.Logic.Factory;
using WeatherOrNot.Logic.Model;

namespace WeatherOrNot.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IWeatherFactory _wf;

        public WeatherController(ILogger<WeatherController> logger, IWeatherFactory wf)
        {
            _logger = logger;
            _wf = wf;
        }

        /// <summary>
        /// Get the full index of observations available on weather.gov
        /// </summary>
        /// <param name="useCache">boolean parameter to force a fresh grab rather than a cached version</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetObservationIndex")]
        public async Task<IActionResult> GetObservationIndex(bool useCache = true)
        {
            var observation = await _wf.GetObservationIndex(useCache);

            return Ok(JsonSerializer.Serialize(observation));
        }

        /// <summary>
        /// Returns information from a specific observation site
        /// </summary>
        /// <param name="stationId">Station ID from weather.gov</param>
        /// <param name="useCache">boolean parameter to force a fresh grab rather than a cached version</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetObservationByStationId")]
        public async Task<IActionResult> GetObservationByStationId(string stationId, bool useCache = true)
        {
            var observation = await _wf.GetObservationByStationId(stationId, useCache);

            return Ok(JsonSerializer.Serialize(observation));
        }

        /// <summary>
        /// Returns weather forecast for the zip code provided
        /// </summary>
        /// <param name="zipCode">5 digit zip code for the area you want weather for</param>
        /// <param name="useCache">boolean parameter to force a fresh grab rather than a cached version</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetForecastByZipCode")]
        public async Task<IActionResult> GetForecastByZipCode(string zipCode, bool useCache = true)
        {
            var currently = await _wf.GetForecastByZipCode(zipCode, useCache);

            return Ok(JsonSerializer.Serialize(currently));
        }
    }
}
