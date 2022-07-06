using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PGB.Models
{
    [DataContract]
    public class BodyAddTrip
    {
        /// <summary>
        /// Username of the user that make the trip.
        /// </summary>
        /// <value>Username of the user that make the trip.</value>
        [DataMember(Name = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or Sets Trip
        /// </summary>
        [DataMember(Name = "trip")]
        public Trip Trip { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public BodyAddTrip(string username, Trip trip)
        {
            Username = username;
            Trip = trip;
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}