using Serilog;
using Spectre.Console;

namespace LoginDemo.UI.Windows;

public class AdminWindow : CustomerHeader
{
    /// <inheritdoc />
    public override void Show()
    {
        base.Show();
        var bankDataTable = new Table();
        bankDataTable
            .Title("Overview")
            .AddColumns(
                new TableColumn("Total Customers"),
                new TableColumn("Total Accounts"),
                new TableColumn("Total Transactions")
            )
            .AddRow(
                new Markup(UserService.CustomerList.Count.ToString()),
                new Markup(AccountService.AccountList.Count.ToString()),
                new Markup(MoneyTransferService.TransferLog.Count.ToString())
            );
        AnsiConsole.Write(bankDataTable);

        var windowChoices = new[] { CreateCustomer, Currency, TransferControl, Logout };
        var menuItems = new[]
        {
            "Create new Customer account",
            "Edit Currency Exchange Values",
            "Transaction Control",
            "Log Out"
        };

        WindowName choice = AnsiConsole.Prompt(
            new SelectionPrompt<WindowName>()
                .Title("Operation:")
                .AddChoices(windowChoices)
                .UseConverter(Prompts.SelectionConverter(windowChoices, menuItems))
        );

        if (choice == Logout)
        {
            AnsiConsole.Clear();
            UserService.LogOut();
            AnsiConsole.WriteLine("You are now logged out of the system.");
            Console.ReadKey();
            return;
        }

        Navigate(this, WindowManager.Windows[choice]);
    }
}
