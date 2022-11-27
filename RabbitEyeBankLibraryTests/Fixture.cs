using RabbitEyeBank.Money;
using RabbitEyeBank.Services;
using RabbitEyeBank.Users;

namespace RabbitEyeTests;

public class Fixture
{
    public UserService UserService;
    public AccountService AccountService;
    public MoneyTransferService MoneyTransferService;

    public Customer Customer1;
    public Customer Customer2;
    public BankAccount BankAccount1;
    public BankAccount BankAccount2;
    public BankAccount BankAccount3;

    public Fixture()
    {
        AccountService = new AccountService();
        UserService = new UserService();
        MoneyTransferService = new MoneyTransferService(UserService, AccountService);

        Customer1 = new Customer("Alice", "Allison", "alice", "alice", true);
        Customer2 = new Customer("Bob", "Roberts", "bob", "bob", true);
        BankAccount1 = new BankAccount("1234", "savings", 100m, new Currency(), Customer1);
        BankAccount2 = new BankAccount("5678", "loan", 200m, new Currency(), Customer1);
        BankAccount3 = new BankAccount("9012", "slush-fund", 300m, new Currency(), Customer2);
        AccountService.AddBankAccount(BankAccount1);
        AccountService.AddBankAccount(BankAccount2);
    }
}
