using RabbitEyeBank.Services;
using REB.UI;
using Spectre.Console;

namespace LoginDemo.UI.Windows
{
    internal class LoginWindow : CustomerHeader
    {
        public override void Show()
        {
            base.Show();
            bool loggedIn = false;
            var isAdmin = false;
            do
            {
                var username = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter username?").PromptStyle("green")
                );

                var password = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter password?").PromptStyle("green")
                );

                string loginStatus = bankService.Login(username, password);

                string textResponse = "";
                switch (loginStatus)
                {
                    case "KNG":
                        textResponse = "\nWELCOME MASTER";
                        isAdmin = true;
                        loggedIn = true;
                        break;
                    case "SBS":
                        textResponse = "Please check your username, your username is not found.";
                        break;
                    case "RIP":
                        textResponse = "This account is inactive, please contact administrator.";
                        break;
                    case "SBK":
                        textResponse = string.Format(
                            "Wrong password. This is attempt {0} of 3",
                            bankService.GetCustomer(username)!.LoginAttempts
                        );
                        break;
                    case "RED":
                        textResponse =
                            "You have exceeded the password attempt limit. You are now locked out, please contact administrator.";
                        break;
                    case "ELK":
                        var customer = bankService.GetCustomer(username);
                        var fullName = $"{customer.FirstName} {customer.LastName}";
                        textResponse = $"Login successful, welcome {fullName}!";
                        loggedIn = true;
                        break;
                }
                AnsiConsole.WriteLine(textResponse);
                Console.ReadKey();
            } while (loggedIn != true);

            if (isAdmin)
            {
                WindowManager.Navigate(this, WindowManager.WindowDictionary[WindowName.Admin]);
            }
            else
            {
                WindowManager.Navigate(this, WindowManager.WindowDictionary[WindowName.Customer]);
            }
        }
    }
}
