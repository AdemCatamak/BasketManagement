using BasketManagement.AccountModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.AccountModule.Domain.Exceptions
{
    public class UsernameNotValidException : ValidationException
    {
        public UsernameNotValidException(Username username, string errorMessage)
            : base($"'{username}' is not valid. {errorMessage}")
        {
        }
    }
}