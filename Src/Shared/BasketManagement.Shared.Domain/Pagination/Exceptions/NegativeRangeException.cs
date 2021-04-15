using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.Shared.Domain.Pagination.Exceptions
{
    public class NegativeLimitException : ValidationException
    {
        public NegativeLimitException(int limit)
            : base($"Negative limit is not acceptable [{limit}]")
        {
        }
    }
}