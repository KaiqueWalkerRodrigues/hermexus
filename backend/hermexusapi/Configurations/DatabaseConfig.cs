using hermexusapi.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace hermexusapi.Configuration
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var connectionString = configuration.GetConnectionString("connMySQL");

            // Build a temporary service provider to resolve the logger
            using var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<ILogger<MySQLContext>>();

            try
            {
                logger.LogDebug("DATABASE: Attempting to detect MySQL server version...");

                // Detect the server version (required by Pomelo)
                var serverVersion = ServerVersion.AutoDetect(connectionString);

                services.AddDbContext<MySQLContext>(options =>
                    options.UseMySql(connectionString, serverVersion,
                        mySqlOptions =>
                        {
                            mySqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null);
                        }));

                logger.LogInformation("DATABASE: MySQL configuration applied successfully. Server version: {Version}", serverVersion);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, "DATABASE: Critical error during MySQL connection setup. Check if the database is running on the specified port.");
                throw; // Rethrow to prevent the application from starting with a broken connection
            }
        }
    }
}