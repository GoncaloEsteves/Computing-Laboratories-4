using System;
namespace PGB.entity{
    public class ChargingStationOperator{
        private string operatorName;
        private Uri websiteURL;

        public ChargingStationOperator(string operatorName, Uri websiteURL){
            this.operatorName = operatorName;
            this.websiteURL = websiteURL;
        }

        public string OperatorName { get; set; }

        public Uri WebsiteURL { get; set; }
    }
}
