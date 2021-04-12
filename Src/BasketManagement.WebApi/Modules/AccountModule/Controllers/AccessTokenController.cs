using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BasketManagement.AccountModule.Application.Commands;
using BasketManagement.AccountModule.Domain.ValueObjects;
using BasketManagement.Shared.Infrastructure;
using BasketManagement.WebApi.Modules.AccountModule.Controllers.Mappings;
using BasketManagement.WebApi.Modules.AccountModule.Controllers.Requests;
using BasketManagement.WebApi.Modules.AccountModule.Controllers.Responses;

namespace BasketManagement.WebApi.Modules.AccountModule.Controllers
{
    [Route("access-tokens")]
    [ApiController]
    public class AccessTokenController : ControllerBase
    {
        private readonly IExecutionContext _executionContext;

        public AccessTokenController(IExecutionContext executionContext)
        {
            _executionContext = executionContext;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AccessTokenHttpResponse), (int) HttpStatusCode.Created)]
        public async Task<IActionResult> PostAccessToken([FromBody] PostAccessTokenHttpRequest? postAccessTokenHttpRequest)
        {
            var createAccessTokenCommand = new CreateAccessTokenCommand(new Username(postAccessTokenHttpRequest?.Username ?? string.Empty),
                                                                        new Password(postAccessTokenHttpRequest?.Password ?? string.Empty));
            AccessToken accessToken = await _executionContext.ExecuteAsync(createAccessTokenCommand, CancellationToken.None);
            AccessTokenHttpResponse accessTokenHttpResponse = accessToken.ToAccessTokenHttpResponse();
            return StatusCode((int) HttpStatusCode.Created, accessTokenHttpResponse);
        }
    }
}