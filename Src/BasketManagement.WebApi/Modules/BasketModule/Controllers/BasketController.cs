using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using BasketManagement.BasketModule.Application.Commands;
using BasketManagement.BasketModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.Pagination;
using BasketManagement.Shared.Infrastructure;
using BasketManagement.WebApi.Modules.BasketModule.Controllers.HttpValueObjects;
using BasketManagement.WebApi.Modules.BasketModule.Controllers.Requests;
using BasketManagement.WebApi.Modules.BasketModule.Controllers.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BasketManagement.WebApi.Modules.BasketModule.Controllers
{
    public class BasketController : ControllerBase
    {
        private readonly IExecutionContext _executionContext;

        public BasketController(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        [HttpPost("accounts/{accountId}/baskets")]
        [ProducesResponseType(typeof(Guid), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> PostBasket([FromRoute] string accountId)
        {
            CreateBasketCommand createBasketCommand = new CreateBasketCommand(accountId);
            BasketId basketId = await _executionContext.ExecuteAsync(createBasketCommand, CancellationToken.None);

            return StatusCode((int) HttpStatusCode.Created, basketId.Value);
        }

        [HttpGet("accounts/{accountId}/baskets")]
        [ProducesResponseType(typeof(Guid), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetBaskets([FromRoute] string accountId, [FromQuery] GetBasketHttpRequest? getBasketHttpRequest)
        {
            var queryBasketCommand = new QueryBasketCommand(accountId)
            {
                BasketId = getBasketHttpRequest?.BasketId == null ? null : new BasketId(getBasketHttpRequest.BasketId.Value),
                Offset = getBasketHttpRequest?.Offset ?? 0,
                Limit = getBasketHttpRequest?.Limit ?? 10
            };

            PaginatedCollection<BasketResponse> paginatedCollection = await _executionContext.ExecuteAsync(queryBasketCommand, CancellationToken.None);
            var result = new PaginatedCollection<BasketHttpResponse>(paginatedCollection.TotalCount,
                paginatedCollection.Data
                    .Select(orderResponse => new BasketHttpResponse
                    {
                        BasketId = orderResponse.BasketId.Value.ToString(),
                        BasketItemHttpModels = orderResponse.OrderItems.Select(item => new BasketItemHttpModel
                            {
                                ProductId = item.ProductId,
                                Quantity = item.Quantity
                            })
                            .ToList()
                    }));

            return StatusCode((int) HttpStatusCode.OK, result);
        }

        [HttpPut("accounts/{accountId}/baskets/{basketId}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> PutItemIntoBasket([FromRoute] string accountId, [FromRoute] Guid basketId, [FromBody] BasketItemHttpModel basketItemHttpModel)
        {
            BasketItem basketItem = new BasketItem(basketItemHttpModel?.ProductId ?? string.Empty, basketItemHttpModel?.Quantity ?? 0);
            PutItemIntoBasketCommand putItemIntoBasketCommand = new PutItemIntoBasketCommand(accountId, new BasketId(basketId), basketItem);

            await _executionContext.ExecuteAsync(putItemIntoBasketCommand, CancellationToken.None);

            return StatusCode((int) HttpStatusCode.OK);
        }

        [HttpDelete("accounts/{accountId}/baskets/{basketId}")]
        [ProducesResponseType(typeof(Guid), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> DeleteBasket([FromRoute] string accountId, [FromRoute] Guid basketId)
        {
            var deleteBasketCommand = new DeleteBasketCommand(accountId, new BasketId(basketId));

            await _executionContext.ExecuteAsync(deleteBasketCommand, CancellationToken.None);

            return StatusCode((int) HttpStatusCode.OK);
        }

        [HttpDelete("accounts/{accountId}/baskets/{basketId}/basket-item/{productId}")]
        [ProducesResponseType(typeof(Guid), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> DeleteBasketLine([FromRoute] string accountId, [FromRoute] Guid basketId, [FromRoute] string productId)
        {
            var removeItemFromBasket = new RemoveItemFromBasket(accountId, new BasketId(basketId), productId);

            await _executionContext.ExecuteAsync(removeItemFromBasket, CancellationToken.None);

            return StatusCode((int) HttpStatusCode.OK);
        }
    }
}