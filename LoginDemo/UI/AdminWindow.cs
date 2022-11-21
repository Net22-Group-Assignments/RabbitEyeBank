using Spectre.Console;

namespace LoginDemo.UI;

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

        do
        {
            firstName = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter customers first name?").PromptStyle("green")
            );

            lastName = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter password?").PromptStyle("green")
            );

            do
            {
                password = AnsiConsole.Prompt(
                    new TextPrompt<string>("Enter password?").PromptStyle("green")
                );
            } while (true);
        } while (true);
        Console.ReadKey();
    }
}
