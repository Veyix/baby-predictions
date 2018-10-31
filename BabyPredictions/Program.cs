using System;
using DbUp;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BabyPredictions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (!DeployDatabaseChanges())
            {
                return;
            }

            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static bool DeployDatabaseChanges()
        {
            string connectionString = GetDatabaseConnectionString();

            Console.WriteLine($"Ensuring database with connection string: {connectionString}...");
            EnsureDatabase.For.PostgresqlDatabase(connectionString);

            var deployer = DeployChanges.To.PostgresqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(typeof(Program).Assembly)
                .LogToConsole()
                .Build();

            if (!deployer.IsUpgradeRequired())
            {
                Console.WriteLine("No upgrade required");

                return true;
            }

            Console.WriteLine("Deploying changes...");
            var result = deployer.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Deployment Failed");
                Console.WriteLine(result.Error);
                Console.ResetColor();

                return false;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Deployment successful!");
            Console.ResetColor();

            return true;
        }

        private static string GetDatabaseConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddJsonFile("appsettings.Production.json", optional: true);

            var configuration = builder.Build();
            return configuration.GetConnectionString("Database");
        }
    }
}
