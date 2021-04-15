using System;
using Xunit;

namespace BasketManagement.WebApi.FunctionalTest.Fixtures
{
    [CollectionDefinition(WebApiInfraFixture.WebApiInfraFixtureKey)]
    public class WebApiInfraFixtureDefinition : ICollectionFixture<WebApiInfraFixture>
    {
    }


    public class WebApiInfraFixture : IDisposable
    {
        public const string WebApiInfraFixtureKey = "WEB_API_INFRA_FIXTURE";

        public WebApiInfraMock WebApiInfraMockInstance { get; }

        public WebApiInfraFixture()
        {
            WebApiInfraMockInstance = WebApiInfraMockSingleton.Instance;
        }


        public void Dispose()
        {
            WebApiInfraMockInstance.Dispose();
        }
    }
}