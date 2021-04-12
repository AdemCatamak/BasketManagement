using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.AccountModule.Domain.Exceptions
{
    public class AccountIdEmptyException : ValidationException
    {
        public AccountIdEmptyException() : base("Account id should not be empty")
        {
        }
    }
}