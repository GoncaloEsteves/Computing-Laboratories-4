using PGB.entity;

namespace PGB.Entity{

    public class EV_Connector {
        private string id;
        private StatusEnum status;
        private double powerKw;
        private int amps;
        private int voltage;
        private PhaseEnum phase;
        private ConnectorEnum connectorType;
        private ConnectorChargingRateEnum rate;

        public EV_Connector(string id, StatusEnum status, double powerKw, int amps, int voltage, PhaseEnum phase, ConnectorEnum connectorType, ConnectorChargingRateEnum rate){
            this.id = id;
            this.status = status;
            this.powerKw = powerKw;
            this.amps = amps;
            this.voltage = voltage;
            this.phase = phase;
            this.connectorType = connectorType;
            this.rate = rate;
        }

        public string Id { get; set; }

        public StatusEnum Status { get; set; }

        public double PowerKw { get; set; }

        public int Amps { get; set; }

        public int Voltage { get; set; }

        public PhaseEnum Phase { get; set; }

        public ConnectorEnum ConnectorType { get; set; }

        public ConnectorChargingRateEnum Rate { get; set; }
    }
}
