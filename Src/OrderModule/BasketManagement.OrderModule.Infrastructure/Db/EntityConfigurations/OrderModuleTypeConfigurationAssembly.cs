using System.Reflection;
using BasketManagement.Shared.Infrastructure.Db;

namespace BasketManagement.OrderModule.Infrastructure.Db.EntityConfigurations
{
    public class OrderModuleTypeConfigurationAssembly : IEntityTypeConfigurationAssembly
    {
        public Assembly Assembly => this.GetType().Assembly;
    }
}