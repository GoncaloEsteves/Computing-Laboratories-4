using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PGB.Models
{
    public class Trip
    {
        /// <summary>
        /// Gets or Sets TripId
        /// </summary>
        [DataMember(Name = "tripId")]
        public int? TripId { get; set; }

        /// <summary>
        /// Gets or Sets TripName
        /// </summary>
        [DataMember(Name = "tripName")]
        public string TripName { get; set; }

        /// <summary>
        /// Gets or Sets VehicleRegistrationNumber
        /// </summary>
        [DataMember(Name = "vehicleRegistrationNumber")]
        public string VehicleRegistrationNumber { get; set; }

        /// <summary>
        /// Gets or Sets Origin
        /// </summary>
        [DataMember(Name = "origin")]
        public Position Origin { get; set; }

        /// <summary>
        /// Gets or Sets Destination
        /// </summary>
        [DataMember(Name = "destination")]
        public Position Destination { get; set; }

        /// <summary>
        /// Gets or Sets Duration
        /// </summary>
        [DataMember(Name = "duration")]
        public string Duration { get; set; } // No server eles têm duration como string, portanto temos que converter o Timespan para string

        /// <summary>
        /// Gets or Sets Cost
        /// </summary>
        [DataMember(Name = "cost")]
        public double? Cost { get; set; }

        /// <summary>
        /// Gets or Sets Date
        /// </summary>
        [DataMember(Name = "date")]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Array that contains all of the charging stations id&#x27;s used during the trip
        /// </summary>
        /// <value>Array that contains all of the charging stations id&#x27;s used during the trip</value>
        [DataMember(Name = "usedChargingStations")]
        public List<string> UsedChargingStations { get; set; }

        public Trip(int? tripId, string tripName, string vehicleRegistrationNumber, Position origin, Position destination,
            string duration, double cost, List<string> usedChargingStations, DateTime date)
        {
            TripId = tripId;
            TripName = tripName;
            VehicleRegistrationNumber = vehicleRegistrationNumber;
            Origin = origin;
            Destination = destination;
            Duration = duration;
            Cost = cost;
            UsedChargingStations = usedChargingStations;
            Date = date;
        }
    }
}