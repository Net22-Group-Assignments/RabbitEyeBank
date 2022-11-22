using Spectre.Console;

namespace LoginDemo.UI.Windows;

public class CreateAccountWindow : IWindow
{
    public void Show()
    {
        AnsiConsole.Clear();
        WindowManager.showWindowStack();
        AnsiConsole.WriteLine($"Level {WindowManager.Level}");
        AnsiConsole.WriteLine("Customers account creation screen here:");
        AnsiConsole.WriteLine("Press a key to go back. In real app Customer would choose exit.");
        Console.ReadKey();
    }
}
