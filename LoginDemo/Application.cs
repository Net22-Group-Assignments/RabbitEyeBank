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
            foreach (var customer in ServiceContainer.bankService.CustomerList)
            {
                Log.Information("{customer}", customer);
            }
            AnsiConsole.WriteLine("Press a key to continue.");
            Console.ReadKey();
            do
            {
                WindowName destination = WindowName.Login;
                WindowName start = WindowName.Login;
                if (devMode)
                {
                    if (AnsiConsole.Confirm("Login as admin?"))
                    {
                        ServiceContainer.bankService.Login("admin", "admin");
                        destination = WindowName.Admin;
                    }
                    else
                    {
                        ServiceContainer.bankService.Login("username", "password");
                        destination = WindowName.Customer;
                    }
                }
                WindowManager.Navigate(
                    WindowManager.WindowDictionary[start],
                    WindowManager.WindowDictionary[destination]
                );
            } while (WindowManager.Level > 0);
        }
    }
}
