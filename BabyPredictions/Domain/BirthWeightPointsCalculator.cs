using System;
using System.Collections.Generic;
using System.Linq;

namespace BabyPredictions.Domain
{
    public static class BirthWeightPointsCalculator
    {
        public static void CalculatePoints(Birth birthDetails, IEnumerable<PersonWithPoints> containers)
        {
            var groupedByDistanceFromWeight = containers.Select(x => new
                {
                    Distance = Math.Abs(birthDetails.BirthWeightInOunces - x.Prediction.BirthWeightInOunces),
                    P = x
                })
                .GroupBy(x => x.Distance)
                .OrderBy(x => x.Key)
                .Take(3)
                .ToArray();

            int points = 3;
            foreach (var group in groupedByDistanceFromWeight)
            {
                foreach (var container in group)
                {
                    container.P.Points += points;
                }

                points--;
            }
        }
    }
}