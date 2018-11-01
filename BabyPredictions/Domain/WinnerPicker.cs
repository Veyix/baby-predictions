using System.Collections.Generic;
using System.Linq;

namespace BabyPredictions.Domain
{
    public class WinnerPicker
    {
        private readonly DatabaseContext _context;

        public WinnerPicker(DatabaseContext context)
        {
            _context = context;
        }

        public void PickWinner()
        {
            var birthDetails = _context.Set<Birth>().Single();
            var predictions = _context.Set<Prediction>().ToArray();
            var containers = new List<PersonWithPoints>();

            containers.AddRange(predictions.Select(x => new PersonWithPoints(x)));

            CalculatePointsForGender(birthDetails, containers);
            CalculatePointsForBirthDate(birthDetails, containers);
            CalculatePointsForBirthTime(birthDetails, containers);
            CalculatePointsForBirthWeight(birthDetails, containers);

            PickTheWinners(containers);
        }

        private void CalculatePointsForGender(Birth birthDetails, IEnumerable<PersonWithPoints> containers)
        {
            var winners = containers.Where(x => x.Prediction.Gender == birthDetails.Gender);
            foreach (var winner in winners)
            {
                winner.Points++;
            }
        }

        private void CalculatePointsForBirthDate(Birth birthDetails, IEnumerable<PersonWithPoints> containers)
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

        private void CalculatePointsForBirthTime(Birth birthDetails, IEnumerable<PersonWithPoints> containers)
        {
            var groupedByDistanceFromTime = containers.Select(x => new { Distance = birthDetails.BirthTime - x.Prediction.BirthTime, P = x })
                .GroupBy(x => x.Distance)
                .OrderBy(x => x.Key)
                .Take(3)
                .ToArray();

            int points = 3;
            foreach (var group in groupedByDistanceFromTime)
            {
                foreach (var container in group)
                {
                    container.P.Points += points;
                }

                points--;
            }
        }

        private void CalculatePointsForBirthWeight(Birth birthDetails, IEnumerable<PersonWithPoints> containers)
        {
            var groupedByDistanceFromWeight = containers.Select(x => new { Distance = birthDetails.BirthWeightInOunces - x.Prediction.BirthWeightInOunces, P = x })
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

        private void PickTheWinners(IEnumerable<PersonWithPoints> containers)
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

            foreach (var winner in winners)
            {
                _context.Add(winner);
            }

            _context.SaveChanges();
        }

        private sealed class PersonWithPoints
        {
            public PersonWithPoints(Prediction prediction)
            {
                Prediction = prediction;
            }

            public Prediction Prediction { get; }
            public int Points { get; set; }
        }
    }
}