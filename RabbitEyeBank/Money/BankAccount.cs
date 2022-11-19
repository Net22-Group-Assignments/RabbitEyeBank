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
        public decimal AccBalance { get; set; }
        public Currency Currency { get; set; }

        public BankAccount(decimal accBalance, Currency currency)
        {
            AccBalance = accBalance;
            Currency = currency;
        }
    }
}
