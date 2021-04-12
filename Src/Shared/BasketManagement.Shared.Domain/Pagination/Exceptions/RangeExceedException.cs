using BasketManagement.Shared.Domain.Exceptions;

namespace BasketManagement.Shared.Domain.Pagination.Exceptions
{
    public class RangeExceedException : ValidationException
    {
        public RangeExceedException(int maxLimit, int limit)
            : base($"Limit value which is {limit} should be max {maxLimit}")
        {
        }
    }
}