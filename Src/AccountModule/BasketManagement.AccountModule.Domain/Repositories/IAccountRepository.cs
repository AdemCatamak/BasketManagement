using System.Threading;
using System.Threading.Tasks;
using BasketManagement.AccountModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.Pagination;

namespace BasketManagement.AccountModule.Domain.Repositories
{
    public interface IAccountRepository
    {
        Task AddAsync(Account account, CancellationToken cancellationToken);
        Task<Account> GetByUsernameAsync(Username username, CancellationToken cancellationToken);
        Task<Account> GetByAccountIdAsync(AccountId accountId, CancellationToken cancellationToken);
        Task<PaginatedCollection<Account>> GetAsync(int offset, int limit, CancellationToken cancellationToken);
    }
}