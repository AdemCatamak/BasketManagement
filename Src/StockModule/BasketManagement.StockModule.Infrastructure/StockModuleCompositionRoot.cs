using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BasketManagement.Shared.Infrastructure;
using BasketManagement.Shared.Infrastructure.Db;
using BasketManagement.Shared.Infrastructure.DomainEventBroker;
using BasketManagement.StockModule.Application.CommandHandlers;
using BasketManagement.StockModule.Application.Rules;
using BasketManagement.StockModule.Domain.Repositories;
using BasketManagement.StockModule.Domain.Rules;
using BasketManagement.StockModule.Infrastructure.Db;
using BasketManagement.StockModule.Infrastructure.Db.EntityConfigurations;
using BasketManagement.StockModule.Infrastructure.Db.Migrations;

namespace BasketManagement.StockModule.Infrastructure
{
    public class StockModuleCompositionRoot : ICompositionRoot
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomainMessageBroker(typeof(InitializeStockCommandHandler).Assembly);

            services.AddSingleton<BaseDbMigrationEngine, StockModuleMigrationRunner>();
            services.AddSingleton<IEntityTypeConfigurationAssembly, StockModuleEntityConfigurationAssembly>();
            services.AddScoped<IStockDbContext, StockDbContext>();

            services.AddScoped<IStockActionUniqueChecker, StockActionUniqueChecker>();
            services.AddScoped<IStockSnapshotUniqueChecker, StockSnapshotUniqueChecker>();
            services.AddScoped<IStockUniqueChecker, StockUniqueChecker>();
        }
    }
}