using System.Reflection;
using BasketManagement.Shared.Infrastructure.Db;

namespace BasketManagement.StockModule.Infrastructure.Db.EntityConfigurations
{
    public class StockModuleEntityConfigurationAssembly : IEntityTypeConfigurationAssembly
    {
        public Assembly Assembly => typeof(StockEntityConfiguration).Assembly;
    }
}