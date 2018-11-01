using System;

namespace BabyPredictions.Domain
{
    public class Birth
    {
        private const double OuncesInPound = 16d;
        
        public int Id { get; set; }
        public Gender Gender { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public int BirthWeightInOunces { get; set; }

        public TimeSpan BirthTime => BirthDate.TimeOfDay;
        public int BirthWeightInPounds => (int)Math.Floor(BirthWeightInOunces / OuncesInPound);
        public int BirthWeightInOuncesLessPounds => BirthWeightInOunces - (BirthWeightInPounds * (int)OuncesInPound);
    }
}