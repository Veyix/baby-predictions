using System.Collections.Generic;
using System.Linq;

namespace BabyPredictions.Domain
{
    public static class WinnerPicker
    {
        public static IEnumerable<Winner> PickTheWinners(IEnumerable<PersonWithPoints> containers)
        {
            var groupsOfWinners = containers.OrderByDescending(x => x.Points)
                .GroupBy(x => x.Points)
                .Take(3);

            int position = 1;
            var winners = new List<Winner>();
            foreach (var group in groupsOfWinners)
            {
                foreach (var winner in group)
                {
                    winners.Add(new Winner
                    {
                        Forename = winner.Prediction.Forename,
                        Surname = winner.Prediction.Surname,
                        Position = position,
                        Points = winner.Points
                    });
                }

                position++;
            }

            return winners;
        }
    }
}