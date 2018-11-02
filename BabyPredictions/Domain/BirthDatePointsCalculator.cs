using System.Collections.Generic;
using System.Linq;

namespace BabyPredictions.Domain
{
    public static class BirthDatePointsCalculator
    {
        public static void CalculatePoints(Birth birthDetails, IEnumerable<PersonWithPoints> containers)
        {
            var groupedByDistanceFromDate = containers.Select(x => new { Distance = birthDetails.BirthDate - x.Prediction.BirthDate, P = x })
                .GroupBy(x => x.Distance)
                .OrderBy(x => x.Key)
                .Take(3)
                .ToArray();

            int points = 3;
            foreach (var group in groupedByDistanceFromDate)
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