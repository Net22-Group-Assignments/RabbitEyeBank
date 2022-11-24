using REB.UI;
using Serilog;
using Spectre.Console;
using RabbitEyeBank.Services;

namespace REB
{
    internal class Application
    {
        public void Run()
        {
            AnsiConsole.WriteLine("These users are pre-generated for testing use.");
            foreach (var customer in BankServices.CustomerList)
            {
                Log.Information("{customer}", customer);
            }
            AnsiConsole.WriteLine("Press a key to continue.");
            Console.ReadKey();
            do
            {
                WindowManager.Navigate(null, WindowManager.WindowDictionary[WindowName.Login]);
            } while (WindowManager.Level > 0);
        }
    }
}
