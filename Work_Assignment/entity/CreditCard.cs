using System;
namespace PGB.Entity{
    public class CreditCard{
        private string type;
        private long number;
        private DateTime expireDate;
        private int cvv;

        public CreditCard(string type, long number, DateTime expireDate, int cvv){
            this.type = type;
            this.number = number;
            this.expireDate = expireDate;
            this.cvv = cvv;
        }

        public string Type { get; set; }

        public long Number { get; set; }

        public DateTime ExpireDate { get; set; }

        public int Cvv { get; set; }
    }
}
