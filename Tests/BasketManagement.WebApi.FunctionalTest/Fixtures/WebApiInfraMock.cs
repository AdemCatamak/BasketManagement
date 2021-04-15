using System;
using System.Net.Http;
using System.Threading.Tasks;
using BasketManagement.Shared.Infrastructure.Db;
using BasketManagement.Shared.Infrastructure.MassTransitComponents;
using DotNet.Testcontainers.Containers.Builders;
using DotNet.Testcontainers.Containers.Configurations.Databases;
using DotNet.Testcontainers.Containers.Configurations.MessageBrokers;
using DotNet.Testcontainers.Containers.Modules;
using DotNet.Testcontainers.Containers.Modules.Databases;
using DotNet.Testcontainers.Containers.Modules.MessageBrokers;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BasketManagement.WebApi.FunctionalTest.Fixtures
{
    public class WebApiInfraMockSingleton
    {
        private static readonly Lazy<WebApiInfraMock> _lazy =
            new Lazy<WebApiInfraMock>(() => new WebApiInfraMock());

        public static WebApiInfraMock Instance => _lazy.Value;

        private WebApiInfraMockSingleton()
        {
        }
    }

    public class WebApiInfraMock : IDisposable
    {
        public IServiceProvider ServiceProvider { get; }

        private IHost TestServer { get; }
        private readonly TestcontainersContainer _dbTestContainer;
        private readonly TestcontainersContainer _mqTestContainer;

        public WebApiInfraMock()
        {
            IHostBuilder hostBuilder = Program.HostBuilder(null!);
            hostBuilder.UseEnvironment("Development")
                .ConfigureWebHost(builder =>
                {
                    builder.UseTestServer();
                    builder.ConfigureAppConfiguration((context, configurationBuilder) => { configurationBuilder.AddJsonFile("appsettings.test.json"); });
                });

            TestServer = hostBuilder.Build();
            ServiceProvider = TestServer.Services;

            _dbTestContainer = BuildSqlServerTestContainer(ServiceProvider);
            _mqTestContainer = BuildRabbitMqTestContainer(ServiceProvider);

            Task dbRunTask = _dbTestContainer.StartAsync();
            Task mqRunTask = _mqTestContainer.StartAsync();
            Task.WaitAll(dbRunTask, mqRunTask);

            Program.RunMigration(TestServer.Services);
            TestServer.Start();
        }

        private TestcontainersContainer BuildRabbitMqTestContainer(IServiceProvider dropyServiceProvider)
        {
            var massTransitBusConfiguration = dropyServiceProvider.GetRequiredService<MassTransitBusConfiguration>();
            ITestcontainersBuilder<RabbitMqTestcontainer> testContainersBuilder
                = new TestcontainersBuilder<RabbitMqTestcontainer>()
                    .WithMessageBroker(new RabbitMqTestcontainerConfiguration
                    {
                        Username = massTransitBusConfiguration.UserName,
                        Password = massTransitBusConfiguration.Password,
                        Port = massTransitBusConfiguration.Port
                    });

            RabbitMqTestcontainer messageBrokerTestContainer = testContainersBuilder.Build();
            return messageBrokerTestContainer;
        }

        private TestcontainersContainer BuildSqlServerTestContainer(IServiceProvider dropyServiceProvider)
        {
            var dbConfig = dropyServiceProvider.GetRequiredService<AppDbConfig>();
            SqlConnectionStringBuilder dbConnectionStringBuilder = new SqlConnectionStringBuilder(dbConfig.ConnectionStr);
            int portNumber = int.Parse(dbConnectionStringBuilder.DataSource.Split(",")[1]);
            ITestcontainersBuilder<MsSqlTestcontainer> testContainersBuilder
                = new TestcontainersBuilder<MsSqlTestcontainer>()
                    .WithDatabase(new MsSqlTestcontainerConfiguration
                    {
                        Password = dbConnectionStringBuilder.Password,
                        Port = portNumber
                    });

            MsSqlTestcontainer dbTestContainer = testContainersBuilder.Build();
            return dbTestContainer;
        }


        public void Dispose()
        {
            TestServer?.StopAsync().GetAwaiter().GetResult();
            TestServer?.Dispose();
            _dbTestContainer?.DisposeAsync().GetAwaiter().GetResult();
            _mqTestContainer?.DisposeAsync().GetAwaiter().GetResult();
        }

        public HttpClient CreateHttpClient()
        {
            return TestServer.GetTestClient();
        }
    }
}