using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.AccountModule.Domain.Exceptions
{
    public class FirstNameEmptyException : ValidationException
    {
        public FirstNameEmptyException() : base("First name should not be empty")
        {
        }
    }
}