using hermexusapi.Configurations;
using hermexusapi.DTO.V1;
using System.Net.Http.Json;
using System.Reflection;
using Testcontainers.MySql;

namespace hermexusapi.tests.IntegrationTests.Tools
{
    public class MySQLFixture : IAsyncLifetime
    {
        public MySqlContainer Container { get; }
        public string ConnectionString => Container.GetConnectionString();
        public TokenDTO? UserToken { get; private set; }

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
                var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                var migrationsPath = Path.Combine(assemblyPath!, "Database", "Migrations");

                Console.WriteLine($"Procurando migrations em: {migrationsPath}");

                if (!Directory.Exists(migrationsPath) || !Directory.GetFiles(migrationsPath).Any())
                {
                    throw new DirectoryNotFoundException($"Scripts não encontrados em: {migrationsPath}");
                }

                EvolveConfig.ExecuteMigrations(ConnectionString, migrationsPath);

                Console.WriteLine("Migrations executadas com sucesso.");

                await SetupInitialUserAndToken();

                Console.WriteLine("Initial User And Token Created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro crítico: {ex.Message}");
                throw;
            }
        }

        private async Task SetupInitialUserAndToken()
        {
            var factory = new CustomWebApplicationFactory<Program>(ConnectionString);
            var client = factory.CreateClient();

            var newUser = new UserDTO
            {
                Username = "jonny.test",
                Password = "johntest@",
                Name = "Admin Test",
                IsActive = true
            };

            var createResponse = await client.PostAsJsonAsync("/api/user/v1", newUser);

            var loginResponse = await client.PostAsJsonAsync("/api/auth/signin", new
            {
                Username = "jonny.test",
                Password = "johntest@"
            });

            if (loginResponse.IsSuccessStatusCode)
            {
                UserToken = await loginResponse.Content.ReadFromJsonAsync<TokenDTO>();
            }
        }

        public async Task DisposeAsync()
        {
            await Container.StopAsync();
            await Container.DisposeAsync();
        }
    }
}