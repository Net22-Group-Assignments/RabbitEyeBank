using RabbitEyeBank.Money;
using RabbitEyeBank.Users;

namespace RabbitEyeBank.Services;

public class AccountService
{
    private readonly List<BankAccount> accountList = new();

    public IReadOnlyList<BankAccount> AccountList => accountList;

    public AccountService() { }

    public IReadOnlyList<BankAccount> BankAccountsByCustomer(Customer customer)
    {
        return accountList.FindAll(account => account.Owner == customer);
    }

    public BankAccount? BankAccountByAccountNumber(string accountNumber)
    {
        return accountList.Find(acc => acc.AccountNumber == accountNumber);
    }

    public void AddBankAccount(BankAccount bankAccount)
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

    public void DeleteAllBankAccounts()
    {
        accountList.Clear();
    }
}
