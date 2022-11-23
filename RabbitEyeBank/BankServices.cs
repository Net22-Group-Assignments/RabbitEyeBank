using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitEyeBank.Users;
using Spectre.Console;

namespace RabbitEyeBank
{
    public static class BankServices
    {
        /// <summary>
        /// Stores all users/customers in the bank.
        /// </summary>
        public static List<Customer?> CustomerList { get; } = new();

        public static Customer? LoggedInCustomer;

        private static bool adminMode = false;

        public static string Login(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                adminMode = true;
                return "KNG"; // admin response code
            }

            // kolla om username existerar
            // om inte returnera felkod
            // om den finns:
            // Check password
            // if wrong, increase logintries++ return errormessage
            // if right login success.
            Customer? foundCustomer = GetCustomer(username);

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

        public static Customer? GetCustomer(string username)
        {
            foreach (Customer? customer in CustomerList)
            {
                if (customer?.Username == username)
                {
                    return customer;
                }
            }
            // If a customer not found, return null.
            return null;
        }

        public static bool UserNameExists(string username)
        {
            if (username == "admin")
            {
                return true;
            }

            foreach (Customer customer in CustomerList)
            {
                if (customer.Username == username.ToLower())
                {
                    Console.WriteLine("That username is already taken");
                    return true;
                }
            }

            // check if username already exists.
            return false;  //false is placeholder
        }

        public static void AdminCreateUser(string firstName, string lastName,string username,string password)
        {
            Customer customer = new Customer(firstName, lastName, username.ToLower(), password, true);

            if (UserNameExists(username))
            {
                throw new Exception("Username already exists");
            }
            CustomerList.Add(customer);
            CustomerList.ForEach(c => AnsiConsole.WriteLine(c.ToString()));
        }
        
            // Create a user and save it.
        
    }
}
