using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BasketManagement.Shared.Domain.Pagination;
using BasketManagement.Shared.Infrastructure;
using BasketManagement.StockModule.Application.Commands;
using BasketManagement.WebApi.Modules.StockModule.Controllers.Requests;

namespace BasketManagement.WebApi.Modules.StockModule.Controllers
{
    [Route("stocks")]
    public class StockController : ControllerBase
    {
        private readonly IExecutionContext _executionContext;

        public StockController(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetStocks([FromQuery] GetStockHttpRequest? getStockHttpRequest)
        {
            QueryStockCommand queryStockCommand = new QueryStockCommand
                                                  {
                                                      ProductId = getStockHttpRequest?.ProductId,
                                                      InStock = getStockHttpRequest?.InStock,
                                                      Offset = 0,
                                                      Limit = 10
                                                  };
            PaginatedCollection<StockResponse> paginatedCollection = await _executionContext.ExecuteAsync(queryStockCommand, CancellationToken.None);
            return StatusCode((int) HttpStatusCode.OK, paginatedCollection);
        }
    }
}