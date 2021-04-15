using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BasketManagement.BasketModule.Domain;
using BasketManagement.BasketModule.Domain.Repositories;
using BasketManagement.BasketModule.Domain.Specifications;
using BasketManagement.StockModule.Domain;
using BasketManagement.StockModule.Domain.Repositories;
using BasketManagement.StockModule.Domain.Specifications.StockSpecifications;
using BasketManagement.WebApi.FunctionalTest.Extensions;
using BasketManagement.WebApi.FunctionalTest.Fixtures;
using BasketManagement.WebApi.Modules.BasketModule.Controllers.HttpValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BasketManagement.WebApi.FunctionalTest.BasketTests
{
    [Collection(WebApiInfraFixture.WebApiInfraFixtureKey)]
    public class PutItemIntoBasketTests : IClassFixture<BasketFixture>,
        IDisposable
    {
        private readonly BasketFixture _basketFixture;
        private readonly HttpClient _httpClient;
        private readonly IServiceProvider _serviceProvider;

        public PutItemIntoBasketTests(WebApiInfraFixture webApiInfraFixture, BasketFixture basketFixture)
        {
            _basketFixture = basketFixture;
            _httpClient = webApiInfraFixture.WebApiInfraMockInstance.CreateHttpClient();
            _serviceProvider = webApiInfraFixture.WebApiInfraMockInstance.ServiceProvider;
        }

        [Fact]
        public async Task PutItemIntoBasket__WhenProductNotExist__ResponseStatusCodeBe404()
        {
            using HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, $"accounts/{_basketFixture.AccountId}/baskets/{_basketFixture.BasketId}")
                .WithJsonBody(new BasketItemHttpModel
                {
                    ProductId = "random-product-id",
                    Quantity = 3
                });
            // Act
            using var response = await _httpClient.SendAsync(httpRequestMessage);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PutItemIntoBasket__WhenRequestQuantityIsBiggerThanAvailableStock__ResponseStatusCodeBe400()
        {
            // Arrange
            Stock initialStock;
            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var stockDbContext = serviceScope.ServiceProvider.GetRequiredService<IStockDbContext>();
                var stockRepository = stockDbContext.StockRepository;
                initialStock = await stockRepository.GetFirstAsync(new StockCountLessThan(10), CancellationToken.None);
            }

            using HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, $"accounts/{_basketFixture.AccountId}/baskets/{_basketFixture.BasketId}")
                .WithJsonBody(new BasketItemHttpModel
                {
                    ProductId = initialStock.ProductId,
                    Quantity = 11
                });
            // Act
            using var response = await _httpClient.SendAsync(httpRequestMessage);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PutItemIntoBasket__WhenRequestIsValid__ResponseStatusCodeBe200()
        {
            // Arrange
            Stock initialStock;
            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var stockDbContext = serviceScope.ServiceProvider.GetRequiredService<IStockDbContext>();
                var stockRepository = stockDbContext.StockRepository;
                initialStock = await stockRepository.GetFirstAsync(new StockCountGreaterThan(5), CancellationToken.None);
            }

            int initialStockCount = initialStock.AvailableStock;

            using HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Put, $"accounts/{_basketFixture.AccountId}/baskets/{_basketFixture.BasketId}")
                .WithJsonBody(new BasketItemHttpModel
                {
                    ProductId = initialStock.ProductId,
                    Quantity = 3
                });
            // Act
            using var response = await _httpClient.SendAsync(httpRequestMessage);
            await TestHelper.WaitForAsyncProcessAsync();

            Stock updatedStock;
            Basket basket;
            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var basketDbContext = serviceScope.ServiceProvider.GetRequiredService<IBasketDbContext>();
                var basketRepository = basketDbContext.BasketRepository;
                basket = await basketRepository.GetFirstAsync(new BasketIdIs((_basketFixture.BasketId)), CancellationToken.None);

                var stockDbContext = serviceScope.ServiceProvider.GetRequiredService<IStockDbContext>();
                var stockRepository = stockDbContext.StockRepository;
                updatedStock = await stockRepository.GetFirstAsync(new ProductIdIs(initialStock.ProductId), CancellationToken.None);
            }

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(basket.BasketLines);
            Assert.Contains(basket.BasketLines, line => line.BasketItem.ProductId == initialStock.ProductId);
            Assert.Equal(initialStockCount, updatedStock.AvailableStock + 3);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}