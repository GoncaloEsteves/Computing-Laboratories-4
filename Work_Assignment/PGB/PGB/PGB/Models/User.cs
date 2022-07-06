using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PGB.Models
{
    public class User
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "username")]
        public string Username { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "nif")]
        public string Nif { get; set; }
        [DataMember(Name = "password")]
        public string Password { get; set; }

        public User(string name, string username, string nif, string password, string email)
        {
            Name = name;
            Username = username;
            Nif = nif;
            Password = password;
            Email = email;

        }
    }
}
