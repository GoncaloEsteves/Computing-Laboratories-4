using System;
using System.Collections.Generic;

namespace PGB.Entity{
    public class Trip{
        private long tripId;
        private string tripName;
        private string vehicleRegistrationNumber;
        private Position origin;
        private Position destination;
        private TimeSpan duration;
        private DateTime date;
        private double cost;
        private List<string> usedChargingStations; //list that contains all of the charging stations id's

        public Trip(long tripId, string tripName, string vehicleRegistrationNumber, Position origin, Position destination,
            TimeSpan duration, double cost, List<string> usedChargingStations){
            this.tripId = tripId;
            this.tripName = tripName;
            this.vehicleRegistrationNumber = vehicleRegistrationNumber;
            this.origin = origin;
            this.destination = destination;
            this.duration = duration;
            this.cost = cost;
            this.usedChargingStations = usedChargingStations;
        }

        public long TripId { get; set; }

        public string TripName { get; set; }

        public string VehicleRegistrationNumber { get; set; }

        public Position Origin { get; set; }

        public Position Destination { get; set; }

        public TimeSpan Duration { get; set; }

        public double Cost { get; set; }

        public List<int> UsedChargingStations { get; set; }
    }
}
