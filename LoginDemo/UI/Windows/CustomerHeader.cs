using REB.UI;
using Spectre.Console;

namespace LoginDemo.UI.Windows
{
    public abstract class CustomerHeader : IWindow
    {
        private readonly Grid grid;

        protected CustomerHeader()
        {
            grid = new Grid();
            grid.AddColumns(2);
            grid.AddRow(
                new FigletText("R.E.B").LeftAligned().Color(Color.Green),
                Markup.FromInterpolated($"{DateOnly.FromDateTime(DateTime.Now)}")
            );
        }

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
