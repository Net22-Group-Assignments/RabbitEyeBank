using System.Globalization;
using RabbitEyeBank.Money;
using RabbitEyeBank.Services;
using Spectre.Console;

namespace LoginDemo.UI.Windows;

public class BankAccountWindow : CustomerHeader
{
    private readonly MoneyTransferService mts = MoneyTransferService.Instance;

    public override void Show()
    {
        base.Show();
        IReadOnlyList<BankAccount> bankAccounts = bankService.LoggedInCustomer.BankAccountList;

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

        AnsiConsole.Write(table);

        if (bankAccounts.Count > 1)
        {
            BankAccount choiceFrom = AnsiConsole.Prompt(
                Prompts.BankAccountSelector("From Account", bankAccounts)
            );

            decimal amount = AnsiConsole.Prompt(
                new TextPrompt<decimal>("Transfer amount?")
                    .ValidationErrorMessage("Insufficient Funds")
                    .Validate(amount =>
                    {
                        return amount > choiceFrom.Balance
                            ? ValidationResult.Error()
                            : ValidationResult.Success();
                    })
            );

            BankAccount choiceTo = AnsiConsole.Prompt(
                Prompts.BankAccountSelector("To Account", bankAccounts, choiceFrom)
            );

            mts.RegisterTransfer(
                mts.CreateTransfer(choiceFrom, choiceTo, amount, choiceTo.Currency)
            );
            mts.CompleteTransfer();
        }

        AnsiConsole.WriteLine("Press a key to go back.");
        Console.ReadKey();
    }
}
