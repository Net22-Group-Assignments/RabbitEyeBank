using RabbitEyeBankLibrary.Services;

namespace RabbitEyeBankLibraryTests;

public class MoneyTransferTests : IClassFixture<Fixture>
{
    private Fixture fixture;

    public MoneyTransferTests(Fixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void TransferBetweenTwoOwnAccounts()
    {
        var userService = fixture.UserService;
        var accountService = new AccountService();
        var currencyService = new CurrencyService();

        var transferService = new MoneyTransferService(
            userService,
            accountService,
            currencyService
        );
        transferService.TransferTimeSpan = TimeSpan.Zero;
        var b1 = fixture.BankAccount1();
        var b2 = fixture.BankAccount2();
        accountService.AddBankAccount(b1);
        accountService.AddBankAccount(b2);

        var transfer = transferService.CreateTransfer(b1, b2, 100m, fixture.Dollar, fixture.Dollar);

        transferService.TransferMoney(transfer);
        Assert.Equal(0m, b1.Balance);
        Assert.Equal(300m, b2.Balance);
    }

    [Fact]
    public void TransferBetweenTwoDifferentCurrencies()
    {
        var userService = fixture.UserService;
        var accountService = new AccountService();
        var currencyService = new CurrencyService();

        var transferService = new MoneyTransferService(
            userService,
            accountService,
            currencyService
        );
        transferService.TransferTimeSpan = TimeSpan.Zero;
        var b1 = fixture.BankAccount1();
        var b3 = fixture.BankAccount3();
        accountService.AddBankAccount(b1);
        accountService.AddBankAccount(b3);

        decimal oldBalance = b3.Balance;
        var transfer = transferService.CreateTransfer(
            b1,
            b3,
            100m,
            fixture.Dollar,
            fixture.ZorkMid
        );
        transferService.TransferMoney(transfer);
        Assert.Equal(oldBalance + 100 / fixture.ZorkMid.DollarValue, b3.Balance);
    }
}
