using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PGB.Models
{
    public partial class BodyAddFavorite
    {
        [DataMember(Name = "chargingStationId")]
        public string ChargingStationId { get; set; }

        public BodyAddFavorite(string id)
        {
            ChargingStationId = id;
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
