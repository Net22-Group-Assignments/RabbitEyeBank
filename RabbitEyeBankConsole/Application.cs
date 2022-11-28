using RabbitEyeBankConsole.UI;
using RabbitEyeBankLibrary.Services;
using Serilog;
using Spectre.Console;

namespace RabbitEyeBankConsole
{
    internal class Application
    {
        public void Run()
        {
            AnsiConsole.WriteLine("These users are pre-generated for testing use.");
            AnsiConsole.MarkupLine("[red]Admin Username: admin Password: admin[/]");
            AnsiConsole.WriteLine("John Doe has account numbers 11111111 and 22222222");
            AnsiConsole.WriteLine("Jane Doe has one account with account number 33333333");
            AnsiConsole.WriteLine("Use those when trying out moneytransfers.");
            foreach (var customer in ServiceContainer.UserService.CustomerList)
            {
                Log.Information("{customer}", customer);
            }
            AnsiConsole.WriteLine("Press a key to continue.");
            Console.ReadKey();
            do
            {
                WindowName destination = Login;
                WindowName start = Login;
#if DEVMODE
                if (AnsiConsole.Confirm("Login as admin?"))
                {
                    ServiceContainer.UserService.Login("admin", "admin");
                    destination = Admin;
                }
                else
                {
                    ServiceContainer.UserService.Login("username", "password");
                    destination = BankAccount;
                }
#endif
                Navigate(Windows[start], Windows[destination]);
            } while (Level > 0);
        }
    }
}
