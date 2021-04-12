using System.Reflection;
using BasketManagement.Shared.Infrastructure.Db;

namespace BasketManagement.ProductModule.Infrastructure.Db.EntityConfigurations
{
    public class ProductModuleTypeConfigurationAssembly : IEntityTypeConfigurationAssembly
    {
        public Assembly Assembly => this.GetType().Assembly;
    }
}