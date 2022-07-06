using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PGB.Models
{
    [DataContract]
    public class Token
    {
        [DataMember(Name = "token")]
        public string token { get; set; }

        [DataMember(Name = "expires")]

        public int expires { get; set; }

        public Token(string t, int e)
        {
            token = t;
            expires = e;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("{\n");
            sb.Append("  token: ").Append(token).Append("\n");
            sb.Append("  expires: ").Append(expires).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
