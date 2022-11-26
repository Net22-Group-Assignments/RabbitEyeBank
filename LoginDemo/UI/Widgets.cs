using RabbitEyeBank.Money;
using Spectre.Console;
using System.Globalization;

namespace LoginDemo.UI;

public static class Widgets
{
    public static Table AccountOverViewTable(IEnumerable<BankAccount> bankAccounts)
    {
        var table = new Table();
        table.AddColumns(
            new TableColumn("Account number"),
            new TableColumn("Account Name"),
            new TableColumn("Balance")
        );
        foreach (var bankAccount in bankAccounts)
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

        return table;
    }

    public static Table TransferTable(IEnumerable<MoneyTransfer> transfers)
    {
        var table = new Table();
        table.AddColumns(
            new TableColumn("Time"),
            new TableColumn("From"),
            new TableColumn("To"),
            new TableColumn("Amount"),
            new TableColumn("Status")
        );
        foreach (var transfer in transfers)
        {
            table.AddRow(
                Markup.FromInterpolated($"{transfer.TimeOfRegistration}"),
                new Markup(transfer.FromAccount.AccountNumber),
                new Markup(transfer.ToAccount.AccountNumber),
                Markup.FromInterpolated($"{transfer.Amount} {transfer.FromCurrency}"),
                Markup.FromInterpolated($"{transfer.Status}")
            );
        }

        if (transfers.Any() == false)
        {
            table.AddEmptyRow();
        }

        return table;
    }
}
