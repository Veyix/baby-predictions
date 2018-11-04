using System;
using System.Linq;
using BabyPredictions.Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace BabyPredictions.Tests.Domain
{
    public class WinnerCalculatorTests
    {
        [Fact]
        public void CalculateWinners_OnePredictionWithExactDetailsAsBirth_OnePredictionAsWinner()
        {
            // Arrange
            var birthDateAndTime = DateTimeOffset.Now;
            const int weightInOunces = 1;

            var prediction = new Prediction("Expected", "Name", Gender.Male, birthDateAndTime, weightInOunces);
            var birth = new Birth(Gender.Male, birthDateAndTime, weightInOunces);

            using (var container = new TestDataContextContainer(
                nameof(CalculateWinners_OnePredictionWithExactDetailsAsBirth_OnePredictionAsWinner)))
            {
                container.Context.Add(prediction);
                container.Context.Add(birth);

                container.Context.SaveChanges();

                // Act
                var calculator = new WinnerCalculator(container.Context);
                calculator.CalculateWinners();

                // Assert
                var winners = container.Context.Set<Winner>().ToArray();
                Assert.Single(winners);

                var winner = winners[0];
                Assert.Equal("Expected", winner.Forename);
                Assert.Equal("Name", winner.Surname);
                Assert.Equal(10, winner.Points);
                Assert.Equal(1, winner.Position);
            }
        }

        [Fact]
        public void CalculateWinners_ThreePredictionsOneExactEachFurtherAwayFromBirth_FirstSecondAndThirdPlacePredictions()
        {
            // Arrange
            var birthDateAndTime = DateTimeOffset.Now;
            const int weightInOunces = 20;

            var firstPlacePrediction = new Prediction("First", "Place", Gender.Male, birthDateAndTime, weightInOunces);
            var secondPlacePrediction = new Prediction("Second", "Place", Gender.Female, birthDateAndTime.AddDays(-1).AddHours(-1), weightInOunces - 1);
            var thirdPlacePrediction = new Prediction("Third", "Place", Gender.Male, birthDateAndTime.AddDays(2).AddHours(-3), weightInOunces + 2);

            var birth = new Birth(Gender.Male, birthDateAndTime, weightInOunces);

            using (var container = new TestDataContextContainer(
                nameof(CalculateWinners_ThreePredictionsOneExactEachFurtherAwayFromBirth_FirstSecondAndThirdPlacePredictions)))
            {
                container.Context.Add(firstPlacePrediction);
                container.Context.Add(secondPlacePrediction);
                container.Context.Add(thirdPlacePrediction);
                container.Context.Add(birth);

                container.Context.SaveChanges();

                // Act
                var calculator = new WinnerCalculator(container.Context);
                calculator.CalculateWinners();

                // Assert
                var winners = container.Context.Set<Winner>()
                    .OrderBy(x => x.Position)
                    .ToArray();

                Assert.Equal(3, winners.Length);

                var winner1 = winners[0];
                Assert.Equal("First", winner1.Forename);
                Assert.Equal("Place", winner1.Surname);
                Assert.Equal(10, winner1.Points);
                Assert.Equal(1, winner1.Position);

                var winner2 = winners[1];
                Assert.Equal("Second", winner2.Forename);
                Assert.Equal("Place", winner2.Surname);
                Assert.Equal(6, winner2.Points);
                Assert.Equal(2, winner2.Position);

                var winner3 = winners[2];
                Assert.Equal("Third", winner3.Forename);
                Assert.Equal("Place", winner3.Surname);
                Assert.Equal(4, winner3.Points);
                Assert.Equal(3, winner3.Position);
            }
        }

        private sealed class TestDataContextContainer : IDisposable
        {
            private readonly DatabaseContext _context;

            public TestDataContextContainer(string testName)
            {
                var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseInMemoryDatabase(testName)
                    .Options;

                _context = new DatabaseContext(options);
            }
            
            public DatabaseContext Context => _context;

            public void Dispose()
            {
                _context.Dispose();
            }
        }
    }
}