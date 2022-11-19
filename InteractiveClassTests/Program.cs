using RabbitEyeBank.Money;

namespace ClassTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("All available currencies");
            foreach (var currency in Currency.CurrencyDictionary.Values)
            {
                Console.WriteLine(currency);
            }
        }
    }
}
