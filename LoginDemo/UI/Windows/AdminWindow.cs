using Spectre.Console;

namespace LoginDemo.UI.Windows;

public class AdminWindow : CustomerHeader
{
    /// <inheritdoc />
    public override void Show()
    {
        base.Show();
        var bankDataTable = new Table();
        bankDataTable
            .Title("Overview")
            .AddColumns("Total Customers", "Total Accounts", "Total Transactions")
            .AddRow(
                Markup.FromInterpolated($"{UserService.CustomerList.Count}"),
                Markup.FromInterpolated($"{AccountService.AccountList.Count}"),
                Markup.FromInterpolated($"{MoneyTransferService.TransferLog}")
            ); // TODO add total balance in bank? (bonus)
    }
}
