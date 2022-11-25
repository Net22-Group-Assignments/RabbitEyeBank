using System.Collections.Concurrent;
using RabbitEyeBank.Money;
using Serilog;

namespace RabbitEyeBank.Services;

public class MoneyTransferService
{
    private readonly AccountService accountService;
    private readonly List<MoneyTransfer> TransferLog = new();
    private readonly ConcurrentQueue<MoneyTransfer> TransferQueue = new();

    public MoneyTransferService(AccountService accountService)
    {
        this.accountService = accountService;
    }

    public MoneyTransfer CreateTransfer(
        BankAccount fromAccount,
        BankAccount toAccount,
        decimal amount,
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

        return new MoneyTransfer(fromAccount, toAccount, amount, toCurrency);
    }

    public MoneyTransfer CreateTransfer(
        string fromAccountNumber,
        string toAccountNumber,
        decimal amount,
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

        return CreateTransfer(fromAccount, toAccount, amount, toCurrency);
    }

    public void RegisterTransfer(MoneyTransfer transfer)
    {
        transfer.Register();
        TransferQueue.Enqueue(transfer);
        Log.Debug(
            "Transfer from {FromAccount} to {ToAccount} in queue",
            transfer.FromAccount,
            transfer.ToAccount
        );
    }

    public void CompleteTransfer()
    {
        MoneyTransfer transfer;
        TransferQueue.TryDequeue(out transfer);
        if (transfer.Status == TransferStatus.Pending)
        {
            transfer.ToAccount.Deposit(transfer.Amount);
            transfer.Complete();
        }
        TransferLog.Add(transfer);
        Log.Debug(
            "Transfer from {FromAccount} to {ToAccount} completed with status {Status}",
            transfer.FromAccount,
            transfer.ToAccount,
            transfer.Status
        );
    }
}
