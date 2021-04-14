using System;
using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.BasketModule.Domain.Exceptions
{
    public class ProductIdEmptyException : ValidationException
    {
        public ProductIdEmptyException()
            : this("Product identifier should not be empty")
        {
        }

        public ProductIdEmptyException(string message, Exception? innerException = null) : base(message, innerException)
        {
        }
    }
}