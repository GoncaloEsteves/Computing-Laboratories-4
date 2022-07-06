using SQLite;

namespace PGB.Data.Models
{
    public class ConnectorDB
    {
    
        [PrimaryKey]
        public long Id { get; set; }

        public bool IsOperational { get; set; }

        public bool IsFastChargeCapable { get; set; }

        public long StationId { get; set; }
    }
}