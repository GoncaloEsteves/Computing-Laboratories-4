using System.Collections.Generic;

namespace PGB.Entity{
    public class User{
        private string name;
        private string username;
        private string nif;
        private string password;
        private List<CreditCard> creditCards;
        private List<string> favoriteChargingStations;
        private List<long> trips;
        private List<string> vehiclesIds;

        public User(string name, string username, string nif, string password, List<CreditCard> creditCards, List<string> favoriteChargingStations, List<string> vehiclesIds){
            this.name = name;
            this.username = username;
            this.nif = nif;
            this.password = password;
            this.creditCards = creditCards;
            this.favoriteChargingStations = favoriteChargingStations;
            this.vehiclesIds = vehiclesIds;
        }

        public string Name { get; }

        public string Username { get; }

        public int Nif { get; }

        public string Password { get; set; }

        public List<CreditCard> CreditCards { get; set; }

        public List<int> FavoriteChargingStations { get; }

        public List<string> VehicleIds { get; }

        public void AddCreditCard(CreditCard card) => creditCards.Add(card);

        public void AddFavoriteChargingStation(string id) => favoriteChargingStations.Add(id);

        public void AddVehicleId(string vehicleId) => vehiclesIds.Add(vehicleId);
    }
}
