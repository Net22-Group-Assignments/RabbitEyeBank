using System.Globalization;
using RabbitEyeBank;
using RabbitEyeBank.Money;
using RabbitEyeBank.Users;
using Spectre.Console;

namespace LoginDemo.UI;

public class BankAccountWindow : IWindow
{
    public void Show()
    {
        AnsiConsole.Clear();
        WindowManager.showWindowStack();
        AnsiConsole.WriteLine($"Level {WindowManager.Level}");
        AnsiConsole.WriteLine("Customers Account and balance screen here:");

        var table = new Table();
        table.AddColumns(
            new TableColumn("Account Name"),
            new TableColumn("Balance"),
            new TableColumn("Currency")
        );

        foreach (Customer c in BankServices.UserList)
        {
            foreach (BankAccount ba in c.BankAccountList)
            {
                // ba.Name, ba.AccBalance, ba.Currency.Symbol
                table.AddRow(
                    new Markup(ba.Name),
                    new Markup(ba.AccBalance.ToString(CultureInfo.InvariantCulture)),
                    new Markup(ba.Currency.Symbol)
                );
            }
        }
        AnsiConsole.Write(table);

        AnsiConsole.WriteLine("Press a key to go back. In real app Customer would choose exit.");
        Console.ReadKey();
    }
}
