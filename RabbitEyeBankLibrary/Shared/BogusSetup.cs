using RabbitEyeBankLibrary.Money;
using RabbitEyeBankLibrary.Services;
using RabbitEyeBankLibrary.Users;

namespace RabbitEyeBankLibrary.Shared
{
    public static class BogusSetup
    {
        private static readonly AccountService AccountService = ServiceContainer.AccountService;
        private static readonly UserService UserService = ServiceContainer.UserService;
        private static readonly CurrencyService CurrencyService = ServiceContainer.CurrencyService;

        /// <summary>
        /// Populates the bank service classes with randomly generated instances.
        /// The admin account and one customer is predefined with known values.
        /// The number of accounts is randomly generated between 0..maxAccounts.
        /// </summary>
        /// <param name="nCustomers">Number of customers.</param>
        /// <param name="maxAccounts">Max number of customers.</param>
        public static void InitData(int nCustomers, int maxAccounts)
        {
            // Known entities.
            // Admin? admin = new Admin("admin", "password", true);
            // UserService.Admin = admin;
            Customer customer = new Customer("John", "Doe", "username", "password", true);
            UserService.AddCustomer(customer);
            BankAccount b1 = new BankAccount(
                "11111111",
                "Savings",
                10000m,
                CurrencyService.CurrencyFromIso(CurrencyISO.SEK),
                customer
            );
            BankAccount b2 = new BankAccount(
                "22222222",
                "Wages",
                1000,
                CurrencyService.CurrencyFromIso(CurrencyISO.USD),
                customer
            );
            AccountService.AddBankAccount(b1);
            AccountService.AddBankAccount(b2);

            customer = new Customer("Jane", "Doe", "jade", "flower", true);
            UserService.AddCustomer(customer);
            BankAccount b3 = new BankAccount(
                "33333333",
                "Fun",
                0,
                CurrencyService.CurrencyFromIso(CurrencyISO.USD),
                customer
            );
            AccountService.AddBankAccount(b3);

            // Random entities.
            for (int i = 0; i < nCustomers; i++)
            {
                Customer? c = BogusData.Customer();
                for (int j = 0; j < Random.Shared.Next(maxAccounts + 1); j++)
                {
                    var acc = BogusData.BankAccount(c);
                    AccountService.AddBankAccount(acc);
                }
                UserService.AddCustomer(c);
            }
        }
    }
}
