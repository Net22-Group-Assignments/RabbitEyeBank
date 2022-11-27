using System.Globalization;
using RabbitEyeBank;
using RabbitEyeBank.Money;
using Spectre.Console;

namespace LoginDemo.UI.Windows;

public class CurrencyWindow : AdminHeader
{
    /// <inheritdoc />
    public override void Show()
    {
        while (true)
        {
            base.Show();

            var currencyExchangeTable = new Table();
            currencyExchangeTable
                .Title("Currency Exchange")
                .BorderColor(Color.Green)
                .RoundedBorder()
                .AddColumns(
                    new TableColumn("Curency ISO"),
                    new TableColumn("Symbol"),
                    new TableColumn("USD $ Value")
                );
            List<Currency> currencyList = CurrencyService.CurrencyList.ToList();
            List<CurrencyISO> isoList = CurrencyService.CurrencyISOList.ToList();

            foreach (var currency in currencyList)
            {
                currencyExchangeTable.AddRow(
                    new Markup(currency.CurrencyISO.ToString()),
                    new Markup(currency.Symbol),
                    new Markup(currency.DollarValue.ToString(CultureInfo.InvariantCulture))
                );
            }
            AnsiConsole.Write(currencyExchangeTable);

            if (AnsiConsole.Confirm("Edit currencies?") == false)
            {
                return;
            }

            var currenciesToEdit = AnsiConsole.Prompt(
                new MultiSelectionPrompt<Currency>()
                    .Title("Choose currencies to edit:")
                    .NotRequired()
                    .PageSize(10)
                    .MoreChoicesText("[green]Move up and down to see more[/]")
                    .InstructionsText(
                        "[green](Press <space> to toggle a currency, " + "<enter> to accept)[/]"
                    )
                    .AddChoices(currencyList)
                    .HighlightStyle(Style.Parse("green"))
                    .UseConverter(
                        Prompts.SelectionConverter(
                            currencyList,
                            isoList.Select(iso => iso.ToString())
                        )
                    )
            );

            foreach (var currency in currenciesToEdit)
            {
                decimal newValue = AnsiConsole.Prompt(
                    new TextPrompt<decimal>($"[green]{currency.CurrencyISO} Enter new $ value:[/]")
                        .DefaultValue(currency.DollarValue)
                        .Validate(
                            value =>
                                value <= 0
                                    ? ValidationResult.Error("[green]Enter a positive value[/]")
                                    : ValidationResult.Success()
                        )
                );
                CurrencyService.EditCurrency(currency.CurrencyISO, newValue);
            }
        }
    }
}
