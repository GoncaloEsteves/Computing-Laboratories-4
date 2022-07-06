using System.Collections.Generic;
using PGB.Entity;

namespace PGB.entity{
    public class ChargingStation{
        private readonly string id;
        private readonly Position location;
        private ChargingStationOperator chargingStationOperator;
        private StatusEnum currentStatus;
        private double priceByActivation;
        private double priceByMinute;
        private double priceByKwh;
        private int waitingToCharge;
        private List<EV_Connector> chargingSockets;

        public ChargingStation(string id, Position location, ChargingStationOperator chargingStationOperator, StatusEnum currentStatus,
            double priceByActivation, double priceByMinute, double priceByKwh, int waitingToCharge,List<EV_Connector> chargingSockets){

            this.id = id;
            this.location = location;
            this.chargingStationOperator = chargingStationOperator;
            this.currentStatus = currentStatus;
            this.priceByActivation = priceByActivation;
            this.priceByMinute = priceByMinute;
            this.priceByKwh = priceByKwh;
            this.waitingToCharge = waitingToCharge;
            this.chargingSockets = chargingSockets;
        }

        public int Id { get; }

        public Position Location { get; }

        public ChargingStationOperator ChargingStationOperator { get; }

        public StatusEnum CurrentStatus { get; set; }

        public double PriceByActivation { get; set; }

        public double PriceByMinute { get; set; }

        public double PriceByKwh { get; set; }

        public int WaitingToCharge { get; set; }

        public List<EV_Connector> ChargingSockets { get; set; }
    }
}
