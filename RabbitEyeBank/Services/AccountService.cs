using RabbitEyeBank.Money;
using RabbitEyeBank.Users;

namespace RabbitEyeBank.Services;

public static class AccountService
{
    private static readonly List<BankAccount> accountList = new();
    public static IReadOnlyList<BankAccount> AccountList => accountList;

    public static IReadOnlyList<BankAccount> BankAccountsByCustomer(Customer customer)
    {
        return accountList.FindAll(account => account.Owner == customer);
    }

    public static BankAccount? BankAccountByAccountNumber(string accountNumber)
    {
        return accountList.Find(acc => acc.AccountNumber == accountNumber);
    }

    public static void AddBankAccount(BankAccount bankAccount)
    {
        if (accountList.Contains(bankAccount))
        {
            throw new InvalidOperationException("Duplicate bankaccount");
        }

        if (bankAccount.Owner is null)
        {
            throw new ArgumentException("Bankaccount must have owner");
        }
        accountList.Add(bankAccount);
    }
}
