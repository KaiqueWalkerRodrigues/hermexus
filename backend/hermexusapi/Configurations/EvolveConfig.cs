using EvolveDb;
using MySqlConnector;
using Serilog;

namespace hermexusapi.Configurations
{
    public static class EvolveConfig
    {
        public static void ExecuteMigrations(IConfiguration configuration, IWebHostEnvironment environment)
        {
            if (!environment.IsDevelopment()) return;
            var connectionString = configuration.GetConnectionString("connMySQL");
            if (string.IsNullOrEmpty(connectionString)) return;

            ExecuteMigrations(connectionString);
        }

        // Adicione o parâmetro opcional migrationFolder
        public static void ExecuteMigrations(string connectionString, string? migrationFolder = null)
        {
            if (string.IsNullOrEmpty(connectionString)) return;

            // Se migrationFolder não for passado, usa o padrão relativo
            string folderToUse = migrationFolder ?? "Database/Migrations";

            int retryCount = 0;
            const int maxRetries = 10;

            while (retryCount < maxRetries)
            {
                try
                {
                    using var connection = new MySqlConnection(connectionString);
                    var evolve = new Evolve(connection, msg => Log.Information("EVOLVE: {Msg}", msg))
                    {
                        // Usa a variável folderToUse aqui
                        Locations = new[] { folderToUse },
                        IsEraseDisabled = true,
                        MetadataTableName = "changelog",
                        SqlMigrationSuffix = ".sql"
                    };

                    evolve.Migrate();
                    Log.Information("EVOLVE: Migrações aplicadas com sucesso em {Path}", folderToUse);
                    return;
                }
                catch (Exception ex)
                {
                    retryCount++;
                    if (retryCount >= maxRetries)
                    {
                        Log.Fatal(ex, "EVOLVE: Não foi possível conectar ao MySQL após várias tentativas.");
                        throw;
                    }

                    Log.Warning("EVOLVE: Banco iniciando... Tentativa {Count}/{Max}. Aguardando 5s...", retryCount, maxRetries);
                    Thread.Sleep(5000);
                }
            }
        }
    }
}