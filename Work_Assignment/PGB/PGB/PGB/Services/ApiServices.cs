using Newtonsoft.Json;
using PGB.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace PGB.Services
{
    internal class ApiServices
    {
        public static string BaseApiAddress => "http://plugandgobeyond.azurewebsites.net";

        public async Task<bool> RegisterUserAsync(User user)
        {
            var client = new HttpClient();


            var json = JsonConvert.SerializeObject(user, Formatting.Indented);
       
            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                BaseApiAddress + "/users", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public static async Task<HttpStatusCode> LoginAsync(string username, string password)
        {
            var client = new HttpClient();

            var keyValues = new { Username = username, Password = password };

            var json = JsonConvert.SerializeObject(keyValues, Formatting.Indented);
            
            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                BaseApiAddress + "/users/login", httpContent);


           try { 
                var token = await response.Content.ReadAsStringAsync();

                Settings.Settings.AccessToken = token;
                return (response.StatusCode);
           } catch(JsonSerializationException e)
            {
                return (HttpStatusCode.BadRequest);
            }
        }

        public async Task<HttpStatusCode> LogoutAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.DeleteAsync(
                BaseApiAddress + "/users/logout");

            return response.StatusCode;
        }

        public static async Task<User> GetUserAsync(string username, string accessToken)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(BaseApiAddress + "/users/" + username);

            var user = JsonConvert.DeserializeObject<User>(json);

            return user;
        }

        public static async Task DeleteUserAsync(string username, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.DeleteAsync(
                BaseApiAddress + "/users/" + username);
        }

        public async Task<HttpStatusCode> ChangePasswordAsync(string username, string password, string newpassword, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            BodyPassword body = new BodyPassword(password, newpassword);

            var json = body.ToJson();

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(
                BaseApiAddress + "/users/" + username + "/manage/password", httpContent);

            return response.StatusCode;
        }

        public static async Task<bool> RegisterVehicleAsync(string user, Vehicle vehicle, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            BodyRegisterVehicle body = new BodyRegisterVehicle(user, vehicle);

            var json = body.ToJson();

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                BaseApiAddress + "/vehicles", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<List<Vehicle>> GetVehiclesAsync(string username, string accessToken)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(BaseApiAddress + "/users/" + username + "/manage/vehicles");

            var vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(json);

            return vehicles;
        }

        public async Task DeleteVehicleAsync(string username, string registration_number, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.DeleteAsync(
                BaseApiAddress + "/vehicles/" + registration_number + "?username=" + username);
        }

        public static async Task<List<CreditCard>> GetCreditCardsAsync(string username, string accessToken)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(BaseApiAddress + "/users/" + username + "/manage/creditCards");

            var cards = JsonConvert.DeserializeObject<List<CreditCard>>(json);

            return cards;
        }

        public static async Task<bool> AddCreditCardAsync(string user, CreditCard card, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = JsonConvert.SerializeObject(card, Formatting.Indented);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                BaseApiAddress + "/users/" + user + "/manage/creditCards", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task DeleteCreditCardAsync(string username, string card_number, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.DeleteAsync(
                BaseApiAddress + "/users/" + username + "/manage/creditCards/" + card_number);
        }

        public async Task<bool> AddFavoriteAsync(string user, string id, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            BodyAddFavorite body = new BodyAddFavorite(id);

            var json = body.ToJson();

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                BaseApiAddress + "/users/" + user + "/manage/favorites", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public static async Task<List<string>> GetFavoritesAsync(string username, string accessToken)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(BaseApiAddress + "/users/" + username + "/manage/favorites");

            var favorites = JsonConvert.DeserializeObject<List<string>>(json);

            return favorites;
        }

        public static async Task DeleteFavoriteAsync(string username, string id, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.DeleteAsync(
                BaseApiAddress + "/users/" + username + "/manage/favorites/" + id);
        }

        public static async Task<List<VehicleModels>> GetVehiclesModelsAsync(string accessToken)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(BaseApiAddress + "/vehicleModels");

            var vehiclesmodels = JsonConvert.DeserializeObject<List<VehicleModels>>(json);

            return vehiclesmodels;
        }

        public async Task<List<Trip>> GetTripsAsync(string username, string accessToken)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(BaseApiAddress + "/users/" + username + "/manage/trips");

            var trips = JsonConvert.DeserializeObject<List<Trip>>(json);

            return trips;
        }

        public async Task<bool> AddTripAsync(string user, Trip trip, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            BodyAddTrip body = new BodyAddTrip(user, trip);

            var json = body.ToJson();

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                BaseApiAddress + "/trips", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task DeleteTripAsync(string username, int? id, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.DeleteAsync(
                BaseApiAddress + "/trips/" + id + "?username=" + username);
        }
    }
}
