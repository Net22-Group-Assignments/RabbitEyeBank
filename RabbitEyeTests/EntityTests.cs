using RabbitEyeBank.Money;
using RabbitEyeBank.Services;
using RabbitEyeBank.Users;

namespace RabbitEyeTests;

public class EntityTests
{
    private readonly Customer customer1;
    private readonly Customer customer2;
    private readonly BankAccount bankAccount1;
    private readonly BankAccount bankAccount2;
    private readonly BankAccount bankAccount3;

    public EntityTests()
    {
        customer1 = new Customer("Alice", "Allison", "alice", "alice", true);
        customer2 = new Customer("Bob", "Roberts", "bob", "bob", true);
        BankServices.AddCustomer(customer1);
        BankServices.AddCustomer(customer2);
        bankAccount1 = new BankAccount("1234", "savings", 100m, new Currency(), customer1);
        bankAccount2 = new BankAccount("5678", "loan", 200m, new Currency(), customer1);
        bankAccount3 = new BankAccount("9012", "slush-fund", 300m, new Currency(), customer2);
        AccountService.AddBankAccount(bankAccount1);
        AccountService.AddBankAccount(bankAccount2);
        AccountService.AddBankAccount(bankAccount3);
    }

    [Fact]
    public void CustomersBankAccountPropertyShouldFindOwnAccounts()
    {
        IReadOnlyList<BankAccount> accounts = AccountService.BankAccountsByCustomer(customer1);
        Assert.Equal(new[] { bankAccount1, bankAccount2 }, accounts);
    }
}
