using RabbitEyeBank.Money;
using RabbitEyeBank.Services;
using RabbitEyeBank.Users;

namespace RabbitEyeTests;

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
        Assert.Throws<InvalidOperationException>(() => fixture.BankAccount1.Withdraw(101m));
    }

    [Fact]
    public void CustomersBankAccountPropertyShouldFindOwnAccounts()
    {
        IReadOnlyList<BankAccount> accounts = fixture.AccountService.BankAccountsByCustomer(
            fixture.Customer1
        );
        Assert.Equal(new[] { fixture.BankAccount1, fixture.BankAccount2 }, accounts);
    }
}
