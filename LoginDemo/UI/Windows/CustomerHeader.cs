using RabbitEyeBank.Services;
using REB.UI;
using Spectre.Console;

namespace LoginDemo.UI.Windows
{
    public abstract class CustomerHeader : IWindow
    {
        protected BankService bankService;
        protected AccountService accountService;
        protected MoneyTransferService moneyTransferService;

        private readonly Grid grid;

        protected CustomerHeader(
            BankService bankService,
            AccountService accountService,
            MoneyTransferService moneyTransferService
        )
        {
            this.bankService = bankService;
            this.accountService = accountService;
            this.moneyTransferService = moneyTransferService;

            grid = new Grid();
            grid.AddColumns(2);
            grid.AddRow(
                new FigletText("R.E.B").LeftAligned().Color(Color.Green),
                Markup.FromInterpolated($"{DateOnly.FromDateTime(DateTime.Now)}")
            );
        }

        protected CustomerHeader()
            : this(
                ServiceContainer.bankService,
                ServiceContainer.accountService,
                ServiceContainer.MoneyTransferService
            ) { }

        /// <inheritdoc />
        public virtual void Show()
        {
            AnsiConsole.Clear();
            WindowManager.showWindowStack();
            AnsiConsole.WriteLine($"Level {WindowManager.Level}");
            AnsiConsole.Write(grid);
            AnsiConsole.Write(new Rule());
        }
    }
}
