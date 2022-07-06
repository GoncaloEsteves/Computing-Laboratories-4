using System.Collections.Generic;

namespace Data.Classes
{
    public class User
    {
        public User(string name, string username, string nif, string password, List<CreditCard> creditCards, List<Vehicle> vehicles, List<float> tripsIds, List<string> favouriteStations)
        {
            this.Name = name;
            this.Username = username;
            this.Nif = nif;
            this.Password = password;
            this.CreditCards = creditCards;
            this.Vehicles = vehicles;
            this.TripIds = tripsIds;
            this.FavouriteStations = favouriteStations;
        }

        public string Name { get; set; }

        public string Username { get; set; }

        public string Nif { get; set; }

        public string Password { get; set; }

        public List<CreditCard> CreditCards { get; set; }

        public List<Vehicle> Vehicles { get; set; }

        public List<float> TripIds { get; set; }

        public List<string> FavouriteStations { get; set; }

        public void AddCreditCard(CreditCard card) => CreditCards.Add(card);

        public void AddVehicleId(Vehicle vehicle) => Vehicles.Add(vehicle);
    }
}
