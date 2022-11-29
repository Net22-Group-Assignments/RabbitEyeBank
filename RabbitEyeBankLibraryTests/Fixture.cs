using RabbitEyeBankLibrary.Money;
using RabbitEyeBankLibrary.Services;
using RabbitEyeBankLibrary.Users;

namespace RabbitEyeBankLibraryTests;

public class Fixture
{
    public UserService UserService;
    public AccountService AccountService;
    public CurrencyService CurrencyService;
    public MoneyTransferService MoneyTransferService;

    public Customer Customer1;
    public Customer Customer2;
    public BankAccount BankAccount1;
    public BankAccount BankAccount2;
    public BankAccount BankAccount3;

    public Currency ZorkMid;

    public Fixture()
    {
        AccountService = new AccountService();
        UserService = new UserService();
        CurrencyService = new CurrencyService();
        MoneyTransferService = new MoneyTransferService(
            UserService,
            AccountService,
            CurrencyService
        );
        ZorkMid = new Currency(CurrencyISO.SEK, "Z", 0.5m);

        Customer1 = new Customer("Alice", "Allison", "alice", "alice", true);
        Customer2 = new Customer("Bob", "Roberts", "bob", "bob", true);
        BankAccount1 = new BankAccount("1234", "savings", 100m, CurrencyService.Dollar, Customer1);
        BankAccount2 = new BankAccount("5678", "loan", 200m, CurrencyService.Dollar, Customer1);
        BankAccount3 = new BankAccount("9012", "slush-fund", 300m, ZorkMid, Customer2);
        AccountService.AddBankAccount(BankAccount1);
        AccountService.AddBankAccount(BankAccount2);
        AccountService.AddBankAccount(BankAccount3);
    }
}
