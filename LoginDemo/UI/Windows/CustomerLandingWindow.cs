using RabbitEyeBank;
using Spectre.Console;

namespace LoginDemo.UI.Windows;

public class CustomerWindow : IWindow
{
    private WindowName[] windowNames =
    {
        WindowName.BankAccount,
        WindowName.CreateAccount,
        WindowName.MoneyTransfer,
        WindowName.Logout
    };

    private string[] selectionStrings =
    {
        "see bank accounts",
        "create new account",
        "transfer money",
        "logout"
    };

    private readonly SelectionPrompt<WindowName> selection;

    public CustomerWindow()
    {
        selection = new SelectionPrompt<WindowName>().Title("Where to go?").AddChoices(windowNames);
        selection.Converter = Helpers.SelectionConverter(windowNames, selectionStrings);
    }

    public void Show()
    {
        AnsiConsole.Clear();
        WindowManager.showWindowStack();
        AnsiConsole.WriteLine($"Level {WindowManager.Level}");
        AnsiConsole.WriteLine("Customers Landing screen here:");

        WindowName choice = AnsiConsole.Prompt(selection);
        if (choice == WindowName.Logout)
        {
            AnsiConsole.Clear();
            BankServices.LogOut();
            AnsiConsole.WriteLine("You are now logged out of the system.");
            Console.ReadKey();
            return;
        }

        WindowManager.Navigate(this, WindowManager.WindowDictionary[choice]);
    }
}
