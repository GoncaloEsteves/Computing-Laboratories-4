using System;
using System.Collections.Generic;

namespace Data.Classes
{
    public class Trip
    {
        public Trip(float tripId, string name, string vehicleRegistrationNumber, Position origin, Position destination,
            DateTime date, TimeSpan duration, double cost, List<string> usedChargingStations)
        {
            this.TripId = tripId;
            this.Name = name;
            this.VehicleRegistrationNumber = vehicleRegistrationNumber;
            this.Start = origin;
            this.End = destination;
            this.Date = date;
            this.Duration = duration;
            this.Cost = cost;
            this.UsedChargingStations = usedChargingStations;
        }

        public float TripId { get; set; }

        public string Name { get; set; }

        public string VehicleRegistrationNumber { get; set; }

        public Position Start { get; set; }

        public Position End { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Duration { get; set; }

        public double Cost { get; set; }

        public List<string> UsedChargingStations { get; set; }
    }
}
