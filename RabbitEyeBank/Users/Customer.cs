using RabbitEyeBank.Money;

namespace RabbitEyeBank.Users
{
    /// TODO Should this be named customer instead? Or should this be user and customer inherits this + gets the bankaccount?
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
        public string Username { get; set; }
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

        public List<BankAccount> BankAccountList { get; } = new();

        public Customer(
            string firstName,
            string lastName,
            string username,
            string password,
            bool isActive
        )
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            IsActive = isActive;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Username)}: {Username}, {nameof(Password)}: {Password}, {nameof(IsActive)}: {IsActive}, {nameof(LoginAttempts)}: {LoginAttempts}, Bank accounts: {BankAccountList.Count}";
        }
    }
}
