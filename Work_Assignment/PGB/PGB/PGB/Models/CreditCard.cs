using System;
using System.Runtime.Serialization;

namespace PGB.Models
{
    public class CreditCard{
        [DataMember(Name = "cardType")]
        public string CardType { get; set; }
        [DataMember(Name = "cardNumber")]
        public string CardNumber { get; set; }
        [DataMember(Name = "expireDate")]
        public DateTime? ExpireDate { get; set; }
        [DataMember(Name = "cvv")]
        public int? Cvv { get; set; }

        public CreditCard(string type, string number, DateTime expireDate, int? cvv){
            CardType = type;
            CardNumber = number;
            ExpireDate = expireDate;
            Cvv = cvv;
        }

    }
}
