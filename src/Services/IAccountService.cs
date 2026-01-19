using Domain;

namespace Services
{
    public interface IAccountService
    {
        Task<IList<Account>> GetAllAccountsAsync();
        Task<Account> CreateAsync(string name);
    }
}
