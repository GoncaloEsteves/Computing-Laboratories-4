using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PGB.Models
{
        /// <summary>
        /// 
        /// </summary>
        [DataContract]
        public class BodyRegisterVehicle
        {
            /// <summary>
            /// Username of owner of the vehicle.
            /// </summary>
            /// <value>Username of owner of the vehicle.</value>
            [DataMember(Name = "username")]
            public string Username { get; set; }

        /// <summary>
        /// Gets or Sets _VehicleInfo
        /// </summary>
        [DataMember(Name = "vehicleInfo")]
        public Vehicle _VehicleInfo { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public BodyRegisterVehicle(string username, Vehicle vehicle)
            {
                Username = username;
                _VehicleInfo = vehicle;
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
