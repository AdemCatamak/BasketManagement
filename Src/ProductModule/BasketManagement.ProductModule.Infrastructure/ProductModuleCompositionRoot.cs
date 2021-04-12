using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BasketManagement.ProductModule.Application.CommandHandlers;
using BasketManagement.ProductModule.Domain.Repositories;
using BasketManagement.ProductModule.Infrastructure.Db;
using BasketManagement.ProductModule.Infrastructure.Db.EntityConfigurations;
using BasketManagement.ProductModule.Infrastructure.Db.Migrations;
using BasketManagement.Shared.Infrastructure;
using BasketManagement.Shared.Infrastructure.Db;
using BasketManagement.Shared.Infrastructure.DomainEventBroker;

namespace BasketManagement.ProductModule.Infrastructure
{
    public class ProductModuleCompositionRoot : ICompositionRoot
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEntityTypeConfigurationAssembly, ProductModuleTypeConfigurationAssembly>();
            services.AddSingleton<BaseDbMigrationEngine, ProductModuleMigrationRunner>();
            services.AddScoped<IProductDbContext, ProductDbContext>();
            services.AddDomainMessageBroker(typeof(CreateBookCommandHandler).Assembly);
        }
    }
}