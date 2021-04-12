using System.Threading;
using System.Threading.Tasks;
using BasketManagement.AccountModule.Domain.Exceptions;
using BasketManagement.AccountModule.Domain.Repositories;
using BasketManagement.AccountModule.Domain.Rules;
using BasketManagement.AccountModule.Domain.ValueObjects;

namespace BasketManagement.AccountModule.Application.Rules
{
    public class UsernameUniqueChecker : IUsernameUniqueChecker
    {
        private readonly IAccountDbContext _accountDbContext;

        public UsernameUniqueChecker(IAccountDbContext accountDbContext)
        {
            _accountDbContext = accountDbContext;
        }

        public async Task<bool> CheckAsync(Username username, CancellationToken cancellationToken = default)
        {
            var result = false;
            try
            {
                await _accountDbContext.AccountRepository.GetByUsernameAsync(username, cancellationToken);
            }
            catch (AccountNotFoundException)
            {
                result = true;
            }

            return result;
        }
    }
}