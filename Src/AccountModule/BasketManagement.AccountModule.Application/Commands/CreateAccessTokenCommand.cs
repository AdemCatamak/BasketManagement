using BasketManagement.AccountModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;

namespace BasketManagement.AccountModule.Application.Commands
{
    public class CreateAccessTokenCommand : IDomainCommand<AccessToken>
    {
        public Username Username { get; }
        public Password Password { get; }

        public CreateAccessTokenCommand(Username username, Password password)
        {
            Username = username;
            Password = password;
        }
    }
}