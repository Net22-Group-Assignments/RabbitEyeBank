using RabbitEyeBank;

namespace ClassTests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("All available currencies");
            foreach (var currency in BankData.CurrencyDictionary.Values)
            {
                Console.WriteLine(currency);
            }
        }
    }
}
