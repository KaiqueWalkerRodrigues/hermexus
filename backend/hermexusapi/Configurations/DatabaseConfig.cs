using hermexusapi.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace hermexusapi.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("connMySQL");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 44));

            services.AddDbContext<MySQLContext>(options =>
                options.UseMySql(connectionString, serverVersion,
                    mySqlOptions =>
                    {
                        mySqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 10,
                            maxRetryDelay: TimeSpan.FromSeconds(5),
                            errorNumbersToAdd: null);
                    }));
        }
    }
}