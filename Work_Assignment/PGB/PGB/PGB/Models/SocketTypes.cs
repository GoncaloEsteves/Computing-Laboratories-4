using Newtonsoft.Json;

namespace PGB.Models
{
    public partial class SocketTypes
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("result")]
        public string[] Result { get; set; }

        [JsonProperty("metadata")]
        public IternioMetadata Metadata { get; set; }
    }

    public partial class IternioMetadata
    {
        [JsonProperty("server_type")]
        public string ServerType { get; set; }

        [JsonProperty("server")]
        public string Server { get; set; }

        [JsonProperty("frontend")]
        public string Frontend { get; set; }
    }
}