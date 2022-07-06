namespace Data.Classes
{
    public class EV_Connector
    {
        public EV_Connector(string id, StatusEnum status, double powerOutput, int amps, int voltage, int phase, ChargingStationTypeEnum type, int rate)
        {
            this.Id = id;
            this.Status = status;
            this.PowerOutput = powerOutput;
            this.Amps = amps;
            this.Voltage = voltage;
            this.Phase = phase;
            this.Type = type;
            this.Rate = rate;
        }

        public string Id { get; set; }

        public StatusEnum Status { get; set; }

        public double PowerOutput { get; set; }

        public int Amps { get; set; }

        public int Voltage { get; set; }

        public int Phase { get; set; }

        public ChargingStationTypeEnum Type { get; set; }
    
        public int Rate { get; set; }
    }
}
