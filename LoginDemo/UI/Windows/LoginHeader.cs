using RabbitEyeBank;
using Spectre.Console;
using LoginDemo.UI;

namespace LoginDemo.UI.Windows
{
    internal class LoginWindow : CustomerWindow
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

                string loginStatus = BankServices.Login(username, password);

                string textResponse = "";
                switch (loginStatus)
                {
                    case "KNG":
                        textResponse = "WELCOME MASTER";
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
                            BankServices.GetCustomer(username)!.LoginAttempts
                        );
                        break;
                    case "RED":
                        textResponse =
                            "You have exceeded the password attempt limit. You are now locked out, please contact administrator.";
                        break;
                    case "ELK":
                        var customer = BankServices.GetCustomer(username);
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
