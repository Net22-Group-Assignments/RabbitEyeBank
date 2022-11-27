using Spectre.Console;

namespace LoginDemo.UI.Windows;

//TODO Abracadabra logindemo to actual rabbiteyebank

public class CreateCustomerWindow : CustomerHeader
{
    public override void Show()
    {
        base.Show();
        string firstName;
        string lastName;
        string username;
        string password;
        bool usernameExists;
        bool successfulCreation;
        bool again = true;

        while (again)
        {
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
                    usernameExists = UserService.UserNameExists(username);
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

                //UserService.AdminCreateUser(firstName, lastName, username, password, true);
                UserService.AdminCreateUser(firstName, lastName, username, password);
                successfulCreation = true;
            } while (successfulCreation == false);
            AnsiConsole.WriteLine("Customer successfully created.");
            again = AnsiConsole.Confirm("Create another customer?");
        }
    }
}
