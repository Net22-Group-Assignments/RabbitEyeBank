namespace RabbitEyeBankLibrary.Money
{
    /// <summary>
    /// Represents a currency. Stores it's type as an enum and it's symbol as a string.
    /// </summary>
    public struct Currency
    {
        public CurrencyISO CurrencyISO { get; internal set; }
        public string Symbol { get; internal set; }

        public decimal DollarValue { get; internal set; }

        public Currency(CurrencyISO currencyISO, string symbol, decimal dollarValue = 1)
        {
            CurrencyISO = currencyISO;
            Symbol = symbol;
            DollarValue = dollarValue;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Symbol} ({CurrencyISO})";
        }
    }
}
