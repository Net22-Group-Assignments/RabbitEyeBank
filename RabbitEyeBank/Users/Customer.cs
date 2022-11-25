using RabbitEyeBank.Money;
using RabbitEyeBank.Services;
using RabbitEyeBank.Shared;

namespace RabbitEyeBank.Users
{
    /// <summary>
    /// Represents a user/customer.
    /// </summary>
    public class Customer
    {
        //private string id; //variable camelCase
        private int loginAttempts;

        //public Guid  { get; set; }
        public string FirstName { get; set; } // property PascalCase
        public string LastName { get; set; }
        public string Username { get; init; }
        public string Password { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// Can make the user inactive if failed.
        /// Usually used with incrementation.
        /// </summary>
        public int LoginAttempts
        {
            get => loginAttempts;
            set
            {
                loginAttempts = value;
                if (loginAttempts >= 3)
                {
                    IsActive = false;
                }
            }
        }

        //public IReadOnlyList<BankAccount> BankAccountList => adapter.BankAccountsByCustomer(this);

        public Customer(
            string firstName,
            string lastName,
            string username,
            string password,
            bool isActive = false
        )
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            IsActive = isActive;
        }

        protected bool Equals(Customer other)
        {
            return Username == other.Username;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Customer)obj);
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode();
        }

        public static bool operator ==(Customer? left, Customer? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Customer? left, Customer? right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Username)}: {Username}, {nameof(Password)}: {Password}, {nameof(IsActive)}: {IsActive}, {nameof(LoginAttempts)}: {LoginAttempts}, Bank accounts: BankAccountList.Count";
        }
    }
}
