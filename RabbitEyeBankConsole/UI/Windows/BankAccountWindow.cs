﻿using RabbitEyeBankLibrary.Money;
using Spectre.Console;

namespace RabbitEyeBankConsole.UI.Windows;

public class BankAccountWindow : CustomerHeader
{
    private IReadOnlyList<BankAccount>? bankAccounts;

    public BankAccountWindow() { }

    public override void Show()
    {
        base.Show();
        bankAccounts = AccountService.BankAccountsByCustomer(UserService.LoggedInCustomer);

        AnsiConsole.Write(Widgets.AccountOverViewTable(bankAccounts));

        List<WindowName> windowChoices = new List<WindowName>();
        List<string> menuItems = new List<string>();
        if (bankAccounts.Count > 0)
        {
#if DEVMODE
            windowChoices.Add(BankAccountDetails);
            menuItems.Add("See Bank Account Details");
#endif
            windowChoices.Add(WindowName.MoneyTransfer);
            menuItems.Add("Transfer Money");
        }
#if DEVMODE
        windowChoices.Add(CreateAccount);
        menuItems.Add("Create New Bank Account");
#endif
        windowChoices.Add(Logout);
        menuItems.Add("Log Out");

        WindowName choice = AnsiConsole.Prompt(
            new SelectionPrompt<WindowName>()
                .Title("What do you want to do?")
                .AddChoices(windowChoices)
                .UseConverter(Prompts.SelectionConverter(windowChoices, menuItems))
        );

        if (choice == Logout)
        {
            AnsiConsole.Clear();
            UserService.LogOut();
            AnsiConsole.WriteLine("You are now logged out of the system.");
            Console.ReadKey();
            return;
        }

        Navigate(this, WindowManager.Windows[choice]);
    }
}
