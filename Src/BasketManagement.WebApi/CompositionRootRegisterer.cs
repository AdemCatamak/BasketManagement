using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BasketManagement.Shared.Infrastructure;

namespace BasketManagement.WebApi
{
    public class CompositionRootRegisterer
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly IConfiguration _configuration;

        public CompositionRootRegisterer(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            _serviceCollection = serviceCollection;
            _configuration = configuration;
        }

        public CompositionRootRegisterer Registerer(ICompositionRoot compositionRoot)
        {
            compositionRoot.Register(_serviceCollection, _configuration);
            return this;
        }
    }
}