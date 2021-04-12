using BasketManagement.AccountModule.Domain.ValueObjects;

namespace BasketManagement.AccountModule.Domain.Services
{
    public interface IPasswordHasher
    {
        PasswordHash Hash(Password password);
        PasswordHash Hash(Password password, string salt);
    }
}