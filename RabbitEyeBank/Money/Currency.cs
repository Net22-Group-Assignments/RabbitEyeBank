using System.Collections.Generic;

namespace RabbitEyeBank.Money
{
    public class Currency
    {
        public static Dictionary<CurrencyType, string> CurrenctyDict;

        public CurrencyType CurrencyType { get; }
        public string Symbol { get; }

        public Currency(CurrencyType currencyType, string symbol)
        {
            this.CurrencyType = currencyType;
            this.Symbol = symbol;
            //Hundtext
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
