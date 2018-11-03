using BabyPredictions.Domain;
using Xunit;

namespace BabyPredictions.Tests.Domain
{
    public class BirthWeightPointsCalculatorTests
    {
        [Fact]
        public void CalculatePoints_OnePredictionOnWeight_ThreePoints()
        {
            // Arrange
            var prediction = new Prediction { BirthWeightInOunces = 1 };
            var points = new PersonWithPoints(prediction);
            var birthDetails = new Birth { BirthWeightInOunces = 1 };

            // Act
            BirthWeightPointsCalculator.CalculatePoints(birthDetails, new[] { points });

            // Assert
            Assert.Equal(3, points.Points);
        }

        [Fact]
        public void CalculatePoints_TwoPredictionsBothEqualDistanceEitherSizeOfWeight_ThreePointsEach()
        {
            // Arrange
            var prediction1 = new Prediction { BirthWeightInOunces = 1 };
            var prediction2 = new Prediction { BirthWeightInOunces = 3 };
            var points1 = new PersonWithPoints(prediction1);
            var points2 = new PersonWithPoints(prediction2);

            var birthDetails = new Birth { BirthWeightInOunces = 2 };

            // Act
            BirthWeightPointsCalculator.CalculatePoints(birthDetails, new[] { points1, points2 });

            // Assert
            Assert.Equal(3, points1.Points);
            Assert.Equal(3, points2.Points);
        }

        [Fact]
        public void CalculatePoints_TwoPredictionsOneOunceAndTwoOuncesFromWeight_ThreePointsAndTwoPointsRespectively()
        {
            // Arrange
            var prediction1 = new Prediction { BirthWeightInOunces = 1 };
            var prediction2 = new Prediction { BirthWeightInOunces = 4 };
            var points1 = new PersonWithPoints(prediction1);
            var points2 = new PersonWithPoints(prediction2);

            var birthDetails = new Birth { BirthWeightInOunces = 2 };

            // Act
            BirthWeightPointsCalculator.CalculatePoints(birthDetails, new[] { points1, points2 });

            // Assert
            Assert.Equal(3, points1.Points);
            Assert.Equal(2, points2.Points);
        }

        [Fact]
        public void CalculatePoints_ThreePredictionsOneOnWeightTwoBothOneOunceBelow_ThreePointsAndTwoPointsRespectively()
        {
            // Arrange
            var prediction1 = new Prediction { BirthWeightInOunces = 1 };
            var prediction2 = new Prediction { BirthWeightInOunces = 1 };
            var prediction3 = new Prediction { BirthWeightInOunces = 3 };
            var points1 = new PersonWithPoints(prediction1);
            var points2 = new PersonWithPoints(prediction2);
            var points3 = new PersonWithPoints(prediction3);

            var birthDetails = new Birth { BirthWeightInOunces = 3 };

            // Act
            BirthWeightPointsCalculator.CalculatePoints(birthDetails, new[] { points1, points2, points3 });

            // Assert
            Assert.Equal(2, points1.Points);
            Assert.Equal(2, points2.Points);
            Assert.Equal(3, points3.Points);
        }

        [Fact]
        public void CalculatePoints_ThreePredictionsEachOneOunceExtraFromWeight_ThreePointsTwoPointsOnePoint()
        {
            // Arrange
            var prediction1 = new Prediction { BirthWeightInOunces = 1 };
            var prediction2 = new Prediction { BirthWeightInOunces = 2 };
            var prediction3 = new Prediction { BirthWeightInOunces = 3 };
            var points1 = new PersonWithPoints(prediction1);
            var points2 = new PersonWithPoints(prediction2);
            var points3 = new PersonWithPoints(prediction3);

            var birthDetails = new Birth { BirthWeightInOunces = 4 };

            // Act
            BirthWeightPointsCalculator.CalculatePoints(birthDetails, new[] { points1, points2, points3 });

            // Assert
            Assert.Equal(1, points1.Points);
            Assert.Equal(2, points2.Points);
            Assert.Equal(3, points3.Points);
        }

        [Fact]
        public void CalculatePoints_FourPredictionsEachOneOunceExtraFromWeight_ThreePointsTwoPointsOnePointZeroPoints()
        {
            // Arrange
            var prediction1 = new Prediction { BirthWeightInOunces = 1 };
            var prediction2 = new Prediction { BirthWeightInOunces = 2 };
            var prediction3 = new Prediction { BirthWeightInOunces = 3 };
            var prediction4 = new Prediction { BirthWeightInOunces = 4 };
            var points1 = new PersonWithPoints(prediction1);
            var points2 = new PersonWithPoints(prediction2);
            var points3 = new PersonWithPoints(prediction3);
            var points4 = new PersonWithPoints(prediction4);

            var birthDetails = new Birth { BirthWeightInOunces = 5 };

            // Act
            BirthWeightPointsCalculator.CalculatePoints(birthDetails, new[] { points1, points2, points3, points4 });

            // Assert
            Assert.Equal(0, points1.Points);
            Assert.Equal(1, points2.Points);
            Assert.Equal(2, points3.Points);
            Assert.Equal(3, points4.Points);
        }
    }
}