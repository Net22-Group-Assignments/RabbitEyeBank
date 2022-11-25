using RabbitEyeBank.Money;

namespace RabbitEyeTests;

public class MoneyTransferTests : IClassFixture<Fixture>
{
    private readonly Fixture fixture;

    public MoneyTransferTests(Fixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void TransferBetweenTwoOwnAccounts()
    {
        var transfer = fixture.MoneyTransferService.CreateTransfer(
            fixture.BankAccount1,
            fixture.BankAccount2,
            100m,
            new Currency()
        );
        fixture.MoneyTransferService.RegisterTransfer(transfer);
        fixture.MoneyTransferService.CompleteTransfer();
        Assert.Equal(0m, fixture.BankAccount1.Balance);
        Assert.Equal(300m, fixture.BankAccount2.Balance);
    }
}
