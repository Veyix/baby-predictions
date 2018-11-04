using System;
using System.Collections.Generic;
using System.Linq;

namespace BabyPredictions.Domain
{
    public static class BirthDatePointsCalculator
    {
        public static void CalculatePoints(Birth birthDetails, IEnumerable<PersonWithPoints> containers)
        {
            var groupedByDistanceFromDate = containers.Select(x => new
                {
                    Distance = GetDaysFromBirthDate(birthDetails.BirthDate, x.Prediction.BirthDate),
                    Points = x
                })
                .GroupBy(x => x.Distance)
                .OrderBy(x => x.Key)
                .Take(3)
                .ToArray();

            int points = 3;
            foreach (var group in groupedByDistanceFromDate)
            {
                foreach (var container in group)
                {
                    container.Points.Points += points;
                }

                points--;
            }
        }

        private static int GetDaysFromBirthDate(DateTimeOffset birthDate, DateTimeOffset predictedBirthDate)
        {
            return (int)Math.Abs((birthDate.Date - predictedBirthDate.Date).TotalDays);
        }
    }
}