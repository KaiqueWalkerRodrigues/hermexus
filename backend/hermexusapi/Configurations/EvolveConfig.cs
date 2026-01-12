using EvolveDb;
using hermexusapi.Models.Context;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Serilog;

namespace RestWithASPNET10Erudio.Configurations
{
    public static class EvolveConfig
    {

        public static IServiceCollection AddEvolveConfiguration(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                var connectionString = configuration.GetConnectionString("connMySQL");

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new ArgumentNullException(
                        "Connection string 'MSSQLServerSQLConnectionString' not found.");
                }

                try
                {
                    using var connection = new MySqlConnection(connectionString);

                    using var serviceProvider = services.BuildServiceProvider();
                    var logger = serviceProvider.GetRequiredService<ILogger<MySQLContext>>();

                    var evolve = new Evolve(connection, msg => logger.LogInformation(msg))
                    {
                        Locations = new[] { "Database/Migrations" },
                        IsEraseDisabled = true,
                        MetadataTableName = "changelog",
                        PlaceholderPrefix = "${",
                        PlaceholderSuffix = "}",
                        SqlMigrationSuffix = ".sql"
                    };

                    evolve.Migrate();
                    logger.LogInformation("EVOLVE: Migrations applied successfully.");
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while migrating the database.");
                    throw;
                }
            }
            return services;
        }

        public static void ExecuteMigrations(string connectionString)
        {
            using var evolveConnection = new MySqlConnection(connectionString);
            var evolve = new Evolve(
                evolveConnection,
                msg => Log.Information(msg)
                )
            {
                Locations = new[] { "Database/Migrations" },
                IsEraseDisabled = true
            };
            evolve.Migrate();
        }
    }
}