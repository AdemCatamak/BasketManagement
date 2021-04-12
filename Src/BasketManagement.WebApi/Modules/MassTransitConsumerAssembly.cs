using System.Reflection;
using BasketManagement.Shared.Infrastructure.MassTransitComponents;

namespace BasketManagement.WebApi.Modules
{
    public class MassTransitConsumerAssembly : IIntegrationMessageConsumerAssembly
    {
        public Assembly Assembly => this.GetType().Assembly;
    }
}