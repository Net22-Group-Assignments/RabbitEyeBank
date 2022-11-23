using Bogus.DataSets;
using RabbitEyeBank;
using RabbitEyeBank.Users;
using Spectre.Console;
using System.Collections.Generic;

namespace REB.UI;
//TODO Abracadabra logindemo to actual rabbiteyebank

public class AdminWindow : IWindow
{
    public void Show()
    {
        AnsiConsole.Clear();
        WindowManager.showWindowStack();
        AnsiConsole.WriteLine($"Level {WindowManager.Level}");
        string firstName;
        string lastName;
        string username;
        string password;
        bool usernameExists;
        bool successfulCreation;

        do
        {
            firstName = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter customers first name?").PromptStyle("green")
            );
            lastName = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter customers last name?").PromptStyle("green")
            );
            do
            {
                username = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter username?").PromptStyle("green")
                );
                usernameExists = BankServices.UserNameExists(username);
                if (usernameExists)
                {
                    AnsiConsole.MarkupLineInterpolated($"[blink]Username: {username} taken[/]");
                }
            } while (usernameExists);
            password = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter password?").PromptStyle("green")
            );

            string passwordCheck;
            do
            {
                passwordCheck = AnsiConsole.Prompt(
           new TextPrompt<string>("Enter password again?").PromptStyle("green")
           );
            } while (passwordCheck != password);




            //BankServices.AdminCreateUser(firstName, lastName, username, password, true);
            BankServices.AdminCreateUser(firstName, lastName, username, password);

            successfulCreation = true;
            Console.ReadKey();
        } while (successfulCreation == false);

    }
}
