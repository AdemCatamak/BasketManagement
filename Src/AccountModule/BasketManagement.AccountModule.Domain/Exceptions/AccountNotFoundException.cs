using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.AccountModule.Domain.Exceptions
{
    public class AccountNotFoundException : NotFoundException
    {
        public AccountNotFoundException() : base($"Account could not found")
        {
        }
    }
}