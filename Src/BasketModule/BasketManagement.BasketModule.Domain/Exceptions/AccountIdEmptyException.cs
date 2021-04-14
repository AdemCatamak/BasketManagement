using System;
using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.BasketModule.Domain.Exceptions
{
    public class AccountIdEmptyException : ValidationException
    {
        public AccountIdEmptyException() : this("AccountId should not be empty", null)
        {
        }

        public AccountIdEmptyException(string message, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}