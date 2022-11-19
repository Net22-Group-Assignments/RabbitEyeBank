using System.Collections.Generic;
using System.Globalization;

namespace RabbitEyeBank.Money
{
    /// <summary>
    /// Represents a currency. Stores it's type as an enum and it's symbol as a string.
    /// </summary>
    public readonly struct Currency
    {
        private static readonly string[] cultureNames = { "en-US", "FR-fr", "th-TH", "sv-SE" };

        // TODO Should the dictionary be stored somewhere else, like BankService?
        public static readonly Dictionary<CurrencyISO, Currency> CurrencyDictionary;

        /// <summary>
        /// Initializes the dictionary by connecting the currency type
        /// to the currency symbol in the CultureInfo class.
        /// </summary>
        static Currency()
        {
            var currencyISOAndCurrency = Enum.GetValues<CurrencyISO>()
                .Zip(
                    cultureNames,
                    (first, second) =>
                        new KeyValuePair<CurrencyISO, Currency>(
                            first,
                            new Currency(
                                first,
                                CultureInfo.GetCultureInfo(second).NumberFormat.CurrencySymbol
                            )
                        )
                );
            CurrencyDictionary = new Dictionary<CurrencyISO, Currency>(currencyISOAndCurrency);

            // Correct to the _real_ SEK symbol:
            CurrencyDictionary[CurrencyISO.SEK] = new Currency(CurrencyISO.SEK, "♕");
        }

        public CurrencyISO CurrencyISO { get; }
        public string Symbol { get; }

        public Currency(CurrencyISO currencyISO, string symbol)
        {
            CurrencyISO = currencyISO;
            Symbol = symbol;
            //Hundtext
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(CurrencyISO)}: {CurrencyISO}, {nameof(Symbol)}: {Symbol}";
        }
    }

    public enum CurrencyISO
    {
        USD,
        EUR,
        THB,
        SEK
    }
}
