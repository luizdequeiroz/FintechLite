namespace Api.DTOs.Account
{
    public sealed class AccountResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public long BalanceCents { get; init; }
        public DateTimeOffset CreatedAtUtc { get; init; }
    }
}
