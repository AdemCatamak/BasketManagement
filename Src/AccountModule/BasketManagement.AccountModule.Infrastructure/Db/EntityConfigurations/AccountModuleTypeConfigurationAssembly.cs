using System.Reflection;
using BasketManagement.Shared.Infrastructure.Db;

namespace BasketManagement.AccountModule.Infrastructure.Db.EntityConfigurations
{
    public class AccountModuleTypeConfigurationAssembly : IEntityTypeConfigurationAssembly
    {
        public Assembly Assembly => this.GetType().Assembly;
    }
}