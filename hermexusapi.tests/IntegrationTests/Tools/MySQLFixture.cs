using hermexusapi.Configurations;
using System.Reflection;
using Testcontainers.MySql;

namespace hermexusapi.tests.IntegrationTests.Tools
{
    public class MySQLFixture : IAsyncLifetime
    {
        public MySqlContainer Container { get; }
        public string ConnectionString => Container.GetConnectionString();

        public MySQLFixture()
        {
            Container = new MySqlBuilder("mysql:8.0.44")
                .WithPassword("strong_password")
                .Build();
        }

        public async Task InitializeAsync()
        {
            await Container.StartAsync();

            try
            {
                // Pega o diretório onde o .dll do teste está (bin/Debug/net10.0)
                var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                // Monta o caminho exato para a pasta que você definiu no .csproj
                var migrationsPath = Path.Combine(assemblyPath!, "Database", "Migrations");

                Console.WriteLine($"Procurando migrations em: {migrationsPath}");

                // Verifique se a pasta realmente existe e tem arquivos
                if (!Directory.Exists(migrationsPath) || !Directory.GetFiles(migrationsPath).Any())
                {
                    throw new DirectoryNotFoundException($"Scripts não encontrados em: {migrationsPath}");
                }

                // Ajuste seu método ExecuteMigrations para aceitar o local das pastas
                EvolveConfig.ExecuteMigrations(ConnectionString, migrationsPath);

                Console.WriteLine("Migrations executadas com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro crítico: {ex.Message}");
                throw;
            }
        }

        public async Task DisposeAsync()
        {
            await Container.StopAsync();
            await Container.DisposeAsync();
        }
    }
}