using System.Threading;
using System.Threading.Tasks;
using BasketManagement.AccountModule.Application.Commands;
using BasketManagement.AccountModule.Domain.Repositories;
using BasketManagement.AccountModule.Domain.Services;
using BasketManagement.AccountModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.AccountModule.Application.CommandHandlers
{
    public class CreateAccessTokenCommandHandler : IDomainCommandHandler<CreateAccessTokenCommand, AccessToken>
    {
        private readonly IAccountDbContext _accountDbContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAccessTokenGenerator _accessTokenGenerator;

        public CreateAccessTokenCommandHandler(IAccountDbContext accountDbContext, IPasswordHasher passwordHasher, IAccessTokenGenerator accessTokenGenerator)
        {
            _accountDbContext = accountDbContext;
            _passwordHasher = passwordHasher;
            _accessTokenGenerator = accessTokenGenerator;
        }

        public async Task<AccessToken> Handle(CreateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var accountRepository = _accountDbContext.AccountRepository;
            var account = await accountRepository.GetByUsernameAsync(request.Username, cancellationToken);
            
            AccessToken accessToken = account.CreateAccessToken(request.Password, _passwordHasher, _accessTokenGenerator);
            return accessToken;
        }
    }
}