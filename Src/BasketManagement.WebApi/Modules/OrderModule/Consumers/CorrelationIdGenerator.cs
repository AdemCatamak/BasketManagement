using BasketManagement.OrderModule.Domain.ValueObjects;

namespace BasketManagement.WebApi.Modules.OrderModule.Consumers
{
    public static class CorrelationIdGenerator
    {
        public static string DecreaseFromStock(OrderId orderId, string productId)
        {
            return $"{orderId}__{productId}";
        }
    }
}