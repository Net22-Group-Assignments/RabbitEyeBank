namespace RabbitEyeBank.Money
{
    public class MoneyTransfer
    {
        private readonly Guid transferId;
        public BankAccount FromAccount { get; init; }
        public BankAccount ToAccount { get; init; }
        public DateTime TimeOfRegistration { get; private set; }
        public DateTime TimeOfCompletion { get; private set; }
        public decimal Amount { get; init; }
        public Currency ToCurrency { get; init; }
        public TransferStatus Status { get; private set; }

        public MoneyTransfer(BankAccount from, BankAccount to, decimal amount, Currency toCurrency)
        {
            transferId = Guid.NewGuid();
            FromAccount = from;
            ToAccount = to;
            Amount = amount;
            ToCurrency = toCurrency;
            Status = TransferStatus.New;
        }

        public void Register()
        {
            TimeOfRegistration = DateTime.Now;
            Status = TransferStatus.Pending;
        }

        public void Complete()
        {
            TimeOfCompletion = DateTime.Now;
            Status = TransferStatus.Completed;
        }

        protected bool Equals(MoneyTransfer other)
        {
            return transferId.Equals(other.transferId);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((MoneyTransfer)obj);
        }

        public override int GetHashCode()
        {
            return transferId.GetHashCode();
        }

        public static bool operator ==(MoneyTransfer? left, MoneyTransfer? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MoneyTransfer? left, MoneyTransfer? right)
        {
            return !Equals(left, right);
        }
    }

    public enum TransferStatus
    {
        New,
        Pending,
        Completed,
        Rejected
    }
}
