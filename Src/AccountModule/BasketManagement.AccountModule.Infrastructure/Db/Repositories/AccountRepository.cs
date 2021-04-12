using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BasketManagement.AccountModule.Domain;
using BasketManagement.AccountModule.Domain.Exceptions;
using BasketManagement.AccountModule.Domain.Repositories;
using BasketManagement.AccountModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.Pagination;
using BasketManagement.Shared.Infrastructure.Db;

namespace BasketManagement.AccountModule.Infrastructure.Db.Repositories
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly EfAppDbContext _appDbContext;

        public AccountRepository(EfAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Account account, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(account, cancellationToken);
        }

        public async Task<Account> GetByUsernameAsync(Username username, CancellationToken cancellationToken)
        {
            Account? account = await _appDbContext.Set<Account>()
                                                  .Where(acc => acc.Username.Value == username.Value)
                                                  .FirstOrDefaultAsync(cancellationToken);

            if (account == null) throw new AccountNotFoundException();
            return account;
        }

        public async Task<Account> GetByAccountIdAsync(AccountId accountId, CancellationToken cancellationToken)
        {
            Account? account = await _appDbContext.Set<Account>()
                                                  .Where(acc => Equals(acc.Id, accountId))
                                                  .FirstOrDefaultAsync(cancellationToken);

            if (account == null) throw new AccountNotFoundException();
            return account;
        }

        public async Task<PaginatedCollection<Account>> GetAsync(int offset, int limit, CancellationToken cancellationToken)
        {
            (int totalItemCount, List<Account> accountList) = await _appDbContext.Set<Account>()
                                                                                 .PaginatedQueryAsync(offset, limit, cancellationToken);

            if (!accountList.Any()) throw new AccountNotFoundException();

            return new PaginatedCollection<Account>(totalItemCount, accountList);
        }
    }
}