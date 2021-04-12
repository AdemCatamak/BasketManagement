using System.Collections.Generic;
using BasketManagement.WebApi.Modules.OrderModule.Controllers.HttpValueObjects;

namespace BasketManagement.WebApi.Modules.OrderModule.Controllers.Requests
{
    public class PostOrderHttpRequest
    {
        public List<OrderItemHttpModel> OrderItemHttpModels { get; set; } = new List<OrderItemHttpModel>();
    }
}