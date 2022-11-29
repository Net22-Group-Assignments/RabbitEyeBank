using System.Collections.Concurrent;
using RabbitEyeBankLibrary.Money;
using RabbitEyeBankLibrary.Users;
using Serilog;

namespace RabbitEyeBankLibrary.Services;

/// <summary>
/// Manages the transferring of money. New Transfers are put on a queue and
/// the transfer requests are handled in line.
/// </summary>
public class MoneyTransferService
{
    private readonly UserService userService;
    private readonly AccountService accountService;
    private readonly CurrencyService currencyService;
    private readonly List<MoneyTransfer> transferLog = new();
    private readonly ConcurrentQueue<MoneyTransfer> TransferQueue = new();

    public MoneyTransferService(
        UserService userService,
        AccountService accountService,
        CurrencyService currencyService
    )
    {
        this.userService = userService;
        this.accountService = accountService;
        this.currencyService = currencyService;
    }

    public IReadOnlyList<MoneyTransfer> TransferLog => transferLog;

    /// <summary>
    /// Fetches all transfers associated with a specific account.
    /// </summary>
    /// <param name="bankAccount">bank account object to search against.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">if bank account parameter is null.</exception>
    /// <exception cref="ArgumentException">if the provide bank account is not present.</exception>
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

    /// <summary>
    /// Fetches all transfers associated with a specific customer.
    /// </summary>
    /// <param name="customer">customer object to search against.</param>
    /// <returns>Read only list of transfers.</returns>
    /// <exception cref="ArgumentNullException">if customer parameter is null</exception>
    /// <exception cref="ArgumentException">if the provided customer is not present.</exception>
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

    /// <summary>
    /// Creates a new transfer object out of parameters.
    /// </summary>
    /// <param name="fromAccount">sender account.</param>
    /// <param name="toAccount">receiver account</param>
    /// <param name="amount">amount of money.</param>
    /// <param name="fromCurrency">sender accounts currency.</param>
    /// <param name="toCurrency">receiver accounts currency.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
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

    /// <summary>
    /// Timestamps and enters the transfer into the queue.
    /// </summary>
    /// <param name="transfer">transfer object to process.</param>
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

    /// <summary>
    /// Tries to fulfill the transfer first in the transfer queue.
    /// If accepted converts the value if needed and deposits the value
    /// in receivers bank account.
    /// If it fails, the amount is returned to senders account.
    /// </summary>
    public void CompleteTransfer()
    {
        MoneyTransfer transfer;
        if (TransferQueue.TryDequeue(out transfer))
        {
            if (transfer.Status == TransferStatus.Pending)
            {
                if (transfer.FromCurrency != transfer.ToCurrency)
                {
                    decimal convertedAmount = currencyService.ConvertCurrency(
                        transfer.FromCurrency,
                        transfer.ToCurrency,
                        transfer.Amount
                    );
                    transfer.ToAccount.Deposit(convertedAmount);
                }
                else
                {
                    transfer.ToAccount.Deposit(transfer.Amount);
                }

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
