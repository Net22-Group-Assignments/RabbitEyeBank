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
        public static List<Customer?> CustomerList { get; } = new();

        public static Customer? LoggedInCustomer;

        // TODO the login method should return a value to the caller so the ui can give the right message to the user.
        // TODO change return type to your response.
        public static string Login(string userName, string password)
        {
            if (userName == "admin" && password == "admin")
            {
                return "KNG"; // admin response code
            }

            // kolla om username existerar
            // om inte returnera felkod
            // om den finns:
            // Check password
            // if wrong, increase logintries++ return errormessage
            // if right login success.
            Customer? foundCustomer = GetCustomer(userName);

            if (foundCustomer == null)
            {
                return "SBS"; // username not found code.
            }

            if (foundCustomer.IsActive == false)
            {
                return "RIP"; // user deactivated code.
            }

            if (foundCustomer.Password != password)
            {
                foundCustomer.LoginAttempts++; // If loginattempts gets to 3 the customer is deactivated.
                if (foundCustomer.IsActive == false)
                {
                    return "RED"; // password error and past limit.
                }
                return "SBK"; // password error code.
            }

            // Reset after successful login
            foundCustomer.LoginAttempts = 0;
            LoggedInCustomer = foundCustomer;

            // return response code //
            return "ELK"; // successfull login code.
        }

        public static void LogOut()
        {
            if (LoggedInCustomer is null)
            {
                throw new InvalidOperationException("No logged in customer.");
            }

            LoggedInCustomer = null;
        }

        public static Customer? GetCustomer(string userName)
        {
            foreach (Customer? customer in CustomerList)
            {
                if (customer?.Username == userName)
                {
                    return customer;
                }
            }
            // If a customer not found, return null.
            return null;
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
