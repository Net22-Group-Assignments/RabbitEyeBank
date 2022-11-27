using RabbitEyeBank.Services;

namespace RabbitEyeTests;

public class AccountServiceTests
{
    readonly AccountService accountService;

    public AccountServiceTests()
    {
        accountService = new AccountService();
    }

    [Fact]
    public void BankAccountExists_ReturnsFalseWhenBankAccountNotAvailable()
    {
        Assert.False(accountService.BankAccountExists("00000000"));
    }
}
