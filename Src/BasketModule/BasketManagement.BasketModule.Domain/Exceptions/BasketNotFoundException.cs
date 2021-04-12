using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.BasketModule.Domain.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException() : base("Order could not found")
        {
        }
    }
}