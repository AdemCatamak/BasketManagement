using System.Collections.Generic;
using BasketManagement.BasketModule.Domain;
using BasketManagement.WebApi.Modules.BasketModule.Controllers.HttpValueObjects;

namespace BasketManagement.WebApi.Modules.BasketModule.Controllers.Responses
{
    public class BasketHttpResponse
    {
        public string BasketId { get; set; } = string.Empty;
        public BasketStatuses BasketStatus { get; set; }
        public List<BasketItemHttpModel> BasketItemHttpModels { get; set; } = new List<BasketItemHttpModel>();
    }
}