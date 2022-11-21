using Spectre.Console;

namespace LoginDemo.UI;

public class MoneyTransferWindow : IWindow
{
    public void Show()
    {
        AnsiConsole.Clear();
        WindowManager.showWindowStack();
        AnsiConsole.WriteLine($"Level {WindowManager.Level}");
        AnsiConsole.WriteLine("Customers money transaction screen here:");
        AnsiConsole.WriteLine(
            "Press a key to go back. In real app Customer would choose exit."
        );
        Console.ReadKey();
    }
}