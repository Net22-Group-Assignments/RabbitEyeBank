using RabbitEyeBank.Money;
using RabbitEyeBank.Users;

namespace RabbitEyeTests;

public class MoneyTransferTests
{
    private readonly Customer customer;
    private readonly BankAccount bankAccount;

    public MoneyTransferTests()
    {
        customer = new Customer("User", "Name", "username", "password", true);
        bankAccount = new("1", "A", 0m, new Currency(), customer);
    }

    [Fact]
    public void WithdrawalWhenAmountTooLittle_ThrowsException()
    {
        Assert.Throws<InvalidOperationException>(() => bankAccount.Withdraw(1m));
    }
}
