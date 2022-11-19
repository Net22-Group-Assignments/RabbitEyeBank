using RabbitEyeBank.Money;

namespace RabbitEyeBank.Users
{
    /// TODO Should this be named customer instead? Or should this be user and customer inherits this + gets the bankaccount?
    /// <summary>
    /// Represents a user/customer.
    /// </summary>
    public class User
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

        public List<BankAccount> BankAccountList = new();
    }
}
