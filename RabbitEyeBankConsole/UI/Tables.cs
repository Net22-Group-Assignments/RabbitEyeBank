using System.Globalization;
using RabbitEyeBankLibrary.Money;
using Spectre.Console;

namespace RabbitEyeBankConsole.UI;

public static class Tables
{
    public static Table AccountOverViewTable(IEnumerable<BankAccount> bankAccounts)
    {
        var table = new Table();
        table
            .AddColumns(
                new TableColumn("Account number"),
                new TableColumn("Account Name"),
                new TableColumn("Balance")
            )
            .Title("Bank Accounts");
        foreach (var bankAccount in bankAccounts)
        {
            table.AddRow(
                new Markup(bankAccount.AccountNumber),
                new Markup(bankAccount.Name),
                new Markup(
                    string.Format("{0:F2} {1}", bankAccount.Balance, bankAccount.Currency.Symbol)
                )
            );
        }

        table.RoundedBorder();
        return table;
    }

    /// <summary>
    /// Generates a table for single bank account.
    /// </summary>
    /// <param name="account">the account to show.</param>
    /// <returns>A table with the bank account</returns>
    public static Table SingleAccountTable(BankAccount? account)
    {
        var table = new Table()
            .Title($"Account Number: {(account != null ? account.AccountNumber : "")}")
            .RoundedBorder()
            .AddColumns(
                new TableColumn("Account Name"),
                new TableColumn("Balance"),
                new TableColumn("Currency")
            );

        if (account == null)
        {
            table.AddEmptyRow();
        }
        else
        {
            table.AddRow(
                new Markup(account.Name),
                new Markup(account.Balance.ToString("F2")),
                new Markup(account.Currency.ToString())
            );
        }
        return table;
    }

    public static Table TransferTable(IEnumerable<MoneyTransfer>? transfers)
    {
        var table = new Table();
        table
            .AddColumns(
                new TableColumn("Time"),
                new TableColumn("From"),
                new TableColumn("To"),
                new TableColumn("Amount"),
                new TableColumn("Status")
            )
            .Title("Transactions");

        foreach (var transfer in transfers)
        {
            table.AddRow(
                Markup.FromInterpolated($"{transfer.TimeOfRegistration}"),
                new Markup(transfer.FromAccount.AccountNumber),
                new Markup(transfer.ToAccount.AccountNumber),
                Markup.FromInterpolated($"{transfer.Amount:F2} {transfer.FromCurrency}"),
                Markup.FromInterpolated($"{transfer.Status}")
            );
        }

        if (transfers.Any() == false)
        {
            table.AddEmptyRow();
        }

        table.RoundedBorder();
        return table;
    }
}
