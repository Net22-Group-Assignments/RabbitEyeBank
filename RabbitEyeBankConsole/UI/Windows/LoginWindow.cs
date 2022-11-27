using Spectre.Console;

namespace RabbitEyeBankConsole.UI.Windows
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
                    new TextPrompt<string>("Enter username?").PromptStyle("blue")
                );

                var password = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter password?").PromptStyle("blue").Secret()
                );

                string loginStatus = UserService.Login(username, password);

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
                            UserService.GetCustomer(username)!.LoginAttempts
                        );
                        break;
                    case "RED":
                        textResponse =
                            "You have exceeded the password attempt limit. You are now locked out, please contact administrator.";
                        break;
                    case "ELK":
                        var customer = UserService.GetCustomer(username);
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
                Navigate(this, WindowManager.Windows[Admin]);
            }
            else
            {
                Navigate(this, WindowManager.Windows[BankAccount]);
            }
        }
    }
}
