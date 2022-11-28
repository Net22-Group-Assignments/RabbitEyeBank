using RabbitEyeBankLibrary;
using RabbitEyeBankLibrary.Money;
using Spectre.Console;
using System.Runtime.Intrinsics.X86;
using RabbitEyeBankLibrary;
using RabbitEyeBankLibrary.Money;

namespace RabbitEyeBankConsole.UI.Windows;

public class CreateAccountWindow : CustomerHeader
{
    public override void Show()
    {
        string name = string.Empty;
        Currency currency = CurrencyService.Dollar;
        if (AnsiConsole.Confirm("Create a new Account?"))
        {
            name = AnsiConsole.Ask("Enter name of account", string.Empty);
            currency = AnsiConsole.Prompt(Prompts.CurrencySelector(CurrencyService.CurrencyList));
            if (AnsiConsole.Confirm("Create this Account?"))
            {
                AccountService.AddBankAccount(
                    new BankAccount(
                        BankData.GenerateAccountNumber(),
                        name,
                        0m,
                        currency,
                        UserService.LoggedInCustomer
                    )
                );
            }
        }
    }
}
