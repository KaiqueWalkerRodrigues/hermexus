using RestWithASPNET10Erudio.Configurations;
using Testcontainers.MySql;

namespace hermexusapi.tests.IntegrationTests.Tools
{
    public class MySQLFixture : IAsyncLifetime
    {
        public MySqlContainer Container { get; }
        public string ConnectionString => Container.GetConnectionString();

        public MySQLFixture()
        {
            Container = new MySqlBuilder("mysql:8.0")
                .WithPassword("strong_password")
                .Build();
        }

        public async Task InitializeAsync()
        {
            await Container.StartAsync();
            EvolveConfig.ExecuteMigrations(ConnectionString);
        }

        public async Task DisposeAsync()
        {
            await Container.StopAsync();
            await Container.DisposeAsync();
        }
    }
}