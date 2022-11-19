using System.Collections.Generic;
using System.Globalization;

namespace RabbitEyeBank.Money
{
    /// <summary>
    /// Represents a currency. Stores it's type as an enum and it's symbol as a string.
    /// </summary>
    public readonly struct Currency
    {
        private static readonly string[] CultureName = { "en-US", "FR-fr", "th-TH", "sv-SE" };

        // TODO Should the dictionary be stored somewhere else, like BankService?
        public static Dictionary<CurrencyType, string> CurrencySymbolDictionary;

        /// <summary>
        /// Initializes the dictionary by connecting the currency type
        /// to the currency symbol in the CultureInfo class.
        /// </summary>
        static Currency()
        {
            var currencyTypesAndCultures = Enum.GetValues<CurrencyType>()
                .Zip(
                    CultureName,
                    (first, second) =>
                        new KeyValuePair<CurrencyType, string>(
                            first,
                            CultureInfo.GetCultureInfo(second).NumberFormat.CurrencySymbol
                        )
                );
            CurrencySymbolDictionary = new Dictionary<CurrencyType, string>(
                currencyTypesAndCultures
            );

            // Correct to the _real_ SEK symbol:
            CurrencySymbolDictionary[CurrencyType.SEK] = "♕";
        }

        public CurrencyType CurrencyType { get; }
        public string Symbol { get; }

        public Currency(CurrencyType currencyType, string symbol)
        {
            CurrencyType = currencyType;
            Symbol = symbol;
            //Hundtext
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(CurrencyType)}: {CurrencyType}, {nameof(Symbol)}: {Symbol}";
        }
    }

    public enum CurrencyType
    {
        USD,
        EUR,
        THB,
        SEK
    }
}
