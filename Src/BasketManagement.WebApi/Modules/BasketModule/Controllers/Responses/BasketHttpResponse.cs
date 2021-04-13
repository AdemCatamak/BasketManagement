using System.Collections.Generic;
using BasketManagement.WebApi.Modules.BasketModule.Controllers.HttpValueObjects;

namespace BasketManagement.WebApi.Modules.BasketModule.Controllers.Responses
{
    public class BasketHttpResponse
    {
        public string BasketId { get; set; } = string.Empty;
        public List<BasketItemHttpModel> BasketItemHttpModels { get; set; } = new List<BasketItemHttpModel>();
    }
}