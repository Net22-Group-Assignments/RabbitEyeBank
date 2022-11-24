using System.Collections.Concurrent;
using RabbitEyeBank.Money;

namespace RabbitEyeBank.Services;

public static class MoneyTransferService
{
    private static readonly ConcurrentQueue<MoneyTransfer> TransferQueue = new();
    private static readonly List<MoneyTransfer> TransferLog = new();

    public static void RegisterTransfer(MoneyTransfer transfer)
    {
        transfer.Register();
        TransferQueue.Enqueue(transfer);
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
    }
}
