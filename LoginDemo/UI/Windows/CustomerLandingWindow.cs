using RabbitEyeBank.Services;
using REB.UI;
using Spectre.Console;

namespace LoginDemo.UI.Windows;

public class CustomerLandingWindow : CustomerHeader
{
    private readonly WindowName[] windowNames =
    {
        WindowName.BankAccount,
        WindowName.CreateAccount,
        WindowName.MoneyTransfer,
        WindowName.Logout
    };

    private readonly string[] selectionStrings =
    {
        "see bank accounts",
        "create new account",
        "transfer money",
        "logout"
    };

    private readonly SelectionPrompt<WindowName> selection;

    public CustomerLandingWindow()
    {
        selection = new SelectionPrompt<WindowName>().Title("Where to go?").AddChoices(windowNames);
        selection.Converter = Helpers.SelectionConverter(windowNames, selectionStrings);
    }

    public override void Show()
    {
        base.Show();
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
