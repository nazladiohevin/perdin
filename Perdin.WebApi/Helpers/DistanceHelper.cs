using System;

namespace Perdin.WebApi.Helpers
{
    public static class DistanceHelper
    {
        public static double CalculateDistance(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {
            // Radius of the Earth in kilometers
            const double R = 6371;

            double lat1Rad = DegreesToRadians((double)lat1);
            double lon1Rad = DegreesToRadians((double)lon1);
            double lat2Rad = DegreesToRadians((double)lat2);
            double lon2Rad = DegreesToRadians((double)lon2);

            double dLat = lat2Rad - lat1Rad;
            double dLon = lon2Rad - lon1Rad;

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}
