using RabbitEyeBankLibrary.Money;
using Spectre.Console;

namespace RabbitEyeBankConsole.UI.Windows;

public class BankAccountDetailsWindow : CustomerHeader
{
    private IReadOnlyList<BankAccount> accounts;
    private BankAccount? Account { get; set; }

    private enum MenuChoice
    {
        SwitchAccount,
        SwitchCurrency,
        Exit
    }

    /// <inheritdoc />
    public override void Show()
    {
        accounts = AccountService.BankAccountsByCustomer(UserService.LoggedInCustomer);
        while (true)
        {
            base.Show();
            if (Account == null)
            {
                Account = accounts.FirstOrDefault();
            }
            AnsiConsole.Write(Widgets.SingleAccountTable(Account));
            AnsiConsole.Write(
                Widgets.TransferTable(
                    Account is not null
                        ? MoneyTransferService.TransfersByAccount(Account)
                        : new List<MoneyTransfer>()
                )
            );
            List<MenuChoice> menuChoices = new List<MenuChoice>();
            List<string> menuCaptions = new List<string>();
            if (accounts.Count > 1)
            {
                menuChoices.Add(MenuChoice.SwitchAccount);
                menuCaptions.Add("Switch Account");
            }
            menuChoices.AddRange(new[] { MenuChoice.SwitchCurrency, MenuChoice.Exit });
            menuCaptions.AddRange(new[] { "Switch Currency", "Exit" });

            var menuSelect = new SelectionPrompt<MenuChoice>()
                .Title("Options")
                .AddChoices(menuChoices)
                .UseConverter(Prompts.SelectionConverter(menuChoices, menuCaptions));
            ;

            switch (AnsiConsole.Prompt(menuSelect))
            {
                case MenuChoice.SwitchAccount:
                    var newAccount = AnsiConsole.Prompt(
                        Prompts.BankAccountSelector(
                            "Select a bank account:",
                            AccountService.BankAccountsByCustomer(UserService.LoggedInCustomer)
                        )
                    );
                    if (newAccount != Prompts.NullBankAccount)
                    {
                        Account = newAccount;
                    }
                    break;
                case MenuChoice.SwitchCurrency:
                    Currency newCurrency = AnsiConsole.Prompt(
                        Prompts.CurrencySelector(CurrencyService.CurrencyList)
                    );
                    Account.Currency = newCurrency;
                    break;
                case MenuChoice.Exit:
                    Account = null;
                    return;
            }
        }
    }
}
