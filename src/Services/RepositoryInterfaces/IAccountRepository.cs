using Domain;

namespace Services.RepositoryInterfaces
{
    public interface IAccountRepository
    {
        Task<IList<Account>> SelectAllAccountsAsync();
        Task<Account?> SelectAccountByIdAsync(Guid accountId);
        Task AddAsync(Account account);
        Task<int> SaveChangesAsync();
    }
}
