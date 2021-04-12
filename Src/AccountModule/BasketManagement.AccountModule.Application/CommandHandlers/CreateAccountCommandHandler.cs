using System.Threading;
using System.Threading.Tasks;
using BasketManagement.AccountModule.Application.Commands;
using BasketManagement.AccountModule.Domain;
using BasketManagement.AccountModule.Domain.Repositories;
using BasketManagement.AccountModule.Domain.Rules;
using BasketManagement.AccountModule.Domain.Services;
using BasketManagement.AccountModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.AccountModule.Application.CommandHandlers
{
    public class CreateAccountCommandHandler : IDomainCommandHandler<CreateAccountCommand, AccountId>
    {
        private readonly IAccountDbContext _dbContext;
        private readonly IUsernameUniqueChecker _usernameUniqueChecker;
        private readonly IPasswordHasher _passwordHasher;

        public CreateAccountCommandHandler(IAccountDbContext dbContext, IUsernameUniqueChecker usernameUniqueChecker, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _usernameUniqueChecker = usernameUniqueChecker;
            _passwordHasher = passwordHasher;
        }

        public async Task<AccountId> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            Account account = Account.Create(request.Username, request.Password, request.Name, request.Role, _usernameUniqueChecker, _passwordHasher, cancellationToken);

            IAccountRepository accountRepository = _dbContext.AccountRepository;
            await accountRepository.AddAsync(account, cancellationToken);

            return account.Id;
        }
    }
}