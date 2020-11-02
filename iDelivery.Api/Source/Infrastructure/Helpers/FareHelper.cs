using System.Threading.Tasks;

namespace iDelivery.Api.Source.Infrastructure.Helpers
{
    public static class FareHelper
    {
        public static decimal Compute(decimal pricePerKilometer, decimal baseFare, decimal? surcharge, decimal distanceKilometer)
        {
            var totalFare = default(decimal);
            var succeedingKilometers = distanceKilometer * pricePerKilometer;
            if (distanceKilometer > 0)
            {
                if (surcharge != null)
                {
                    //baseFare + (totalkms x priceperkm) x surcharge
                    if (surcharge.GetValueOrDefault() > 0)
                        totalFare = (baseFare + (succeedingKilometers)) * surcharge.GetValueOrDefault();
                    else
                        totalFare = baseFare + succeedingKilometers;
                }
            }

            return totalFare;
        }

        public static string ComputeRiderFare(decimal pricePerKilometer, decimal baseFare, decimal? surcharge, decimal distanceKilometer)
        {
            var totalFare = default(decimal);
            var succeedingKilometers = distanceKilometer * pricePerKilometer;
            if (distanceKilometer > 0)
            {
                if (surcharge != null)
                {
                    //baseFare + (totalkms x priceperkm) x surcharge
                    if (surcharge.GetValueOrDefault() > 0)
                        totalFare = (baseFare + (succeedingKilometers)) * surcharge.GetValueOrDefault();
                    else
                        totalFare = baseFare + succeedingKilometers;
                }
            }

            return totalFare.ToString("0.00");
        }
    }
}
