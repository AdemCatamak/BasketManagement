using BasketManagement.AccountModule.Domain.ValueObjects;

namespace BasketManagement.AccountModule.Domain.Services
{
    public interface IAccessTokenGenerator
    {
        AccessToken Generate(AccountId accountId, Roles role);
    }
}