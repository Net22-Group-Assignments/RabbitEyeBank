using RabbitEyeBankLibrary.Money;
using RabbitEyeBankLibrary.Services;
using RabbitEyeBankLibrary.Users;

namespace RabbitEyeBankLibraryTests;

public class Fixture
{
    internal UserService UserService;
    internal Customer Customer1;
    internal Customer Customer2;

    internal Currency ZorkMid;
    internal Currency Dollar;

    public Fixture()
    {
        ZorkMid = new Currency(CurrencyISO.SEK, "Z", 0.5m);
        Dollar = new Currency(CurrencyISO.USD, "$", 1m);
        Customer1 = new Customer("Alice", "Allison", "alice", "alice", true);
        Customer2 = new Customer("Bob", "Roberts", "bob", "bob", true);
        UserService = new UserService();
        UserService.AddCustomer(Customer1);
        UserService.AddCustomer(Customer2);
    }

    public BankAccount BankAccount1()
    {
        return new BankAccount("1234", "savings", 100m, Dollar, Customer1);
    }

    public BankAccount BankAccount2()
    {
        return new BankAccount("5678", "loan", 200m, Dollar, Customer1);
    }

    public BankAccount BankAccount3()
    {
        return new BankAccount("9012", "slush-fund", 300m, ZorkMid, Customer2);
    }
}
