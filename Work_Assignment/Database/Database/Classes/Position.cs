using System;

namespace Data.Classes
{
    public class Position
    {
        public Position(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Distance(Position pos)
        {
            var R = 6371e3;
            var ro1 = ToRadians(pos.Latitude);
            var ro2 = ToRadians(Latitude);
            var lambda1 = ToRadians(Latitude - pos.Latitude);
            var lambda2 = ToRadians(Longitude - pos.Longitude);

            var a = Math.Sin(lambda1 / 2) * Math.Sin(lambda1 / 2) +
                    Math.Cos(ro1) * Math.Cos(ro2) *
                    Math.Sin(lambda2 / 2) * Math.Sin(lambda2 / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private double ToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }
    }
}
