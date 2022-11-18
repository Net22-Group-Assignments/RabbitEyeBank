using System.Collections.Generic;

namespace RabbitEyeBank
{
    public class Currency
    {
        public static Dictionary<CurrencyType, string> currenctyDict;
        
        public CurrencyType CurrencyType { get;}
        public string Symbol { get; }

        public Currency(CurrencyType currencyType, string symbol)
        {
            this.CurrencyType = CurrencyType;
            this.Symbol= symbol;
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
