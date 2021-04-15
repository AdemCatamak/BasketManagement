using System.Threading;
using System.Threading.Tasks;

namespace BasketManagement.Shared.Domain.Outbox
{
    public interface IOutboxClient
    {
        Task AddAsync<T>(T obj, CancellationToken cancellationToken);
    }
}