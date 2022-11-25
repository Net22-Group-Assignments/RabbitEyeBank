using RabbitEyeBank.Money;
using RabbitEyeBank.Services;
using RabbitEyeBank.Users;

namespace RabbitEyeBank.Shared
{
    internal class BankServiceAdapter
    {
        private static readonly Lazy<BankServiceAdapter> _instance = new Lazy<BankServiceAdapter>(
            () => new BankServiceAdapter()
        );

        public static BankServiceAdapter Instance => _instance.Value;

        private readonly AccountService accountService;

        protected BankServiceAdapter()
        {
            accountService = AccountService.Instance;
        }

        public IReadOnlyList<BankAccount> BankAccountsByCustomer(Customer customer)
        {
            return accountService.BankAccountsByCustomer(customer);
        }
    }
}
