using System.Collections.Concurrent;
using RabbitEyeBank.Money;
using Serilog;

namespace RabbitEyeBank.Services;

public static class MoneyTransferService
{
    private static readonly ConcurrentQueue<MoneyTransfer> TransferQueue = new();
    private static readonly List<MoneyTransfer> TransferLog = new();

    public static MoneyTransfer CreateTransfer(
        BankAccount fromAccount,
        BankAccount toAccount,
        decimal amount,
        Currency toCurrency
    )
    {
        if (AccountService.AccountList.Contains(fromAccount) == false)
        {
            throw new ArgumentException("Originator account does not exist", nameof(fromAccount));
        }

        if (AccountService.AccountList.Contains(toAccount) == false)
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

    public static MoneyTransfer CreateTransfer(
        string fromAccountNumber,
        string toAccountNumber,
        decimal amount,
        Currency toCurrency
    )
    {
        var fromAccount =
            AccountService.BankAccountByAccountNumber(fromAccountNumber)
            ?? throw new ArgumentException(
                "Originator bank account number does not exist",
                nameof(fromAccountNumber)
            );
        var toAccount =
            AccountService.BankAccountByAccountNumber(toAccountNumber)
            ?? throw new ArgumentException(
                "Destination bank account number does not exist",
                nameof(toAccountNumber)
            );

        return CreateTransfer(fromAccount, toAccount, amount, toCurrency);
    }

    public static void RegisterTransfer(MoneyTransfer transfer)
    {
        transfer.Register();
        TransferQueue.Enqueue(transfer);
        Log.Debug(
            "Transfer from {FromAccount} to {ToAccount} in queue",
            transfer.FromAccount,
            transfer.ToAccount
        );
    }

    public static void CompleteTransfer()
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
