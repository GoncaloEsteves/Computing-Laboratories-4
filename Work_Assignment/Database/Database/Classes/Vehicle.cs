using System;
using System.Collections.Generic;

namespace Data.Classes
{
    public class Vehicle
    {
        public Vehicle(string registrationNumber, string brand, string model, string version, int year, double batteryCapacity, double lastComsumptionReport)
        {
            this.RegistrationNumber = registrationNumber;
            this.Brand = brand;
            this.Model = model;
            this.Version = version;
            this.Year = year;
            this.BatteryCapacity = batteryCapacity;
            this.LastComsumptionReport = lastComsumptionReport;
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
