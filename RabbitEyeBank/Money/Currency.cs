using System.Collections.Generic;
using System.Globalization;

namespace RabbitEyeBank.Money
{
    /// <summary>
    /// Represents a currency. Stores it's type as an enum and it's symbol as a string.
    /// </summary>
    public readonly struct Currency
    {
        public CurrencyISO CurrencyISO { get; }
        public string Symbol { get; }

        public Currency(CurrencyISO currencyISO, string symbol)
        {
            CurrencyISO = currencyISO;
            Symbol = symbol;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Symbol} ({CurrencyISO})";
        }
    }
}
