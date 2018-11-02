namespace BabyPredictions.Domain
{
    public class PersonWithPoints
    {
        public PersonWithPoints(Prediction prediction)
        {
            Prediction = prediction;
        }

        public Prediction Prediction { get; }
        public int Points { get; set; }
    }
}