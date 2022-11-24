using System.Globalization;
using RabbitEyeBank;
using RabbitEyeBank.Money;
using RabbitEyeBank.Users;
using Spectre.Console;

namespace LoginDemo.UI.Windows;

public class BankAccountWindow : CustomerHeader
{
    public override void Show()
    {
        base.Show();
        var table = new Table();
        table.AddColumns(
            new TableColumn("Account number"),
            new TableColumn("Account Name"),
            new TableColumn("Balance")
        );
        foreach (var bankAccount in BankServices.LoggedInCustomer.BankAccountList)
        {
            table.AddRow(
                new Markup(bankAccount.AccountNumber),
                new Markup(bankAccount.Name),
                new Markup(
                    string.Format(
                        "{0} {1}",
                        bankAccount.Balance.ToString(CultureInfo.InvariantCulture),
                        bankAccount.Currency.Symbol
                    )
                )
            );
        }

        AnsiConsole.Write(table);

        AnsiConsole.WriteLine("Press a key to go back.");
        Console.ReadKey();
    }
}
