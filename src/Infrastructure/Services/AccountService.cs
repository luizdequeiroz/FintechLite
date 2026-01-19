using Domain;
using Services;
using Services.RepositoryInterfaces;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task<IList<Account>> GetAllAccountsAsync()
        {
            return await accountRepository.SelectAllAccountsAsync();
        }

        public async Task<Account?> GetByIdAsync(Guid id)
        {
            return await accountRepository.SelectAccountByIdAsync(id);
        }

        public async Task<Account> CreateAsync(string name)
        {
            var account = new Account
            {
                Name = name,
                BalanceCents = 0
            };
            await accountRepository.AddAsync(account);
            await accountRepository.SaveChangesAsync();
            return account;
        }
    }
}
