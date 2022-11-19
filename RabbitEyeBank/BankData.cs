using System.Globalization;
using RabbitEyeBank.Money;

namespace RabbitEyeBank;

public static class BankData
{
    private static readonly string[] cultureNames = { "en-US", "be-BE", "th-TH", "sv-SE" };
    public static readonly Dictionary<CurrencyISO, Currency> CurrencyDictionary;

    // TODO Should the dictionary be stored somewhere else, like BankService?

    /// <summary>
    /// Initializes the dictionary by connecting the currency type
    /// to the currency symbol in the CultureInfo class.
    /// </summary>
    static BankData()
    {
        var currencyISOAndCurrency = Enum.GetValues<CurrencyISO>()
            .Zip(
                cultureNames,
                (currencyIso, currencyInstance) =>
                    new KeyValuePair<CurrencyISO, Currency>(
                        currencyIso,
                        new Currency(
                            currencyIso,
                            CultureInfo.GetCultureInfo(currencyInstance).NumberFormat.CurrencySymbol
                        )
                    )
            );

        CurrencyDictionary = new Dictionary<CurrencyISO, Currency>(currencyISOAndCurrency);

        // Correct to the _real_ SEK symbol:
        CurrencyDictionary[CurrencyISO.SEK] = new Currency(CurrencyISO.SEK, "♕");
    }
}

public enum CurrencyISO
{
    USD,
    EUR,
    THB,
    SEK,
}
