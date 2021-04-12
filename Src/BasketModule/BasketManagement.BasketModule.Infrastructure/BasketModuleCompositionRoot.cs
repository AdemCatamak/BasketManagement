using BasketManagement.BasketModule.Application.CommandHandlers;
using BasketManagement.BasketModule.Application.Services;
using BasketManagement.BasketModule.Domain.Repositories;
using BasketManagement.BasketModule.Domain.Services;
using BasketManagement.BasketModule.Infrastructure.Db;
using BasketManagement.BasketModule.Infrastructure.Db.EntityConfigurations;
using BasketManagement.BasketModule.Infrastructure.Db.Migrations;
using BasketManagement.Shared.Infrastructure;
using BasketManagement.Shared.Infrastructure.Db;
using BasketManagement.Shared.Infrastructure.DomainEventBroker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BasketManagement.BasketModule.Infrastructure
{
    public class BasketModuleCompositionRoot : ICompositionRoot
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomainMessageBroker(typeof(CreateBasketCommandHandler).Assembly);

            services.AddSingleton<IEntityTypeConfigurationAssembly, BasketModuleTypeConfigurationAssembly>();
            services.AddSingleton<BaseDbMigrationEngine, BasketModuleMigrationRunner>();
            services.AddScoped<IBasketDbContext, BasketDbContext>();

            services.AddScoped<IBasketStateMachineFactory, BasketStateMachineFactory>();

            services.AddSingleton<IStockActionCorrelationIdGenerator, StockActionCorrelationIdGenerator>();
        }
    }
}