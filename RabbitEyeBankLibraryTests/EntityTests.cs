using RabbitEyeBankLibrary.Money;
using RabbitEyeBankLibrary.Services;

namespace RabbitEyeBankLibraryTests;

public class EntityTests : IClassFixture<Fixture>
{
    private readonly Fixture fixture;

    public EntityTests(Fixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public void WithdrawalWhenAmountTooLittle_ThrowsException()
    {
        Assert.Throws<InvalidOperationException>(() => fixture.BankAccount1().Withdraw(101m));
    }

    [Fact]
    public void CustomersBankAccountPropertyShouldFindOwnAccounts()
    {
        var accountService = new AccountService();
        var b1 = fixture.BankAccount1();
        var b2 = fixture.BankAccount2();
        accountService.AddBankAccount(b1);
        accountService.AddBankAccount(b2);

        IReadOnlyList<BankAccount> accounts = accountService.BankAccountsByCustomer(
            fixture.Customer1
        );
        Assert.Equal(new[] { b1, b2 }, accounts);
    }
}
