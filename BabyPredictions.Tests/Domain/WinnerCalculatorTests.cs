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

            var prediction = new Prediction
            {
                Forename = "Expected",
                Surname = "Name",
                Gender = Gender.Male,
                BirthDate = birthDateAndTime,
                BirthWeightInOunces = weightInOunces
            };

            var birth = new Birth
            {
                Gender = Gender.Male,
                BirthDate = birthDateAndTime,
                BirthWeightInOunces = weightInOunces
            };

            using (var container = new TestDataContextContainer(nameof(CalculateWinners_OnePredictionWithExactDetailsAsBirth_OnePredictionAsWinner)))
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