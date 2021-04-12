using System.Threading;
using System.Threading.Tasks;
using BasketManagement.AccountModule.Domain.ValueObjects;

namespace BasketManagement.AccountModule.Domain.Rules
{
    public interface IUsernameUniqueChecker
    {
        Task<bool> CheckAsync(Username username, CancellationToken cancellationToken = default);
    }
}