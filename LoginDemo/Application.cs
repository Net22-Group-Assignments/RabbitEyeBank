using LoginDemo.UI;
using RabbitEyeBank;
using Serilog;
using Spectre.Console;

namespace LoginDemo
{
    internal class Application
    {
        public void Run()
        {
            AnsiConsole.WriteLine("These users are pregenerated for testing use.");
            BankServices.CustomerList.ForEach(customer => Log.Information("{customer}", customer));
            AnsiConsole.WriteLine("Press a key to continue.");
            Console.ReadKey();
            do
            {
                WindowManager.Navigate(null, WindowManager.WindowDictionary[WindowName.Login]);
            } while (WindowManager.Level > 0);
        }
    }
}
