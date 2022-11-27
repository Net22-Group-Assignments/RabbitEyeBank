using System.Reflection.Metadata.Ecma335;
using RabbitEyeBank.Money;
using RabbitEyeBank.Users;
using Spectre.Console;

namespace LoginDemo.UI.Windows;

public class MoneyTransferWindow : CustomerHeader
{
    private IReadOnlyList<BankAccount>? bankAccounts;
    private IReadOnlyList<MoneyTransfer>? transfers;

    private enum MenuChoice
    {
        OwnAccounts,
        OtherAccounts,
        Exit
    }

    public override void Show()
    {
        Customer currentCustomer = UserService.LoggedInCustomer;
        while (true)
        {
            base.Show();
            bankAccounts = AccountService.BankAccountsByCustomer(currentCustomer);
            transfers = MoneyTransferService.TransfersByCustomer(currentCustomer);
            AnsiConsole.Write(Widgets.TransferTable(transfers));

            var menuSelect = new SelectionPrompt<MenuChoice>().Title("What do you want to do?");
            if (bankAccounts.Count > 1)
            {
                menuSelect.AddChoice(MenuChoice.OwnAccounts);
            }

            menuSelect.AddChoices(MenuChoice.OtherAccounts, MenuChoice.Exit);
            menuSelect.Converter = Prompts.SelectionConverter(
                new[] { MenuChoice.OwnAccounts, MenuChoice.OtherAccounts, MenuChoice.Exit },
                new[] { "Transfer between your accounts", "Transfer to other accounts", "Exit" }
            );

            var menuChoice = AnsiConsole.Prompt(menuSelect);

            if (menuChoice == MenuChoice.Exit)
            {
                return;
            }

            BankAccount choiceFrom = null;
            if (menuChoice == MenuChoice.OwnAccounts || menuChoice == MenuChoice.OtherAccounts)
            {
                choiceFrom = AnsiConsole.Prompt(
                    Prompts.BankAccountSelector("From Account", bankAccounts)
                );
                if (choiceFrom == Prompts.NullBankAccount)
                {
                    continue;
                }
            }

            decimal amount = AnsiConsole.Prompt(Prompts.AmountPrompt(choiceFrom));

            if (amount == 0)
            {
                continue;
            }

            BankAccount choiceTo = null;
            if (menuChoice == MenuChoice.OwnAccounts)
            {
                choiceTo = AnsiConsole.Prompt(
                    Prompts.BankAccountSelector("To Account", bankAccounts, choiceFrom)
                );
                if (choiceFrom == Prompts.NullBankAccount)
                {
                    continue;
                }
            }
            else
            {
                string bankAccountNumber = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter AccountNumber (Leave blank to cancel)")
                        .AllowEmpty()
                        .DefaultValue("")
                        .HideDefaultValue()
                        .ValidationErrorMessage("Bank Account Unavailable")
                        .Validate(accountNumber =>
                        {
                            if (AccountService.BankAccountExists(accountNumber))
                            {
                                if (
                                    AccountService.BankAccountByAccountNumber(accountNumber).Owner
                                    == currentCustomer
                                )
                                {
                                    return ValidationResult.Error("This account belongs to you");
                                }

                                return ValidationResult.Success();
                            }

                            return ValidationResult.Error("Account does not exist");
                        })
                );
                if (string.IsNullOrEmpty(bankAccountNumber))
                {
                    continue;
                }

                choiceTo = AccountService.BankAccountByAccountNumber(bankAccountNumber);
            }

            var transfer = MoneyTransferService.CreateTransfer(
                choiceFrom,
                choiceTo,
                amount,
                choiceFrom.Currency,
                choiceTo.Currency
            );

            AnsiConsole.MarkupLineInterpolated($"Transfer {transfer}");
            if (AnsiConsole.Confirm("Proceed with this transfer:"))
            {
                MoneyTransferService.TransferMoney(transfer);
                AnsiConsole.MarkupLineInterpolated(
                    $"Transfer registered at: {transfer.TimeOfRegistration}"
                );
            }
            else
            {
                AnsiConsole.WriteLine("Transfer cancelled.");
            }

            AnsiConsole.WriteLine("Press a key to go back.");
            Console.ReadKey();
        }
    }
}
