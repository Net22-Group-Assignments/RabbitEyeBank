using RabbitEyeBankLibrary.Services;

namespace RabbitEyeBankLibrary.Money
{
    /// <summary>
    /// Represents a currency. Stores it's type as an enum and it's symbol as a string.
    /// </summary>
    public class Currency
    {
        public CurrencyISO CurrencyISO { get; internal set; }
        public string Symbol { get; internal set; }

        public decimal DollarValue { get; internal set; }

        public Currency(CurrencyISO currencyISO, string symbol, decimal dollarValue)
        {
            CurrencyISO = currencyISO;
            Symbol = symbol;
            DollarValue = dollarValue;
        }

        public bool Equals(Currency other)
        {
            return CurrencyISO == other.CurrencyISO;
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is Currency other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return CurrencyISO.GetHashCode();
        }

        public static bool operator ==(Currency left, Currency right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Currency left, Currency right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Symbol} ({CurrencyISO})";
        }
    }
}
