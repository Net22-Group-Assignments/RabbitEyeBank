using RabbitEyeBankConsole.UI;
using RabbitEyeBankLibrary.Services;
using Serilog;
using Spectre.Console;

namespace RabbitEyeBankConsole
{
    internal class Application
    {
        public static async Task AppTask()
        {
            ServiceContainer.MoneyTransferService.TransferTimeSpan = TimeSpan.FromSeconds(30);
            await Task.Run(() =>
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
            });
        }

        public static async Task TransactionTask()
        {
            MoneyTransferService moneyTransferService = ServiceContainer.MoneyTransferService;
            await Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    if (moneyTransferService.TransferReady())
                    {
                        moneyTransferService.CompleteTransfer();
                    }
                }
            });
        }
    }
}
