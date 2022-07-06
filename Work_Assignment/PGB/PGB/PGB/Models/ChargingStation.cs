using System.Collections.Generic;
using SQLite;
using PGB.Models.Enums;

namespace PGB.Models{
    public class ChargingStation{
        
        [PrimaryKey] 
        public string Id { get; set; }

        public Position Location { get; set; }

        public ChargingStationOperator ChargingStationOperator { get; set; }

        public StatusEnum CurrentStatus { get; set; }

        public double PriceByActivation { get; set; }

        public double PriceByMinute { get; set; }

        public double PriceByKwh { get; set; }

        public int WaitingToCharge { get; set; }

        public List<Connector> ChargingSockets { get; set; }

        public ChargingStation(string id, Position location, ChargingStationOperator chargingStationOperator, StatusEnum currentStatus,
            double priceByActivation, double priceByMinute, double priceByKwh, int waitingToCharge,List<Connector> chargingSockets){

            Id =  id;
            Location = location;
            ChargingStationOperator =chargingStationOperator;
            CurrentStatus = currentStatus;
            PriceByActivation = priceByActivation;
            PriceByMinute = priceByMinute;
            PriceByKwh = priceByKwh;
            WaitingToCharge = waitingToCharge;
            ChargingSockets = chargingSockets;
        }

        //temp
        public ChargingStation(string id, Position position)
        {

            this.Id = id;
            this.Location = position;
        } 
    }
}
