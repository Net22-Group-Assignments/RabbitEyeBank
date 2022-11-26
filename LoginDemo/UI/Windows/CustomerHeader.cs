using RabbitEyeBank.Services;
using Spectre.Console;

namespace LoginDemo.UI.Windows
{
    public abstract class CustomerHeader : IWindow
    {
        protected UserService UserService;
        protected AccountService accountService;
        protected MoneyTransferService moneyTransferService;

        private readonly Grid grid;

        protected CustomerHeader(
            UserService userService,
            AccountService accountService,
            MoneyTransferService moneyTransferService
        )
        {
            this.UserService = userService;
            this.accountService = accountService;
            this.moneyTransferService = moneyTransferService;

            grid = new Grid();
            grid.AddColumns(2);
            grid.AddRow(
                Markup.FromInterpolated(
                    $@"&&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%/(&@@@@@@@@@@@@@@@@@@%/*%%%@@@@@@@@@@@@@@@@@@@
&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@(###/*/#&@@@@@@@@@@((*/%%##%@@@@@@@@@@@@@@@@@@@
&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@##(//#(,,*%@@@@@@(,,*(%#####@@@@@@@@@@@@@@@@@@@
&&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%((///(/,.,*&@@&(*,*,##(###%@@@@@@@@@@@@@@@@@@@
&&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&((//((/*(%&&&&&&&&%/(#(#(#%@@@@@@@@@@@@@@@@@@@
&&@&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%(/*(%&&&&@@@@@@@@@@&&%##%@@@@@@@@@@@@@@@@@@@@
&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@%%&&&@@@@@@@@@@@@@@@@@@&&@@@@@@@@@@@@@@@@@@@@
&&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&&&%#&@@@@@@@@@@@@@&&&@@@@@@@@@@@@@@@@@@@@@@
&&&@&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&&%//(#&&@@@@@@@@@@&&#&@@@@@@@@@@@@@@@@@@@@@@
&&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&%/*,.(%&&&&&&&&&&&&&#/(&@@@@@@@@@@@@@@@@@@@@@
&&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@@&%#(#*.,#%&&&&&&&&&&&#,/###&@@@@@@@@@@@@@@@@@@@
&&&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@&&&&&%%%&&&&&&&&&&@@@&&&&&%%&@@@@@@@@@@@@@@@@@@
&&&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@@&&&&&&&&&%%%###%%%&&@@@@@@@@@@@@@@@@@@@@@@@@@@@
&&&&&&@@@@@@@@@@@@@@@@@@@@@@@@&@@&&&&&&&&&&&&%(#%(%&&&@&@@@@@@@@@@@@@@@@@@@@@@@@
&&&&&&&@@@@@@@@@@@@@@@@@@@@@&&&@&&&&&&&&&&&&&&%##%&&&@@@@@@@@@@@@@@@@@@@@@@@@@@@
&&&&&&&@@@@@@@@@@@@@@@@@@@@&&&&&&&&&&&&&&&&&&&&&&&&&&&&@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&@@@@@@@@@@@@@@@@@@@@@@@
"
                ),
                Markup.FromInterpolated($"{DateTime.Now}")
            );
        }

        protected CustomerHeader()
            : this(
                ServiceContainer.UserService,
                ServiceContainer.accountService,
                ServiceContainer.MoneyTransferService
            ) { }

        /// <inheritdoc />
        public virtual void Show()
        {
            AnsiConsole.Clear();
            showWindowStack();
            AnsiConsole.WriteLine($"Level {Level}");
            //AnsiConsole.Write(grid);
            AnsiConsole.Write(new Rule());
        }
    }
}
