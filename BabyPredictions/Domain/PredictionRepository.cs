using System;
using System.Collections.Generic;

namespace BabyPredictions.Domain
{
    public class PredictionRepository
    {
        private static readonly List<Prediction> Predictions = new List<Prediction>();

        static PredictionRepository()
        {
            Predictions.Add(new Prediction
            {
                Forename = "Samuel",
                Surname = "Slade",
                Gender = Gender.Male,
                BirthDate = DateTimeOffset.Now.Date,
                BirthTime = DateTimeOffset.Now.TimeOfDay,
                BirthWeightInOunces = (8 /* lbs */ * 16 /* ounces */)
            });

            Predictions.Add(new Prediction
            {
                Forename = "Mikayla",
                Surname = "Valentine",
                Gender = Gender.Female,
                BirthDate = DateTimeOffset.Now.Date.AddDays(3),
                BirthTime = DateTimeOffset.Now.TimeOfDay,
                BirthWeightInOunces = (7 /* lbs */ * 16 /* ounces */) + 4 /* ounces */
            });
        }

        public IReadOnlyCollection<Prediction> GetPredictions()
        {
            return Predictions.ToArray();
        }
    }
}