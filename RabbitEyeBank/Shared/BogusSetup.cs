using BankClassLib.Helpers;
using RabbitEyeBank.Money;
using RabbitEyeBank.Services;
using RabbitEyeBank.Users;

namespace RabbitEyeBank.Shared
{
    public static class BogusSetup
    {
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
            BankServices.CustomerList.Add(customer);
            BankAccount b1 = new BankAccount(
                BankData.GenerateAccountNumber(),
                "Savings",
                10000m,
                BankData.CurrencyDictionary[CurrencyISO.SEK]
            );
            BankAccount b2 = new BankAccount(
                BankData.GenerateAccountNumber(),
                "Wages",
                1000,
                BankData.CurrencyDictionary[CurrencyISO.USD]
            );
            customer.BankAccountList.Add(b1);
            customer.BankAccountList.Add(b2);

            customer = new Customer("Jane", "Doe", "jade", "flower", false);
            BankServices.CustomerList.Add(customer);
            // Random entities.
            for (int i = 0; i < nCustomers; i++)
            {
                Customer? c = BogusData.Customer();
                for (int j = 0; j < Random.Shared.Next(maxAccounts + 1); j++)
                {
                    var acc = BogusData.BankAccount();
                    c.BankAccountList.Add(acc);
                }
                BankServices.CustomerList.Add(c);
            }
        }
    }
}
