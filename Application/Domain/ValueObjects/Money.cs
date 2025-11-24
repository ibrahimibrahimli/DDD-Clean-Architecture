namespace Domain.ValueObjects
{
    public sealed class Money : IEquatable<Money>
    {
        public decimal Amount { get; }
        public string Currency { get; }

        private Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Money Create(decimal amount, string currency = "USD")
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("Amount can not be negative", nameof(amount));

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency can not be empty", nameof(currency));

            return new Money(amount, currency);
        }
        public bool Equals(Money? other)
        {
            if(other is null)
                return false;

            return Amount == other.Amount && Currency == other.Currency;
        }

        public override bool Equals(object? obj) => obj is Money money && Equals(money);

        public override int GetHashCode() => HashCode.Combine(Amount, Currency);

        public static bool operator ==(Money? left, Money? right) =>
        left?.Equals(right) ?? right is null;

        public static bool operator !=(Money? left, Money? right) => !(left == right);
    }
}
