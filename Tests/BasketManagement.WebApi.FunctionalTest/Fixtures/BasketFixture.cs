using System;
using System.Net.Http;
using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.WebApi.FunctionalTest.Extensions;

namespace BasketManagement.WebApi.FunctionalTest.Fixtures
{
    public class BasketFixture
    {
        public string AccountId { get; private set; }
        public BasketId BasketId { get; private set; }

        public BasketFixture(WebApiInfraFixture webApiInfraFixture)
        {
            AccountId = "account-id";
            using HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"accounts/{AccountId}/baskets");
            using var response = webApiInfraFixture.WebApiInfraMockInstance.CreateHttpClient().SendAsync(httpRequestMessage).GetAwaiter().GetResult();
            var basketId = response.Content.ReadAsAsync<Guid>().GetAwaiter().GetResult();
            BasketId = new BasketId(basketId);
        }
    }
}