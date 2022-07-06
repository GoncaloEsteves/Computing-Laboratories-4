using PGB.Models.Enums;
using SQLite;

namespace PGB.Models
{

    public class Connector {
        [PrimaryKey]
        public string ID { get; set; }

        public StatusEnum Status { get; set; }

        public double PowerKw { get; set; }

        public int Amps { get; set; }

        public int Voltage { get; set; }

        public PhaseEnum Phase { get; set; }

        public ConnectorEnum ConnectorType { get; set; }

        public ConnectorChargingRateEnum Rate { get; set; }

        public Connector(string id, StatusEnum status, double powerKw, int amps, int voltage, PhaseEnum phase, ConnectorEnum connectorType, ConnectorChargingRateEnum rate){
            ID = id;
            Status = status;
            PowerKw = powerKw;
            Amps = amps;
            Voltage = voltage;
            Phase = phase;
            ConnectorType = connectorType;
            Rate = rate;
        }
    }
}
