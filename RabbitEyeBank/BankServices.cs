﻿using System;
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
        public static List<User> UserList { get; } = new();

        // TODO the login method should return a value to the caller so the ui can give the right message to the user.
        public static void Login(string userName = "admin", string password = "password")
        {
            //kontrollera mot variabel
            //om fel, avbryt
            //om rätt, fråga om lösen

            //skapa List string username
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
