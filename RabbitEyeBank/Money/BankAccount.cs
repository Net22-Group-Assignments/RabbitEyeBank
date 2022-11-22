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
        public string Name { get; set; }
        public decimal AccBalance { get; set; }
        public Currency Currency { get; set; }

        public BankAccount(string name, decimal accBalance, Currency currency)
        {
            Name = name;
            AccBalance = accBalance;
            Currency = currency;
        }
    }
}
