using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.Extensions.Caching.Memory;
using WeatherOrNot.Logic.Model;

namespace WeatherOrNot.Logic.Factory
{
    public interface IWeatherFactory
    {
        Task<wx_station_index> GetObservationIndex(bool useCache = true);
        Task<current_observation> GetObservationByStationId(string observationCode, bool useCache = true);
        Task<CurrentForecast> GetForecastByZipCode(string zipCode, bool useCache = true);
    }

    public class WeatherFactory : IWeatherFactory
    {
        private IMemoryCache _cache;
        private readonly HttpClient _client = new HttpClient();

        public WeatherFactory(IMemoryCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Get the index list of observations available from weather.gov
        /// </summary>
        /// <param name="useCache">boolean parameter to force a fresh grab rather than a cached version</param>
        /// <returns></returns>
        public async Task<wx_station_index> GetObservationIndex(bool useCache = true)
        {
            wx_station_index index;
            //Return the cached version if we did not specify a force refresh AND if the cache exists
            if (useCache && _cache.TryGetValue("cachedObservationIndex", out index))
            {
                return index;
            }

            try
            {
                _client.DefaultRequestHeaders.Add("User-Agent","Other");
                //fetch the data
                var fetch = await _client.GetAsync("https://w1.weather.gov/xml/current_obs/index.xml");

                _client.DefaultRequestHeaders.Clear();
                //Set up a serializer object to the type
                XmlSerializer serializer = new XmlSerializer(typeof(wx_station_index));

                //Deserialize to the appropriate object
                index = (wx_station_index)serializer.Deserialize(fetch.Content.ReadAsStream());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            //save our object in our cache so we aren't hitting the main web service more than once per hour if we need to.
            _cache.Set("cachedObservationIndex", index, TimeSpan.FromHours(1));

            return index;
        }

        /// <summary>
        /// Retrieves the current observation for any site by Station ID
        /// </summary>
        /// <param name="stationId">Station ID for any particular observation site</param>
        /// <param name="useCache">boolean parameter to force a fresh grab rather than a cached version</param>
        /// <returns></returns>
        public async Task<current_observation> GetObservationByStationId(string stationId, bool useCache = true)
        {
            current_observation observation;

            //Return the cached version if we did not specify a force refresh AND if the cache exists
            if (useCache && _cache.TryGetValue($"cachedObservation-{stationId}", out observation))
            {
                return observation;
            }

            var index = await GetObservationIndex();

            var station = index.station.Where(s => s.station_id == stationId).ToArray();

            try
            {
                //Clear Headers to ensure we only send any specific header once
                _client.DefaultRequestHeaders.Clear();
                _client.DefaultRequestHeaders.Add("User-Agent", "Other");

                //fetch the data
                var fetch = await _client.GetAsync(station[0].xml_url);

                //If we get a redirect, follow it
                if (fetch.StatusCode == HttpStatusCode.Found)
                {
                    var redirect_url = fetch.Headers.Location;
                    fetch = await _client.GetAsync(redirect_url);
                }
                
                //Set up a serializer object to the type
                XmlSerializer serializer = new XmlSerializer(typeof(current_observation));

                //Deserialize to the appropriate object
                observation = (current_observation)serializer.Deserialize(fetch.Content.ReadAsStream());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            //save our object in our cache so we aren't hitting the main web service more than once per hour if we need to.
            _cache.Set($"cachedObservation-{stationId}", observation, TimeSpan.FromHours(1));

            return observation;
        }

        public async Task<CurrentForecast> GetForecastByZipCode(string zipCode, bool useCache = true)
        {
            CurrentForecast forecast;

            //Return the cached version if we did not specify a force refresh AND if the cache exists
            if (useCache && _cache.TryGetValue($"cachedForecast-{zipCode}", out forecast))
            {
                return forecast;
            }
            //build payload

            var data = new Dictionary<string, string>();
            data.Add("inputstring", zipCode);
            var payload = new FormUrlEncodedContent(data);

            //Clear Headers to ensure we only send any specific header once
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("User-Agent", "Other");

            //post
            var resp = await _client.PostAsync("https://forecast.weather.gov/zipcity.php ", payload);

            forecast = new CurrentForecast(await resp.Content.ReadAsStringAsync());


            //save our object in our cache so we aren't hitting the main web service more than once per hour if we need to.
            _cache.Set($"cachedForecast-{zipCode}", forecast, TimeSpan.FromHours(1));

            return forecast;
        }

    }
}