using BasketManagement.AccountModule.Domain.ValueObjects;
using BasketManagement.Shared.Domain.DomainMessageBroker;
using BasketManagement.Shared.Domain.Pagination;

namespace BasketManagement.AccountModule.Application.Commands
{
    public class QueryAccountCommand : PaginatedRequest,
                                       IDomainCommand<PaginatedCollection<AccountResponse>>
    {
        public AccountId? AccountId { get; set; }
    }

    public class AccountResponse
    {
        public AccountId AccountId { get; private set; }
        public Username Username { get; private set; }
        public Name Name { get; private set; }

        public AccountResponse(AccountId accountId, Username username, Name name)
        {
            AccountId = accountId;
            Username = username;
            Name = name;
        }
    }
}