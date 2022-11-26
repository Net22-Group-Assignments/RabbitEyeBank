using LoginDemo.UI;
using RabbitEyeBank.Services;
using Serilog;
using Spectre.Console;

namespace LoginDemo
{
    internal class Application
    {
        public void Run()
        {
            var devMode = true;

            AnsiConsole.WriteLine("These users are pre-generated for testing use.");
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
                if (devMode)
                {
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
                }
                Navigate(Windows[start], Windows[destination]);
            } while (Level > 0);
        }
    }
}
