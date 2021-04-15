using System.Collections.Generic;

namespace BasketManagement.Shared.Domain.Pagination
{
    public class PaginatedCollection<T>
    {
        public int TotalCount { get; }
        public IEnumerable<T> Data { get; }

        public PaginatedCollection(int totalCount, IEnumerable<T> data)
        {
            TotalCount = totalCount;
            Data = data;
        }
    }
}