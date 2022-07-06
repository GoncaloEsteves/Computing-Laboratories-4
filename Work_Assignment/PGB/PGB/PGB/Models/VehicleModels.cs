using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PGB.Models
{
    public class VehicleModels
    {
        [DataMember(Name = "typeCode")]
        public string TypeCode { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets LevelTwoCharges
        /// </summary>
        [DataMember(Name = "levelTwoCharges")]
        public string LevelTwoCharges { get; set; }

        /// <summary>
        /// Gets or Sets FastCharges
        /// </summary>
        [DataMember(Name = "fastCharges")]
        public string FastCharges { get; set; }

        public VehicleModels(string type, string name, string level, string fast)
        {

            TypeCode = type;
            Name = name;
            LevelTwoCharges = level;
            FastCharges = fast;
        }
    }
}
