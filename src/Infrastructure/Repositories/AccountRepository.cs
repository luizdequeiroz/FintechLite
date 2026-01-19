using Domain;
using Microsoft.EntityFrameworkCore;
using Services.RepositoryInterfaces;

namespace Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FintechLiteDbContext fintechLiteDbContext;

        public AccountRepository(FintechLiteDbContext fintechLiteDbContext)
        {
            this.fintechLiteDbContext = fintechLiteDbContext;
        }

        public async Task AddAsync(Account account, CancellationToken cancellationToken = default)
        {
            await fintechLiteDbContext.Accounts.AddAsync(account, cancellationToken);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return fintechLiteDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<Account?> SelectAccountByIdAsync(Guid accountId, CancellationToken cancellationToken = default)
        {
           return await fintechLiteDbContext.Accounts
                .FirstOrDefaultAsync(a => a.Id == accountId, cancellationToken);
        }

        public async Task<IList<Account>> SelectAllAccountsAsync(CancellationToken cancellationToken = default)
        {
            return await fintechLiteDbContext.Accounts.ToListAsync(cancellationToken);
        }
    }
}
