using System;
namespace PGB.Entity{
    public class Position{
        private double latitude;
        private double longitude;

        public Position(double latitude, double longitude){
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Distance(Position pos) {
            var R = 6371e3;
            var ro1 = ToRadians(pos.Latitude);
            var ro2 = ToRadians(latitude);
            var lambda1 = ToRadians(latitude - pos.Latitude);
            var lambda2 = ToRadians(longitude - pos.Longitude);

            var a = Math.Sin(lambda1 / 2) * Math.Sin(lambda1 / 2) +
                    Math.Cos(ro1) * Math.Cos(ro2) *
                    Math.Sin(lambda2 / 2) * Math.Sin(lambda2 / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private double ToRadians(double angle){
            return (Math.PI / 180) * angle;
        }
    }
}
