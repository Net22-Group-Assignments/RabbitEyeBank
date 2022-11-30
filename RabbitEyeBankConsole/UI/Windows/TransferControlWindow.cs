using Spectre.Console;

namespace RabbitEyeBankConsole.UI.Windows;

public class TransferControlWindow : AdminHeader
{
    /// <inheritdoc />
    public override void Show()
    {
        base.Show();
        AnsiConsole.Write(Tables.TransferTable(MoneyTransferService.TransferLog));
        AnsiConsole.WriteLine("Press a key to go back");
        Console.ReadKey();
    }
}
