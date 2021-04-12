using BasketManagement.AccountModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.AccountModule.Domain.Exceptions
{
    public class PasswordNotValidException : ValidationException
    {
        public PasswordNotValidException(Password password, string errorMessage) : base($"'{password}' is not valid. {errorMessage}")
        {
        }
    }
}