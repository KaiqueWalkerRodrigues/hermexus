using hermexusapi.Configurations;
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

            // Adicione logging
            Console.WriteLine($"MySQL Container Started: {Container.GetConnectionString()}");

            try
            {
                EvolveConfig.ExecuteMigrations(ConnectionString);
                Console.WriteLine("Migrations executed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Migration error: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
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