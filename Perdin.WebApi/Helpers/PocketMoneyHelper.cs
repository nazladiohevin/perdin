using System;

namespace Perdin.WebApi.Helpers
{
    public static class PocketMoneyHelper
    {
        private const int UsdToIdrRate = 16000;
        private const int ForeignRatePerDayUsd = 50;

        public static int CalculatePocketMoney(double distance, bool isForeign, bool isSameProvince, bool isSameIsland, int durationInDays)
        {
            if (isForeign)
            {
                return ForeignRatePerDayUsd * UsdToIdrRate * durationInDays;
            }

            if (distance <= 60)
            {
                return 0;
            }

            int ratePerDay = 0;

            if (isSameProvince)
            {
                ratePerDay = 200000;
            }
            else if (isSameIsland)
            {
                ratePerDay = 250000;
            }
            else
            {
                ratePerDay = 300000;
            }

            return ratePerDay * durationInDays;
        }
    }
}
