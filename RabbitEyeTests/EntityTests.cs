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
        bankAccount1 = new BankAccount("1234", "savings", 100m, new Currency(), customer1);
        bankAccount2 = new BankAccount("5678", "loan", 200m, new Currency(), customer1);
        bankAccount3 = new BankAccount("9012", "slush-fund", 300m, new Currency(), customer2);
    }

    [Fact]
    public void WithdrawalWhenAmountTooLittle_ThrowsException()
    {
        Assert.Throws<InvalidOperationException>(() => bankAccount1.Withdraw(101m));
    }

    [Fact]
    public void CustomersBankAccountPropertyShouldFindOwnAccounts()
    {
        var accountService = AccountService.Instance;
        IReadOnlyList<BankAccount> accounts = accountService.BankAccountsByCustomer(customer1);
        Assert.Equal(new[] { bankAccount1, bankAccount2 }, accounts);
    }
}
