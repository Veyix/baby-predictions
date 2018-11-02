using System.Collections.Generic;
using System.Linq;

namespace BabyPredictions.Domain
{
    public class WinnerCalculator
    {
        private readonly DatabaseContext _context;

        public WinnerCalculator(DatabaseContext context)
        {
            _context = context;
        }

        public void CalculateWinners()
        {
            var birthDetails = _context.Set<Birth>().Single();
            var predictions = _context.Set<Prediction>().ToArray();
            var containers = new List<PersonWithPoints>();

            containers.AddRange(predictions.Select(x => new PersonWithPoints(x)));

            GenderPointsCalculator.CalculatePoints(birthDetails, containers);
            BirthDatePointsCalculator.CalculatePoints(birthDetails, containers);
            BirthTimePointsCalculator.CalculatePoints(birthDetails, containers);
            BirthWeightPointsCalculator.CalculatePoints(birthDetails, containers);

            var winners = WinnerPicker.PickTheWinners(containers);

            foreach (var winner in winners)
            {
                _context.Add(winner);
            }

            _context.SaveChanges();
        }
    }
}