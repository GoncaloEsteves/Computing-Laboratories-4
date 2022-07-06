using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PGB.Models;

namespace PGB.Services
{
    public class GoogleMapsApiService : IGoogleMapsApiService
    {
        static string _googleMapsKey;

        private const string ApiBaseAddress = "https://maps.googleapis.com/maps/";
        private HttpClient CreateClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
        public static void Initialize(string googleMapsKey)
        {
            _googleMapsKey = googleMapsKey;
        }

        /// <summary>
        /// Obtain a polyline route from google maps api by the given waypoints that the route needs to pass.
        /// </summary>
        /// <param name="originLatitude"></param> 
        /// <param name="originLongitude"></param>
        /// <param name="destinationLatitude"></param>
        /// <param name="destinationLongitude"></param>
        /// <param name="steps">The steps from where the route need to pass.</param>
        /// <returns></returns>
        public async Task<GoogleDirection> GetDirections(string originLatitude, string originLongitude, 
            string destinationLatitude, string destinationLongitude, List<IternioStep> steps)
        {
            GoogleDirection googleDirection = new GoogleDirection();

            using (var httpClient = CreateClient())
            {
                var request = $"api/directions/json?mode=driving&transit_routing_preference=less_driving&origin={originLatitude},{originLongitude}&destination={destinationLatitude},{destinationLongitude}&key=AIzaSyAKebUmEyg9SZThB5W9Pse-wPvhD4_ZvfI";
                
                if (!(steps == null || steps.Count == 0))
                {
                    request += "&waypoints=";
                    foreach (var step in steps)
                    {
                        var lat = step.Lat.ToString().Replace(',', '.');
                        var lon = step.Lon.ToString().Replace(',', '.');
                        request += lat + "," + lon + "|";
                    }
                }
                var response = await httpClient.GetAsync(request).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        googleDirection = await Task.Run(() =>
                            JsonConvert.DeserializeObject<GoogleDirection>(json)
                        ).ConfigureAwait(false);

                    }
                }
            }

            return googleDirection;
        }

        public async Task<GooglePlaceAutoCompleteResult> GetPlaces(string text)
        {
            GooglePlaceAutoCompleteResult results = null;

            using (var httpClient = CreateClient())
            {
                var response = await httpClient.GetAsync($"api/place/autocomplete/json?input={Uri.EscapeUriString(text)}&types=geocode&country=PT&language=pt_pt&key={_googleMapsKey}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
                    {
                        results = await Task.Run(() =>
                           JsonConvert.DeserializeObject<GooglePlaceAutoCompleteResult>(json)
                        ).ConfigureAwait(false);

                    }
                }
            }

            return results;
        }

        public async Task<GooglePlace> GetPlaceDetails(string placeId)
        {
            GooglePlace result = null;
            using (var httpClient = CreateClient())
            {
                var response = await httpClient.GetAsync($"api/place/details/json?placeid={Uri.EscapeUriString(placeId)}&country=PT&key={_googleMapsKey}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
                    {
                        result = new GooglePlace(JObject.Parse(json));
                    }
                }
            }

            return result;
        }
    }
}
