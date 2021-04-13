using System;
using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.BasketModule.Domain.Exceptions
{
    public class NegativeQuantityException : ValidationException
    {
        public NegativeQuantityException() : this("Negative quantity is not allowed")
        {
        }

        public NegativeQuantityException(string message, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}