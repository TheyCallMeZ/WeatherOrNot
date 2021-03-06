using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherOrNot.Logic.Factory;

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
    }
}
