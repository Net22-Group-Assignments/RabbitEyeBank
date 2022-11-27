using RabbitEyeBankLibrary.Services;

namespace RabbitEyeBankConsole.UI.Windows
{
    public abstract class Header
    {
        protected UserService UserService;
        protected AccountService AccountService;
        protected MoneyTransferService MoneyTransferService;
        protected CurrencyService CurrencyService;

        protected Header(
            UserService userService,
            AccountService accountService,
            MoneyTransferService moneyTransferService,
            CurrencyService currencyService
        )
        {
            UserService = userService;
            AccountService = accountService;
            MoneyTransferService = moneyTransferService;
            CurrencyService = currencyService;
        }

        protected Header()
            : this(
                ServiceContainer.UserService,
                ServiceContainer.accountService,
                ServiceContainer.MoneyTransferService,
                ServiceContainer.CurrencyService
            ) { }
    }
}
