namespace RabbitEyeBank.Users
{
    internal class User
    {
        //private string id; //variable camelCase
        private int loginAttempts;

        public string FirstName { get; set; } // property PascalCase
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsActive { get; set; }
        public int LoginAttempts
        {
            get { return loginAttempts; }
            set
            {
                loginAttempts++;
                if (loginAttempts >= 3)
                {
                    IsActive = false;
                }
            }

            //Can make the user inactive if failed.



            //public Guid  { get; set; }

            // public List<BankAccount> bankAccountList;
        }
    }
}
