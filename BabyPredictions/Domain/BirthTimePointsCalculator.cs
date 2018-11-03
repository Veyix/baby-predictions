using System;
using System.Collections.Generic;
using System.Linq;

namespace BabyPredictions.Domain
{
    public static class BirthTimePointsCalculator
    {
        public static void CalculatePoints(Birth birthDetails, IEnumerable<PersonWithPoints> containers)
        {
            var groupedByDistanceFromTime = containers.Select(x => new
                {
                    Distance = GetMinutesFromBirthTime(birthDetails.BirthTime, x.Prediction.BirthTime),
                    Points = x
                })
                .GroupBy(x => x.Distance)
                .OrderBy(x => x.Key)
                .Take(3)
                .ToArray();

            int points = 3;
            foreach (var group in groupedByDistanceFromTime)
            {
                foreach (var container in group)
                {
                    container.Points.Points += points;
                }

                points--;
            }
        }

        private static int GetMinutesFromBirthTime(TimeSpan birthTime, TimeSpan predictedBirthTime)
        {
            return (int)Math.Abs((birthTime - predictedBirthTime).TotalMinutes);
        }
    }
}