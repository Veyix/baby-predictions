using System;

namespace BabyPredictions.Domain
{
    public class Prediction
    {
        private const double OuncesInPound = 16d;

        public Prediction()
        {
            // Empty parameterless constructor for serialization
        }

        public Prediction(string forename, string surname, Gender gender, DateTimeOffset birthDate, int birthWeightInOunces)
        {
            Forename = forename;
            Surname = surname;
            Gender = gender;
            BirthDate = birthDate;
            BirthWeightInOunces = birthWeightInOunces;
        }

        public int Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public int BirthWeightInOunces { get; set; }
        public bool HasPaid { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.Now;

        public TimeSpan BirthTime => BirthDate.TimeOfDay;
        public int BirthWeightInPounds => (int)Math.Floor(BirthWeightInOunces / OuncesInPound);
        public int BirthWeightInOuncesLessPounds => BirthWeightInOunces - (BirthWeightInPounds * (int)OuncesInPound);
    }
}