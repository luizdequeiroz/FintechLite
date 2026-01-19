using Domain;

namespace Services.RepositoryInterfaces
{
    public interface IAccountRepository
    {
        Task<IList<Account>> SelectAllAccountsAsync(CancellationToken cancellationToken);
        Task<Account?> SelectAccountByIdAsync(Guid accountId, CancellationToken cancellationToken);
        Task AddAsync(Account account, CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
