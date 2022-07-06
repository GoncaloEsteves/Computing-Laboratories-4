using System;
using System.Collections.Generic;

namespace PGB{
    public class Vehicle{

        private string registrationNumber;
        private string brand;
        private string model;
        private string version;
        private int year;
        private double batteryCapacity;
        private double lastComsumptionReport;   //measured in Wh/km

        public Vehicle(string registrationNumber, string brand, string model, string version, int year, double batteryCapacity, double lastComsumptionReport){
            this.registrationNumber = registrationNumber;
            this.brand = brand;
            this.model = model;
            this.version = version;
            this.year = year;
            this.batteryCapacity = batteryCapacity;
            this.lastComsumptionReport = lastComsumptionReport;
        }

        public string RegistrationNumber { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Version { get; set; }

        public int Year { get; set; }

        public double BatteryCapacity { get; set; }

        public double LastComsumptionReport { get; set; }
    }
}
