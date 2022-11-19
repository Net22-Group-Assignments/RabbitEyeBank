using RabbitEyeBank.Money;

namespace ClassTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create all available currencies in the dictionary.
            List<Currency> currencies = new List<Currency>();
            foreach (var keyValuePair in Currency.CurrencySymbolDictionary)
            {
                currencies.Add(new Currency(keyValuePair.Key, keyValuePair.Value));
            }
            currencies.ForEach(cur => Console.WriteLine(cur));
        }
    }
}
