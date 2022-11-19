using Bogus;
using RabbitEyeBank;
using RabbitEyeBank.Money;
using RabbitEyeBank.Users;

namespace BankClassLib.Helpers
{
    /// <summary>
    /// Provides methods for autogenerating bank objects using Bogus, Faker in C#.
    /// </summary>
    public static class BogusData
    {
        /// <summary>
        /// A Facade for Bogus.
        /// </summary>
        private static readonly Faker faker = new();

        /// <summary>
        /// Generates a random instance of <c>Customer</c>.
        /// </summary>
        /// <returns>A random <c>Customer</c>.</returns>
        public static Customer Customer()
        {
            Customer customer = new Customer(
                faker.Name.FirstName(),
                faker.Name.LastName(),
                faker.Internet.UserName(),
                faker.Internet.Password(length: 8, memorable: true),
                faker.Random.Bool()
            );
            return customer;
        }

        /// <summary>
        /// Generates a random instance of <c>BankAccount</c>.
        /// </summary>
        /// <param name="customer">An instance of <c>Customer</c>.</param>
        /// <returns>A random <c>BankAccount</c>.</returns>
        public static BankAccount BankAccount()
        {
            return new BankAccount(
                faker.Finance.AccountName(),
                faker.Finance.Amount(),
                faker.Random.CollectionItem(BankData.CurrencyDictionary.Values)
            );
        }
    }
}
