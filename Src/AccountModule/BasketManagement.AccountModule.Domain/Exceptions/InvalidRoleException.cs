using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.AccountModule.Domain.Exceptions
{
    public class InvalidRoleException : ValidationException
    {
        public InvalidRoleException(Roles role) : base($"'{role}' is not defined")
        {
        }
    }
}