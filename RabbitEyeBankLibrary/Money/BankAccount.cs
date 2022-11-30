using RabbitEyeBankLibrary.Users;

namespace RabbitEyeBankLibrary.Money
{
    /// <summary>
    /// Represents a customer's bank account.
    /// </summary>
    public class BankAccount
    {
        private int transfersInQueue;

        public string? AccountNumber { get; init; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public Currency Currency { get; set; }

        public Customer Owner { get; init; }
        public int TransfersInQueue
        {
            get => transfersInQueue;
            set => transfersInQueue = value < 0 ? 0 : value;
        }

        public BankAccount(
            string? accountNumber,
            string name,
            decimal balance,
            Currency currency,
            Customer owner
        )
        {
            AccountNumber = accountNumber;
            Name = name;
            Balance = balance;
            Currency = currency;
            Owner = owner;
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

        protected bool Equals(BankAccount other)
        {
            return AccountNumber == other.AccountNumber;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((BankAccount)obj);
        }

        public override int GetHashCode()
        {
            return (AccountNumber != null ? AccountNumber.GetHashCode() : 0);
        }

        public static bool operator ==(BankAccount? left, BankAccount? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BankAccount? left, BankAccount? right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"{nameof(AccountNumber)}: {AccountNumber}, {nameof(Balance)}: {Balance} {Currency.Symbol}";
        }
    }
}
