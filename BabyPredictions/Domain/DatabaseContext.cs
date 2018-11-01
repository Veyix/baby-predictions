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
            ConfigurePredictionModel(modelBuilder);
            ConfigureBirthModel(modelBuilder);
            ConfigureWinnerModel(modelBuilder);
        }

        private static void ConfigurePredictionModel(ModelBuilder modelBuilder)
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
            predictionBuilder.Property(x => x.CreatedDate).HasColumnName("createddate");

            predictionBuilder.Ignore(x => x.BirthTime);
            predictionBuilder.Ignore(x => x.BirthWeightInPounds);
            predictionBuilder.Ignore(x => x.BirthWeightInOuncesLessPounds);

            predictionBuilder.ToTable("Prediction");
        }

        private static void ConfigureBirthModel(ModelBuilder modelBuilder)
        {
            var birthBuilder = modelBuilder.Entity<Birth>();
            birthBuilder.HasKey(x => x.Id);

            birthBuilder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            birthBuilder.Property(x => x.Gender).HasColumnName("gender");
            birthBuilder.Property(x => x.BirthDate).HasColumnName("birthdate");
            birthBuilder.Property(x => x.BirthWeightInOunces).HasColumnName("birthweight");

            birthBuilder.Ignore(x => x.BirthTime);
            birthBuilder.Ignore(x => x.BirthWeightInPounds);
            birthBuilder.Ignore(x => x.BirthWeightInOuncesLessPounds);

            birthBuilder.ToTable("Birth");
        }

        private static void ConfigureWinnerModel(ModelBuilder modelBuilder)
        {
            var winnerBuilder = modelBuilder.Entity<Winner>();
            winnerBuilder.HasKey(x => x.Id);

            winnerBuilder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            winnerBuilder.Property(x => x.Forename).HasColumnName("forename");
            winnerBuilder.Property(x => x.Surname).HasColumnName("surname");
            winnerBuilder.Property(x => x.Position).HasColumnName("position");

            winnerBuilder.ToTable("Winner");
        }
    }
}