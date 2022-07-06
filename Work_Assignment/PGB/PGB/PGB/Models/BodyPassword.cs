using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PGB.Models
{
    [DataContract]
    public partial class BodyPassword
    {
        /// <summary>
        /// Current password of the user.
        /// </summary>
        /// <value>Current password of the user.</value>
        [DataMember(Name = "currentPassword")]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// Current password of the user.
        /// </summary>
        /// <value>Current password of the user.</value>
        [DataMember(Name = "newPassword")]
        public string NewPassword { get; set; }

        public BodyPassword(string currPass, string newPass)
        {
            CurrentPassword = currPass;
            NewPassword = newPass;
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
