using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BasketManagement.OrderModule.Application.CommandHandlers;
using BasketManagement.OrderModule.Application.Services;
using BasketManagement.OrderModule.Domain.Repositories;
using BasketManagement.OrderModule.Domain.Services;
using BasketManagement.OrderModule.Infrastructure.Db;
using BasketManagement.OrderModule.Infrastructure.Db.EntityConfigurations;
using BasketManagement.OrderModule.Infrastructure.Db.Migrations;
using BasketManagement.Shared.Infrastructure;
using BasketManagement.Shared.Infrastructure.Db;
using BasketManagement.Shared.Infrastructure.DomainEventBroker;

namespace BasketManagement.OrderModule.Infrastructure
{
    public class OrderModuleCompositionRoot : ICompositionRoot
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomainMessageBroker(typeof(CreateOrderCommandHandler).Assembly);

            services.AddSingleton<IEntityTypeConfigurationAssembly, OrderModuleTypeConfigurationAssembly>();
            services.AddSingleton<BaseDbMigrationEngine, OrderModuleMigrationRunner>();
            services.AddScoped<IOrderDbContext, OrderDbContext>();

            services.AddScoped<IOrderStateMachineFactory, OrderStateMachineFactory>();
        }
    }
}