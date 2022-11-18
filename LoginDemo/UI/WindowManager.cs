using Spectre.Console;
using RabbitEyeBank;

namespace LoginDemo.UI
{
    internal static class WindowManager
    {
        private static Stack<IWindow> _windowStack = new();
        public static int Level => _windowStack.Count;

        public static void showWindowStack()
        {
            foreach (var window in _windowStack)
            {
                Console.WriteLine(window?.GetType());
            }
        }

        public static void Navigate(IWindow from, IWindow to)
        {
            _windowStack.Push(from);
            to?.Show();
            _windowStack.Pop()?.Show();
        }
    }

    public class LoginWindow : IWindow
    {
        public void Show()
        {
            AnsiConsole.Clear();
            WindowManager.showWindowStack();
            AnsiConsole.WriteLine($"Level {WindowManager.Level}");

            //var font = FigletFont.Parse("coinstak");
            AnsiConsole.Write(new FigletText("R.E.B").LeftAligned().Color(Color.Green));

            // Old shit
            //AnsiConsole.WriteLine("Login screen here:");
            //var choice = AnsiConsole.Prompt(
            //    new SelectionPrompt<string>()
            //        .Title("Did the user log in as admin or customer?")
            //        .AddChoices("admin", "customer", "exit")
            //);

            string username = AnsiConsole.Prompt(
                new TextPrompt<string>("[green]Enter username?[/]").PromptStyle("green")
            );

            string password = AnsiConsole.Prompt(
                new TextPrompt<string>("[green]Enter password?[/]").PromptStyle("green")
            ); //.Secret()

            BankServices.Login(username, password);

            // Old shit
            //if (choice == "admin")
            //{
            //    WindowManager.Navigate(this, new AdminWindow());
            //}
            //else if (choice == "customer")
            //{
            //    WindowManager.Navigate(this, new CustomerWindow());
            //}
        }
    }

    public class AdminWindow : IWindow
    {
        public void Show()
        {
            AnsiConsole.Clear();
            WindowManager.showWindowStack();
            AnsiConsole.WriteLine($"Level {WindowManager.Level}");
            AnsiConsole.WriteLine("User creation screen here:");
            AnsiConsole.WriteLine("Press a key to go back. In real app Admin would choose exit.");
            Console.ReadKey();
        }
    }

    //"see bank accounts", "create new account", "transfer money"
    public class CustomerWindow : IWindow
    {
        private readonly SelectionPrompt<int> selection = new SelectionPrompt<int>()
            .Title("Where to go?")
            .AddChoices(1, 2, 3, 4);

        public void Show()
        {
            selection.Converter = (
                i =>
                {
                    return i switch
                    {
                        1 => "see bank accounts",
                        2 => "create new account",
                        3 => "transfer money",
                        4 => "exit"
                    };
                }
            );

            AnsiConsole.Clear();
            WindowManager.showWindowStack();
            AnsiConsole.WriteLine($"Level {WindowManager.Level}");
            AnsiConsole.WriteLine("Customers Landing screen here:");

            int choice = AnsiConsole.Prompt(selection);
            IWindow? window = choice switch
            {
                1 => new BankAccountWindow(),
                2 => new CreateAccountWindow(),
                3 => new MoneyTransferWindow(),
                _ => null
            };

            if (window is not null)
            {
                WindowManager.Navigate(this, window);
            }
        }
    }

    public class MoneyTransferWindow : IWindow
    {
        public void Show()
        {
            AnsiConsole.Clear();
            WindowManager.showWindowStack();
            AnsiConsole.WriteLine($"Level {WindowManager.Level}");
            AnsiConsole.WriteLine("Customers money transaction screen here:");
            AnsiConsole.WriteLine(
                "Press a key to go back. In real app Customer would choose exit."
            );
            Console.ReadKey();
        }
    }

    public class CreateAccountWindow : IWindow
    {
        public void Show()
        {
            AnsiConsole.Clear();
            WindowManager.showWindowStack();
            AnsiConsole.WriteLine($"Level {WindowManager.Level}");
            AnsiConsole.WriteLine("Customers account creation screen here:");
            AnsiConsole.WriteLine(
                "Press a key to go back. In real app Customer would choose exit."
            );
            Console.ReadKey();
        }
    }

    public class BankAccountWindow : IWindow
    {
        public void Show()
        {
            AnsiConsole.Clear();
            WindowManager.showWindowStack();
            AnsiConsole.WriteLine($"Level {WindowManager.Level}");
            AnsiConsole.WriteLine("Customers Account and balance screen here:");
            AnsiConsole.WriteLine(
                "Press a key to go back. In real app Customer would choose exit."
            );
            Console.ReadKey();
        }
    }
}
