using Spectre.Console;

namespace RabbitEyeBankConsole.UI.Windows;

public class CreateAccountWindow : CustomerHeader
{
    public override void Show()
    {
        base.Show();
        AnsiConsole.WriteLine("Customers account creation screen here:");
        AnsiConsole.WriteLine("Press a key to go back. In real app Customer would choose exit.");
        Console.ReadKey();
    }
}
