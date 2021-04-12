using System.Reflection;

namespace BasketManagement.Shared.Infrastructure.Db
{
    public interface IEntityTypeConfigurationAssembly
    {
        public Assembly Assembly { get; }
    }
}