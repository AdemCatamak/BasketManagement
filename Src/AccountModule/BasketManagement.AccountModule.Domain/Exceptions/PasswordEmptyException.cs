using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.AccountModule.Domain.Exceptions
{
    public class PasswordEmptyException : ValidationException
    {
        public PasswordEmptyException() : base("Account password should not be empty")
        {
        }
    }
}