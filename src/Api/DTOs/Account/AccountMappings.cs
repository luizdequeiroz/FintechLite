namespace Api.DTOs.Account
{
    public static class AccountMappings
    {
        public static AccountResponse ToResponse(this Domain.Account account) => new()
        {
            Id = account.Id,
            Name = account.Name,
            BalanceCents = account.BalanceCents,
            CreatedAtUtc = account.CreatedAtUtc
        };
    }
}
