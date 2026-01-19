using Domain;

namespace Services
{
    public interface IAccountService
    {
        Task<IList<Account>> GetAllAccountsAsync(CancellationToken cancellationToken);
        Task<Account> CreateAsync(string name, CancellationToken cancellationToken);
        Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
