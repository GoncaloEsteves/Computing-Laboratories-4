using System;
using SQLite;

namespace PGB.Models
{
    public class ChargingStationOperator{
        [PrimaryKey]
        public string OperatorId { get; set; }
        public string OperatorName { get; set; }

        public Uri WebsiteURL { get; set; }

        public ChargingStationOperator(string operatorName, Uri websiteURL){
            OperatorName = operatorName;
            WebsiteURL = websiteURL;
        }
    }
}
