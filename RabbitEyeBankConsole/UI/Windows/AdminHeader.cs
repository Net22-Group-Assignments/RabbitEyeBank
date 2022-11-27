using RabbitEyeBank.Services;
using SixLabors.ImageSharp.Processing;
using Spectre.Console;

namespace LoginDemo.UI.Windows
{
    public abstract class AdminHeader : Header, IWindow
    {
        /// <inheritdoc />
        public virtual void Show()
        {
            //Grid grid = new Grid();
            //grid.AddColumns(2);
            AnsiConsole.Profile.Width = 70;
            AnsiConsole.Clear();
            int width = AnsiConsole.Profile.Width;
            showWindowStack();
            AnsiConsole.WriteLine($"Level {Level}");
            var image = new CanvasImage(
                $"Assets{Path.AltDirectorySeparatorChar}Greenbunny blue eye.png"
            );
            image.MaxWidth(70);
            image.PixelWidth(1);
            image.Mutate(ctx => ctx.Contrast(1.5f));
            AnsiConsole.Write(image);

            //grid.AddRow(new FigletText("RabbitEye Bank").LeftAligned().Color(Color.Purple), image);
            //grid.Alignment(Justify.Left);
            //AnsiConsole.Write(grid);
            AnsiConsole.Write(
                new Rule("[green]Rabbit-Eye Bank[/]").LeftAligned().RuleStyle(Style.Parse("green"))
            );
        }
    }
}
