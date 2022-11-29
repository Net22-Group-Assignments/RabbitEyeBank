using RabbitEyeBankLibrary.Money;
using Spectre.Console;

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
                        AccountService.GenerateAccountNumber(),
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
