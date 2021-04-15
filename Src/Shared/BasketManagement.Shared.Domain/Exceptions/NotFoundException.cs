using System;

namespace BasketManagement.Shared.Domain.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        public NotFoundException(string message, Exception? innerException = null)
            : base(message, innerException)
        {
        }
    }
}