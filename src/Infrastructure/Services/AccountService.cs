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

        public async Task<IList<Account>> GetAllAccountsAsync(CancellationToken cancellationToken = default)
        {
            return await accountRepository.SelectAllAccountsAsync(cancellationToken);
        }

        public async Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await accountRepository.SelectAccountByIdAsync(id, cancellationToken);
        }

        public async Task<Account> CreateAsync(string name, CancellationToken cancellationToken = default)
        {
            var account = new Account
            {
                Name = name,
                BalanceCents = 0
            };
            await accountRepository.AddAsync(account, cancellationToken);
            await accountRepository.SaveChangesAsync(cancellationToken);
            return account;
        }
    }
}
