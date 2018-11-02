using System.Collections.Generic;
using System.Linq;

namespace BabyPredictions.Domain
{
    public static class GenderPointsCalculator
    {
        public static void CalculatePoints(Birth birthDetails, IEnumerable<PersonWithPoints> containers)
        {
            var winners = containers.Where(x => x.Prediction.Gender == birthDetails.Gender);
            foreach (var winner in winners)
            {
                winner.Points++;
            }
        }
    }
}