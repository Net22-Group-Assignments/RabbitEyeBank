using RabbitEyeBank.Money;

namespace RabbitEyeTests;

public class MoneyTransferTests
{
    private readonly BankAccount bankAccount = new("1", "A", 0m, new Currency());

    [Fact]
    public void WithdrawalWhenAmountTooLittle_ThrowsException()
    {
        Assert.Throws<InvalidOperationException>(() => bankAccount.Withdraw(1m));
    }
}
