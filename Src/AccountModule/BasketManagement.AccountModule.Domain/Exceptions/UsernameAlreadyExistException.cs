using BasketManagement.AccountModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.AccountModule.Domain.Exceptions
{
    public class UsernameAlreadyExistException : ConflictException
    {
        public UsernameAlreadyExistException(Username username) : base($"'{username}' already exist")
        {
        }
    }
}