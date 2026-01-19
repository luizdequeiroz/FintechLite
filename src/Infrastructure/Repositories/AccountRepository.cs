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

        public async Task AddAsync(Account account)
        {
            await fintechLiteDbContext.Accounts.AddAsync(account);
        }

        public Task<int> SaveChangesAsync()
        {
            return fintechLiteDbContext.SaveChangesAsync();
        }

        public async Task<Account?> SelectAccountByIdAsync(Guid accountId)
        {
           return await fintechLiteDbContext.Accounts
                .FirstOrDefaultAsync(a => a.Id == accountId);
        }

        public async Task<IList<Account>> SelectAllAccountsAsync()
        {
            return await fintechLiteDbContext.Accounts.ToListAsync();
        }
    }
}
