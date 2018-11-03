using System;
using BabyPredictions.Domain;
using Xunit;

namespace BabyPredictions.Tests.Domain
{
    public class BirthDatePointsCalculatorTests
    {
        [Fact]
        public void CalculatePoints_OnePredictionOnBirthDate_ThreePoints()
        {
            // Arrange
            var BirthDate = DateTime.Today.Add(TimeSpan.FromDays(12));
            var prediction = new Prediction { BirthDate = BirthDate };
            var points = new PersonWithPoints(prediction);
            var birthDetails = new Birth { BirthDate = BirthDate };

            // Act
            BirthDatePointsCalculator.CalculatePoints(birthDetails, new[] { points });

            // Assert
            Assert.Equal(3, points.Points);
        }

        [Fact]
        public void CalculatePoints_TwoPredictionsBothEqualDistanceEitherSideOfBirthDate_ThreePointsEach()
        {
            // Arrange
            var BirthDate = DateTime.Today.Add(TimeSpan.FromDays(12));
            var prediction1 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromDays(11)) };
            var prediction2 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromDays(13)) };
            var points1 = new PersonWithPoints(prediction1);
            var points2 = new PersonWithPoints(prediction2);

            var birthDetails = new Birth { BirthDate = BirthDate };

            // Act
            BirthDatePointsCalculator.CalculatePoints(birthDetails, new[] { points1, points2 });

            // Assert
            Assert.Equal(3, points1.Points);
            Assert.Equal(3, points2.Points);
        }

        [Fact]
        public void CalculatePoints_TwoPredictionsOneDayAndTwoDaysFromBirthDate_ThreePointsAndTwoPointsRespectively()
        {
            // Arrange
            var BirthDate = DateTime.Today.Add(TimeSpan.FromDays(12));
            var prediction1 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromDays(11)) };
            var prediction2 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromDays(14)) };
            var points1 = new PersonWithPoints(prediction1);
            var points2 = new PersonWithPoints(prediction2);

            var birthDetails = new Birth { BirthDate = BirthDate };

            // Act
            BirthDatePointsCalculator.CalculatePoints(birthDetails, new[] { points1, points2 });

            // Assert
            Assert.Equal(3, points1.Points);
            Assert.Equal(2, points2.Points);
        }

        [Fact]
        public void CalculatePoints_ThreePredictionsOneOnBirthDateTwoBothOneDayBelow_ThreePointsAndTwoPointsRespectively()
        {
            // Arrange
            var BirthDate = DateTime.Today.Add(TimeSpan.FromDays(12));
            var prediction1 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromDays(12)) };
            var prediction2 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromDays(11)) };
            var prediction3 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromDays(11)) };
            var points1 = new PersonWithPoints(prediction1);
            var points2 = new PersonWithPoints(prediction2);
            var points3 = new PersonWithPoints(prediction3);

            var birthDetails = new Birth { BirthDate = BirthDate };

            // Act
            BirthDatePointsCalculator.CalculatePoints(birthDetails, new[] { points1, points2, points3 });

            // Assert
            Assert.Equal(3, points1.Points);
            Assert.Equal(2, points2.Points);
            Assert.Equal(2, points3.Points);
        }

        [Fact]
        public void CalculatePoints_ThreePredictionsEachOneDayExtraFromBirthDate_ThreePointsTwoPointsOnePoint()
        {
            // Arrange
            var BirthDate = DateTime.Today.Add(TimeSpan.FromDays(12));
            var prediction1 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromDays(11)) };
            var prediction2 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromDays(10)) };
            var prediction3 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromDays(9)) };
            var points1 = new PersonWithPoints(prediction1);
            var points2 = new PersonWithPoints(prediction2);
            var points3 = new PersonWithPoints(prediction3);

            var birthDetails = new Birth { BirthDate = BirthDate };

            // Act
            BirthDatePointsCalculator.CalculatePoints(birthDetails, new[] { points1, points2, points3 });

            // Assert
            Assert.Equal(3, points1.Points);
            Assert.Equal(2, points2.Points);
            Assert.Equal(1, points3.Points);
        }

        [Fact]
        public void CalculatePoints_FourPredictionsEachOneDayExtraFromBirthDate_ThreePointsTwoPointsOnePointZeroPoints()
        {
            // Arrange
            var BirthDate = DateTime.Today.Add(TimeSpan.FromDays(12));
            var prediction1 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromDays(11)) };
            var prediction2 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromDays(10)) };
            var prediction3 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromDays(9)) };
            var prediction4 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromDays(8)) };
            var points1 = new PersonWithPoints(prediction1);
            var points2 = new PersonWithPoints(prediction2);
            var points3 = new PersonWithPoints(prediction3);
            var points4 = new PersonWithPoints(prediction4);

            var birthDetails = new Birth { BirthDate = BirthDate };

            // Act
            BirthDatePointsCalculator.CalculatePoints(birthDetails, new[] { points1, points2, points3, points4 });

            // Assert
            Assert.Equal(3, points1.Points);
            Assert.Equal(2, points2.Points);
            Assert.Equal(1, points3.Points);
            Assert.Equal(0, points4.Points);
        }
    }
}