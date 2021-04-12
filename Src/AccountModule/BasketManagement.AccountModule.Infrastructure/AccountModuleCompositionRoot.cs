using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BasketManagement.AccountModule.Application.CommandHandlers;
using BasketManagement.AccountModule.Application.Rules;
using BasketManagement.AccountModule.Application.Services;
using BasketManagement.AccountModule.Domain.Repositories;
using BasketManagement.AccountModule.Domain.Rules;
using BasketManagement.AccountModule.Domain.Services;
using BasketManagement.AccountModule.Infrastructure.Db;
using BasketManagement.AccountModule.Infrastructure.Db.EntityConfigurations;
using BasketManagement.AccountModule.Infrastructure.Db.Migrations;
using BasketManagement.Shared.Infrastructure;
using BasketManagement.Shared.Infrastructure.Db;
using BasketManagement.Shared.Infrastructure.DomainEventBroker;

namespace BasketManagement.AccountModule.Infrastructure
{
    public class AccountModuleCompositionRoot : ICompositionRoot
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDomainMessageBroker(typeof(CreateAccountCommandHandler).Assembly);
            services.AddSingleton<IEntityTypeConfigurationAssembly, AccountModuleTypeConfigurationAssembly>();
            services.AddSingleton<BaseDbMigrationEngine, AccountModuleMigrationRunner>();
            services.AddScoped<IAccountDbContext, AccountDbContext>();

            services.AddScoped<IUsernameUniqueChecker, UsernameUniqueChecker>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IAccessTokenGenerator, JwtAccessTokenGenerator>();
        }
    }
}