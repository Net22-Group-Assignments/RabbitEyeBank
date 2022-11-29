using RabbitEyeBankLibrary.Money;

namespace RabbitEyeBankLibraryTests;

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
            fixture.CurrencyService.Dollar,
            fixture.CurrencyService.Dollar
        );

        fixture.MoneyTransferService.TransferMoney(transfer);
        Assert.Equal(0m, fixture.BankAccount1.Balance);
        Assert.Equal(300m, fixture.BankAccount2.Balance);
    }

    [Fact]
    public void TransferBetweenTwoDifferentCurrencies()
    {
        decimal oldBalance = fixture.BankAccount3.Balance;
        var transfer = fixture.MoneyTransferService.CreateTransfer(
            fixture.BankAccount1,
            fixture.BankAccount3,
            100m,
            fixture.CurrencyService.Dollar,
            fixture.ZorkMid
        );
        fixture.MoneyTransferService.TransferMoney(transfer);
        Assert.Equal(oldBalance + 100 / fixture.ZorkMid.DollarValue, fixture.BankAccount3.Balance);
    }
}
