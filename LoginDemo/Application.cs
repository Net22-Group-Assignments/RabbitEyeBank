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
            BankServices.UserList.ForEach(customer => Log.Information("{customer}", customer));
            AnsiConsole.WriteLine("Press a key to continue.");
            Console.ReadKey();
            WindowManager.Navigate(null, new BankAccountWindow());
            do
            {
                WindowManager.Navigate(null, new LoginWindow());
            } while (WindowManager.Level > 0);
        }
    }
}
