using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.AccountModule.Domain.Exceptions
{
    public class LastNameEmptyException : ValidationException
    {
        public LastNameEmptyException() : base("Last name should not be empty")
        {
        }
    }
}