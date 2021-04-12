using System.Reflection;

namespace BasketManagement.Shared.Infrastructure.MassTransitComponents
{
    public interface IIntegrationMessageConsumerAssembly
    {
        public Assembly Assembly { get; }
    }
}