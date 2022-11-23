using Spectre.Console;

namespace LoginDemo.UI.Windows;

public class CreateAccountWindow : CustomerWindow
{
    public override void Show()
    {
        base.Show();
        AnsiConsole.WriteLine("Customers account creation screen here:");
        AnsiConsole.WriteLine("Press a key to go back. In real app Customer would choose exit.");
        Console.ReadKey();
    }
}
