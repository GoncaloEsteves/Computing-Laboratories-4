using System;

namespace Data.Classes
{
    public class CreditCard
    {

        public CreditCard(string type, int number, DateTime expireDate, int cvv)
        {
            this.Type = type;
            this.Number = number;
            this.ExpireDate = expireDate;
            this.Cvv = cvv;
        }

        public string Type { get; set; }

        public int Number { get; set; }

        public DateTime ExpireDate { get; set; }

        public int Cvv { get; set; }
    }
}
