using System.Globalization;
using RabbitEyeBank.Money;
using RabbitEyeBank.Services;
using Spectre.Console;

namespace LoginDemo.UI.Windows;

public class BankAccountWindow : CustomerHeader
{
    public override void Show()
    {
        base.Show();
        IReadOnlyList<BankAccount> bankAccounts = accountService.BankAccountsByCustomer(
            bankService.LoggedInCustomer
        );

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

            decimal amount = AnsiConsole.Prompt(Prompts.AmountPrompt(choiceFrom));

            if (amount > 0)
            {
                BankAccount choiceTo = AnsiConsole.Prompt(
                    Prompts.BankAccountSelector("To Account", bankAccounts, choiceFrom)
                );

                var transfer = moneyTransferService.CreateTransfer(
                    choiceFrom,
                    choiceTo,
                    amount,
                    choiceTo.Currency
                );

                AnsiConsole.MarkupLineInterpolated($"Transfer {transfer}");
                if (AnsiConsole.Confirm("Proceed with this transfer:"))
                {
                    moneyTransferService.TransferMoney(transfer);
                    AnsiConsole.MarkupInterpolated(
                        $"Transfer registered at: {transfer.TimeOfRegistration}"
                    );
                }
                else
                {
                    AnsiConsole.Write("Transfer cancelled.");
                }

                AnsiConsole.WriteLine("Press a key to go back.");
                Console.ReadKey();
            }
        }

        AnsiConsole.WriteLine("Press a key to go back.");
        Console.ReadKey();
    }
}
