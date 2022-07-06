using SQLite;

namespace PGB.Data.Models
{
    public class ChargingStationDB
    {
        [PrimaryKey]
        public long Id { get; set; }

        public string StationName { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string ChargingStationOperator { get; set; }
    }
}