using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitEyeBank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(UI.logotyp);
            Console.WriteLine("Hello and Welcome to RabbitEyeBank!");
            //AnsiConsole.WriteLine("I SAY LOTS OF STUFF"); //this is how you use spectre

            //Login();
            Console.ReadKey();
        }
    }
}
