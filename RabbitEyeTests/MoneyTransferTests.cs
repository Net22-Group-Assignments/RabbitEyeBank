using RabbitEyeBank.Money;
using RabbitEyeBank.Services;
using RabbitEyeBank.Users;

namespace RabbitEyeTests;

public class MoneyTransferTests
{
    private readonly Customer customer1;
    private readonly Customer customer2;
    private readonly BankAccount bankAccount1;
    private readonly BankAccount bankAccount2;
    private readonly BankAccount bankAccount3;

    public MoneyTransferTests()
    {
        customer1 = new Customer("Alice", "Allison", "alice", "alice", true);
        customer2 = new Customer("Bob", "Roberts", "bob", "bob", true);
        bankAccount1 = new BankAccount("1234", "savings", 100m, new Currency(), customer1);
        bankAccount2 = new BankAccount("5678", "loan", 200m, new Currency(), customer1);
        bankAccount3 = new BankAccount("9012", "slush-fund", 300m, new Currency(), customer2);
    }

    [Fact]
    public void TransferBetweenTwoOwnAccounts()
    {
        var accountService = AccountService.Instance;
        accountService.AddBankAccount(bankAccount1);
        accountService.AddBankAccount(bankAccount2);
        var moneyTransferService = MoneyTransferService.Instance;
        var transfer = moneyTransferService.CreateTransfer(
            bankAccount1,
            bankAccount2,
            100m,
            new Currency()
        );
        moneyTransferService.RegisterTransfer(transfer);
        moneyTransferService.CompleteTransfer();
        Assert.Equal(0m, bankAccount1.Balance);
        Assert.Equal(300m, bankAccount2.Balance);
    }
}
