using System.Collections.Generic;

namespace Data.Classes
{
    public class ChargingStation
    {
        public ChargingStation(string id, Position location, string operatorName, string website, StatusEnum status, double priceByActivation, double priceByMinute,
            double priceByKwh, int waitingToCharge, List<EV_Connector> chargingSockets)
        {

            this.Id = id;
            this.Location = location;
            this.OperatorName = operatorName;
            this.Website = website;
            this.Status = status;
            this.PriceByActivation = priceByActivation;
            this.PriceByMinute = priceByMinute;
            this.PriceByKwh = priceByKwh;
            this.WaitingToCharge = waitingToCharge;
            this.ChargingSockets = chargingSockets;
        }

        public string Id { get; set; }

        public Position Location { get; set; }

        public string OperatorName { get; set; }

        public string Website { get; set; }

        public StatusEnum Status { get; set; }

        public double PriceByActivation { get; set; }

        public double PriceByMinute { get; set; }

        public double PriceByKwh { get; set; }

        public int WaitingToCharge { get; set; }

        public List<EV_Connector> ChargingSockets { get; set; }
    }
}
