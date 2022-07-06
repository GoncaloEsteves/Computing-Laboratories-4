using System.Runtime.Serialization;

namespace PGB.Models
{
    public class Vehicle{
        [DataMember(Name = "registrationNumber")]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Gets or Sets TypeCode
        /// </summary>
        [DataMember(Name = "typeCode")]
        public string TypeCode { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets LastConsumptionReport
        /// </summary>
        [DataMember(Name = "lastConsumptionReport")]
        public double? LastConsumptionReport { get; set; }

        public Vehicle(string registrationNumber, string type, string name,  double lastConsumptionReport){
            RegistrationNumber = registrationNumber;
            TypeCode = type;
            Name = name;
            LastConsumptionReport = lastConsumptionReport;
        }
    }
}
