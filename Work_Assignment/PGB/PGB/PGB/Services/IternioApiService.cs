using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PGB.Models;

namespace PGB.Services
{
    public class IternioApiService : IternioApiServiceInterface
    {
        static string _iternioApiKey;

        private const string ApiBaseAddress = "https://api.iternio.com/1/";
        private HttpClient CreateClient()
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };

            return httpClient;
        }
        public static void Initialize(string iternioApiKey)
        {
            _iternioApiKey = iternioApiKey;
        }
        
        public async Task<TripPlan> GetTripPlan(string originLatitude, string originLongitude, string destinationLatitude,
            string destinationLongitude, string typecode, int initialSoc, int refConsumption, bool avoidTolls, bool avoidHighways, bool avoidFerries)
        {
            TripPlan tripPlan = new TripPlan();

            if (initialSoc == 0)
            {
                initialSoc = 100;
            }

            if (refConsumption == 0)
            {
                refConsumption = 200;
            }
            
            using (var httpClient = CreateClient())
            {
                var response = await httpClient.GetAsync($"plan?destinations=[{{\"lat\":{originLatitude}," +
                                                         $"\"lon\":{originLongitude}}}, {{\"lat\":{destinationLatitude}," +
                                                         $"\"lon\":{destinationLongitude}}}]&car_model={typecode}&" +
                                                         $"path_steps=false&ref_consumption={refConsumption}&fast_chargers=ccs&realtime_weather=false&" +
                                                         $"initial_soc_perc={initialSoc}&charger_soc_perc=10&charger_max_soc_perc=100.0&" +
                                                         $"arrival_soc_perc=10&charge_overhead=300&speed_factor_perc=100&" +
                                                         $"max_speed=150&allow_ferry={!avoidFerries}&allow_motorway={!avoidHighways}&allow_toll={!avoidTolls}&" +
                                                         $"adjust_speed=false&outside_temp=20&wind_speed=0.0&wind_dir=head&" +
                                                         $"road_condition=normal&extra_weight=0&find_alts=false&find_next_charger_alts=false" +
                                                         $"&exclude_ids=0&network_preferences={{}}&preferred_charge_cost_multiplier=0.7&" +
                                                         $"nonpreferred_charge_cost_addition=0&group_preferences={{}}&access_preferences={{}}&" +
                                                         $"allowed_network_ids&preferred_network_ids&allowed_dbs=ocm&api_key={_iternioApiKey}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        tripPlan = await Task.Run(() =>
                            JsonConvert.DeserializeObject<TripPlan>(json)
                        ).ConfigureAwait(false);
                    }
                }
            }
            return tripPlan;
        }
    }
}