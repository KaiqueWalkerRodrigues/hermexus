using hermexusapi.Models.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace hermexusapi.tests.IntegrationTests.Tools
{
    public class CustomWebApplicationFactory<TProgram>
        : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly string _connectionString;

        public CustomWebApplicationFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void ConfigureWebHost(
            IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                var testConfigPath = Path.Combine(
                    Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().Location)!,
                    "appsettings.Test.json");

                config.Sources.Clear();
                config.AddJsonFile(
                    testConfigPath,
                    optional: false,
                    reloadOnChange: true);
            });
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<MySQLContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<MySQLContext>(options =>
                {
                    // Use o método correto para MySQL
                    var serverVersion = ServerVersion.AutoDetect(_connectionString);
                    options.UseMySql(_connectionString, serverVersion);
                });
            });
        }
    }
}
