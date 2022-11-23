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
        public string AccountNumber { get; init; }
        public string Name { get; set; }
        public decimal AccBalance { get; set; }
        public Currency Currency { get; set; }

        public BankAccount(string accountNumber, string name, decimal accBalance, Currency currency)
        {
            AccountNumber = accountNumber;
            Name = name;
            AccBalance = accBalance;
            Currency = currency;
        }
    }
}
