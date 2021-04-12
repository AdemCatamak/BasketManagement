using System.Reflection;
using BasketManagement.Shared.Infrastructure.Db;

namespace BasketManagement.BasketModule.Infrastructure.Db.EntityConfigurations
{
    public class BasketModuleTypeConfigurationAssembly : IEntityTypeConfigurationAssembly
    {
        public Assembly Assembly => this.GetType().Assembly;
    }
}