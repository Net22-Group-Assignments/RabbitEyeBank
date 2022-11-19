using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitEyeBank.Users;

namespace RabbitEyeBank
{
    public static class BankServices
    {
        /// <summary>
        /// Stores all users/customers in the bank.
        /// </summary>
        public static List<Customer> UserList { get; } = new();

        // TODO the login method should return a value to the caller so the ui can give the right message to the user.
        // TODO change return type to your response.
        public static void Login(string userName = "admin", string password = "password")
        {
            // Ok to use console writeline when debug.
            //kontrollera mot variabel
            //om fel, avbryt
            //om rätt, fråga om lösen

            //skapa List string username
            // return response code //
        }

        // Just a suggestion.
        //public static void AdminCreateUser()
        //{
        //    if (CurrentUser.IsAdmin == false)
        //    {
        //        throw new Exception()
        //    }
        //}
    }
}
