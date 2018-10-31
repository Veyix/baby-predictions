using Microsoft.EntityFrameworkCore;

namespace BabyPredictions.Domain
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var predictionBuilder = modelBuilder.Entity<Prediction>();
            predictionBuilder.HasKey(x => x.Id);

            predictionBuilder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            predictionBuilder.Property(x => x.Forename).HasColumnName("forename");
            predictionBuilder.Property(x => x.Surname).HasColumnName("surname");
            predictionBuilder.Property(x => x.Gender).HasColumnName("gender");
            predictionBuilder.Property(x => x.BirthDate).HasColumnName("birthdate");
            predictionBuilder.Property(x => x.BirthWeightInOunces).HasColumnName("birthweight");
            predictionBuilder.Property(x => x.HasPaid).HasColumnName("haspaid");

            predictionBuilder.Ignore(x => x.BirthTime);
            predictionBuilder.Ignore(x => x.BirthWeightInPounds);
            predictionBuilder.Ignore(x => x.BirthWeightInOuncesLessPounds);

            predictionBuilder.ToTable("Prediction");
        }
    }
}