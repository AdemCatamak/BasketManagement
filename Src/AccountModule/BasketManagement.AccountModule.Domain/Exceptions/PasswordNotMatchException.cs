using BasketManagement.AccountModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.AccountModule.Domain.Exceptions
{
    public class PasswordNotMatchException : ValidationException
    {
        public PasswordNotMatchException(Username username, Password password) :
            base($"'{password}' does not match for '{username}'")
        {
        }
    }
}