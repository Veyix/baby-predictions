using System;
using BabyPredictions.Domain;
using Xunit;

namespace BabyPredictions.Tests.Domain
{
    public class BirthTimePointsCalculatorTests
    {
        [Fact]
        public void CalculatePoints_OnePredictionOnBirthTime_ThreePoints()
        {
            // Arrange
            var birthTime = DateTime.Today.Add(TimeSpan.FromHours(12));
            var prediction = new Prediction { BirthDate = birthTime };
            var points = new PersonWithPoints(prediction);
            var birthDetails = new Birth { BirthDate = birthTime };

            // Act
            BirthTimePointsCalculator.CalculatePoints(birthDetails, new[] { points });

            // Assert
            Assert.Equal(3, points.Points);
        }

        [Fact]
        public void CalculatePoints_TwoPredictionsBothEqualDistanceEitherSideOfBirthTime_ThreePointsEach()
        {
            // Arrange
            var birthTime = DateTime.Today.Add(TimeSpan.FromHours(12));
            var prediction1 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromHours(11)) };
            var prediction2 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromHours(13)) };
            var points1 = new PersonWithPoints(prediction1);
            var points2 = new PersonWithPoints(prediction2);

            var birthDetails = new Birth { BirthDate = birthTime };

            // Act
            BirthTimePointsCalculator.CalculatePoints(birthDetails, new[] { points1, points2 });

            // Assert
            Assert.Equal(3, points1.Points);
            Assert.Equal(3, points2.Points);
        }

        [Fact]
        public void CalculatePoints_TwoPredictionsOneHourAndTwoHoursFromBirthTime_ThreePointsAndTwoPointsRespectively()
        {
            // Arrange
            var birthTime = DateTime.Today.Add(TimeSpan.FromHours(12));
            var prediction1 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromHours(11)) };
            var prediction2 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromHours(14)) };
            var points1 = new PersonWithPoints(prediction1);
            var points2 = new PersonWithPoints(prediction2);

            var birthDetails = new Birth { BirthDate = birthTime };

            // Act
            BirthTimePointsCalculator.CalculatePoints(birthDetails, new[] { points1, points2 });

            // Assert
            Assert.Equal(3, points1.Points);
            Assert.Equal(2, points2.Points);
        }

        [Fact]
        public void CalculatePoints_ThreePredictionsOneOnBirthTimeTwoBothOneHourBelow_ThreePointsAndTwoPointsRespectively()
        {
            // Arrange
            var birthTime = DateTime.Today.Add(TimeSpan.FromHours(12));
            var prediction1 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromHours(12)) };
            var prediction2 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromHours(11)) };
            var prediction3 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromHours(11)) };
            var points1 = new PersonWithPoints(prediction1);
            var points2 = new PersonWithPoints(prediction2);
            var points3 = new PersonWithPoints(prediction3);

            var birthDetails = new Birth { BirthDate = birthTime };

            // Act
            BirthTimePointsCalculator.CalculatePoints(birthDetails, new[] { points1, points2, points3 });

            // Assert
            Assert.Equal(3, points1.Points);
            Assert.Equal(2, points2.Points);
            Assert.Equal(2, points3.Points);
        }

        [Fact]
        public void CalculatePoints_ThreePredictionsEachOneHourExtraFromBirthTime_ThreePointsTwoPointsOnePoint()
        {
            // Arrange
            var birthTime = DateTime.Today.Add(TimeSpan.FromHours(12));
            var prediction1 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromHours(11)) };
            var prediction2 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromHours(10)) };
            var prediction3 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromHours(9)) };
            var points1 = new PersonWithPoints(prediction1);
            var points2 = new PersonWithPoints(prediction2);
            var points3 = new PersonWithPoints(prediction3);

            var birthDetails = new Birth { BirthDate = birthTime };

            // Act
            BirthTimePointsCalculator.CalculatePoints(birthDetails, new[] { points1, points2, points3 });

            // Assert
            Assert.Equal(3, points1.Points);
            Assert.Equal(2, points2.Points);
            Assert.Equal(1, points3.Points);
        }

        [Fact]
        public void CalculatePoints_FourPredictionsEachOneHourExtraFromBirthTime_ThreePointsTwoPointsOnePointZeroPoints()
        {
            // Arrange
            var birthTime = DateTime.Today.Add(TimeSpan.FromHours(12));
            var prediction1 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromHours(11)) };
            var prediction2 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromHours(10)) };
            var prediction3 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromHours(9)) };
            var prediction4 = new Prediction { BirthDate = DateTime.Today.Add(TimeSpan.FromHours(8)) };
            var points1 = new PersonWithPoints(prediction1);
            var points2 = new PersonWithPoints(prediction2);
            var points3 = new PersonWithPoints(prediction3);
            var points4 = new PersonWithPoints(prediction4);

            var birthDetails = new Birth { BirthDate = birthTime };

            // Act
            BirthTimePointsCalculator.CalculatePoints(birthDetails, new[] { points1, points2, points3, points4 });

            // Assert
            Assert.Equal(3, points1.Points);
            Assert.Equal(2, points2.Points);
            Assert.Equal(1, points3.Points);
            Assert.Equal(0, points4.Points);
        }
    }
}