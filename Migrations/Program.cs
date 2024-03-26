using System.Globalization;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var serviceProvider = CreateServices())
            using (var scope = serviceProvider.CreateScope())
            {
                // Put the database update into a scope to ensure
                // that all resources will be disposed.
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// Configure the dependency injection services.
        /// </summary>
        private static ServiceProvider CreateServices()
        {
            var releaseMode = FromEnvironmentOrDefault("RELEASE_MODE", false);
            var connectionString = releaseMode
                ? Environment.GetEnvironmentVariable("DB_CONNECTION_STRING_RELEASE")
                : Environment.GetEnvironmentVariable("DB_CONNECTION_STRING_DEBUG");

            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(Program).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        /// <summary>
        /// Update the database.
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner.
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            var migrateUp = FromEnvironmentOrDefault("MIGRATE_UP", true);

            if (migrateUp)
            {
                runner.MigrateUp();
                return;
            }

            var version = FromEnvironmentOrDefault("MIGRATE_DOWN_TO", 0);
            runner.MigrateDown(version);
        }

        private static T FromEnvironmentOrDefault<T>(string variableName, T defaultValue) where T: IParsable<T>
        {
            var valueString = Environment.GetEnvironmentVariable(variableName);
            if (valueString is null)
            {
                return defaultValue;
            }
            
            return T.TryParse(valueString, CultureInfo.InvariantCulture, out var res) 
                ? res 
                : defaultValue;
        }
    }
}