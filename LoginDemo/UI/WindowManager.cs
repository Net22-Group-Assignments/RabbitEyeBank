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

        /// <summary>
        /// Switches to another IWindow. The 'from' window is put on the stack,
        /// so when the show method in IWindow returns, it will pop and you will be in
        /// this window.
        /// </summary>
        /// <param name="from">The IWindow you are in now.</param>
        /// <param name="to">The IWindow you will jump to.</param>
        public static void Navigate(IWindow from, IWindow to)
        {
            _windowStack.Push(from);
            to?.Show();
            // show method return here.
            _windowStack.Pop()?.Show();
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
                        4 => "logout"
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
            AnsiConsole.Clear();
            AnsiConsole.WriteLine("You are now logged out of the system.");
            Console.ReadKey();
            BankServices.LogOut();
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
