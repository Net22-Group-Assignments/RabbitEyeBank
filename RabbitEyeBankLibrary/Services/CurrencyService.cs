using System.Globalization;
using RabbitEyeBankLibrary.Money;

namespace RabbitEyeBankLibrary.Services;

public class CurrencyService
{
    private readonly (string, decimal)[] cultureNamesDollarValues =
    {
        ("en-US", 1m),
        ("be-BE", 1.04m),
        ("th-TH", 0.028m),
        ("sv-SE", 0.096m)
    };
    private readonly Dictionary<CurrencyISO, Currency> currencyDictionary;

    public IReadOnlyList<CurrencyISO> CurrencyISOList => currencyDictionary.Keys.ToList();
    public IReadOnlyList<Currency> CurrencyList => currencyDictionary.Values.ToList();

    /// <summary>
    /// Initializes the dictionary by connecting the currency type
    /// to the currency symbol in the CultureInfo class.
    /// </summary>
    public CurrencyService()
    {
        var currencyISOAndCurrency = Enum.GetValues<CurrencyISO>()
            .Zip(
                cultureNamesDollarValues,
                (currencyIso, currencyInstance) =>
                    new KeyValuePair<CurrencyISO, Currency>(
                        currencyIso,
                        new Currency(
                            currencyIso,
                            CultureInfo
                                .GetCultureInfo(currencyInstance.Item1)
                                .NumberFormat.CurrencySymbol,
                            currencyInstance.Item2
                        )
                    )
            );

        currencyDictionary = new(currencyISOAndCurrency);

        // Correct to the _real_ SEK symbol:
        Currency sweCurrency = currencyDictionary[CurrencyISO.SEK];
        sweCurrency.Symbol = "♕";
        currencyDictionary[CurrencyISO.SEK] = sweCurrency;
    }

    public void EditCurrency(CurrencyISO iso, decimal dollarValue)
    {
        var currency = currencyDictionary[iso];
        currency.DollarValue = dollarValue;
        currencyDictionary[iso] = currency;
    }
}
