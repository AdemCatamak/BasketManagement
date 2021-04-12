using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.OrderModule.Domain.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException() : base("Order could not found")
        {
        }
    }
}