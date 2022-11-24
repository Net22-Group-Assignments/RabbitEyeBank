using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitEyeBank.Money
{
    /// <summary>
    /// Represents a customer's bank account.
    /// </summary>
    public class BankAccount
    {
        public string? AccountNumber { get; init; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public Currency Currency { get; set; }

        public BankAccount(string? accountNumber, string name, decimal balance, Currency currency)
        {
            AccountNumber = accountNumber;
            Name = name;
            Balance = balance;
            Currency = currency;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                throw new InvalidOperationException("Not enough money in the bankaccount.");
            }

            Balance -= amount;
        }
    }
}
