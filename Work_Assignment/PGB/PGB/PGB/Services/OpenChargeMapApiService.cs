using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PGB.Models;

namespace PGB.Services
{
    public partial class OpenChargeMapApiService
    {
        private const string ApiBaseAddress = "https://api.openchargemap.io/v3/poi/";

        public async Task<IList<ExternalChargingStationOCM>> LoadOCMChargingStations()
        {
            var httpClient = CreateClient();


            IList<ExternalChargingStationOCM> results = null;

            {
                var response = await httpClient.GetAsync(ApiBaseAddress + "?output=json&countrycode=PT&maxresults=3000").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json) && json != "ERROR")
                    {
                        results = await Task.Run(() =>
                            JsonConvert.DeserializeObject<IList<ExternalChargingStationOCM>>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
                        ).ConfigureAwait(false);

                    }
                }
                return results;
            }
        }

        private HttpClient CreateClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };

            httpClient.DefaultRequestHeaders.Accept.Clear();

            return httpClient;
        }
    }
}