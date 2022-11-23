using Spectre.Console;

namespace LoginDemo.UI.Windows;

public class AdminWindow : IWindow
{
    public void Show()
    {
        AnsiConsole.Clear();
        WindowManager.showWindowStack();
        AnsiConsole.WriteLine($"Level {WindowManager.Level}");
        AnsiConsole.WriteLine("Customer creation screen here:");
        AnsiConsole.WriteLine("Press a key to go back. In real app Admin would choose exit.");
        Console.ReadKey();
    }
}
