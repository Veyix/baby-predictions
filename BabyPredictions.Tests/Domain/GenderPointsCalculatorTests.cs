using BabyPredictions.Domain;
using Xunit;

namespace BabyPredictions.Tests.Domain
{
    public class GenderPointsCalculatorTests
    {
        [Fact]
        public void CalculatePoints_TwoPredictionsOneMaleOneFemaleAndBirthIsMale_MalePredictionGetsOnePointFemaleGetsZero()
        {
            // Arrange
            var malePrediction = new Prediction { Gender = Gender.Male };
            var femalePrediction = new Prediction { Gender = Gender.Female };

            var malePoints = new PersonWithPoints(malePrediction);
            var femalePoints = new PersonWithPoints(femalePrediction);

            var containers = new[] { malePoints, femalePoints };

            var birthDetails = new Birth { Gender = Gender.Male };

            // Act
            GenderPointsCalculator.CalculatePoints(birthDetails, containers);

            // Assert
            Assert.Equal(1, malePoints.Points);
            Assert.Equal(0, femalePoints.Points);
        }
    }
}