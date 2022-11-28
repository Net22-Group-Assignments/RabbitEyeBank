using System.Collections.Concurrent;
using RabbitEyeBankLibrary.Money;
using RabbitEyeBankLibrary.Users;
using Serilog;

namespace RabbitEyeBankLibrary.Services;

public class MoneyTransferService
{
    private readonly UserService userService;
    private readonly AccountService accountService;
    private readonly List<MoneyTransfer> transferLog = new();
    private readonly ConcurrentQueue<MoneyTransfer> TransferQueue = new();

    public MoneyTransferService(UserService userService, AccountService accountService)
    {
        this.userService = userService;
        this.accountService = accountService;
    }

    public IReadOnlyList<MoneyTransfer> TransferLog => transferLog;

    public IReadOnlyList<MoneyTransfer> TransfersByAccount(BankAccount bankAccount)
    {
        if (bankAccount == null)
            throw new ArgumentNullException(nameof(bankAccount));

        if (accountService.BankAccountExists(bankAccount) == false)
        {
            throw new ArgumentException("Bank account does not exist", nameof(bankAccount));
        }

        return transferLog.FindAll(
            transfer =>
                transfer.FromAccount.Equals(bankAccount) || transfer.ToAccount.Equals(bankAccount)
        );
    }

    public IReadOnlyList<MoneyTransfer> TransfersByCustomer(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));
        if (userService.CustomerExists(customer) == false)
        {
            throw new ArgumentException("Customer does not exist", nameof(customer));
        }

        return transferLog.FindAll(
            transfer =>
                transfer.FromAccount.Owner.Equals(customer)
                || transfer.ToAccount.Owner.Equals(customer)
        );
    }

    public MoneyTransfer CreateTransfer(
        BankAccount fromAccount,
        BankAccount toAccount,
        decimal amount,
        Currency fromCurrency,
        Currency toCurrency
    )
    {
        if (accountService.AccountList.Contains(fromAccount) == false)
        {
            throw new ArgumentException("Originator account does not exist", nameof(fromAccount));
        }

        if (accountService.AccountList.Contains(toAccount) == false)
        {
            throw new ArgumentException("Destination account does not exist", nameof(toAccount));
        }

        if (fromAccount == toAccount)
        {
            throw new ArgumentException(
                "Origin and destination account are the same",
                $"{nameof(fromAccount)}, {nameof(toAccount)}"
            );
        }

        try
        {
            fromAccount.Withdraw(amount);
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException(
                "Insufficient funds in originator account",
                nameof(amount),
                e
            );
        }

        return new MoneyTransfer(fromAccount, toAccount, amount, fromCurrency, toCurrency);
    }

    public MoneyTransfer CreateTransfer(
        string fromAccountNumber,
        string toAccountNumber,
        decimal amount,
        Currency fromCurrency,
        Currency toCurrency
    )
    {
        var fromAccount =
            accountService.BankAccountByAccountNumber(fromAccountNumber)
            ?? throw new ArgumentException(
                "Originator bank account number does not exist",
                nameof(fromAccountNumber)
            );
        var toAccount =
            accountService.BankAccountByAccountNumber(toAccountNumber)
            ?? throw new ArgumentException(
                "Destination bank account number does not exist",
                nameof(toAccountNumber)
            );

        return CreateTransfer(fromAccount, toAccount, amount, fromCurrency, toCurrency);
    }

    public void RegisterTransfer(MoneyTransfer transfer)
    {
        transfer.Register();
        TransferQueue.Enqueue(transfer);
        transferLog.Add(transfer);
        Log.Debug(
            "Transfer from {FromAccount} to {ToAccount} in queue",
            transfer.FromAccount,
            transfer.ToAccount
        );
    }

    public void CompleteTransfer()
    {
        MoneyTransfer transfer;
        if (TransferQueue.TryDequeue(out transfer))
        {
            if (transfer.Status == TransferStatus.Pending)
            {
                transfer.ToAccount.Deposit(transfer.Amount);
                transfer.Complete();
            }
        }
        else
        {
            transfer.FromAccount.Deposit(transfer.Amount);
            transfer.Reject();
        }
        Log.Debug(
            "Transfer from {FromAccount} to {ToAccount} completed with status {Status}",
            transfer.FromAccount,
            transfer.ToAccount,
            transfer.Status
        );
    }

    public void TransferMoney(MoneyTransfer moneyTransfer)
    {
        RegisterTransfer(moneyTransfer);
        CompleteTransfer();
    }
}
