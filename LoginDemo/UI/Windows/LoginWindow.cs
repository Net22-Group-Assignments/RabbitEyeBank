using RabbitEyeBank;
using Spectre.Console;

namespace LoginDemo.UI.Windows
{
    public class LoginWindow : IWindow
    {
        public void Show()
        {
            AnsiConsole.Clear();
            WindowManager.showWindowStack();
            AnsiConsole.WriteLine($"Level {WindowManager.Level}");

            //var font = FigletFont.Parse("coinstak");
            AnsiConsole.Write(new FigletText("R.E.B").LeftAligned().Color(Color.Green));
            string username;
            string password;
            bool loggedIn = false;
            bool isAdmin = false;
            do
            {
                username = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter username?").PromptStyle("green")
                );

                password = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter password?").PromptStyle("green")
                ); //.Secret()

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
