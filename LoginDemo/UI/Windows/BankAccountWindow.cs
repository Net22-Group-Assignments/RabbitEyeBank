using System.Globalization;
using Spectre.Console;

namespace LoginDemo.UI.Windows;

public class BankAccountWindow : CustomerHeader
{
    /// <inheritdoc />
    //public BankAccountWindow(
    //    BankService bankService,
    //    AccountService accountService,
    //    MoneyTransferService moneyTransferService
    //) : base(bankService, accountService, moneyTransferService) { }

    public override void Show()
    {
        base.Show();
        var table = new Table();
        table.AddColumns(
            new TableColumn("Account number"),
            new TableColumn("Account Name"),
            new TableColumn("Balance")
        );
        foreach (var bankAccount in bankService.LoggedInCustomer.BankAccountList)
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
