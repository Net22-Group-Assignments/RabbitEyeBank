using SixLabors.ImageSharp.Processing;
using Spectre.Console;

namespace RabbitEyeBankConsole.UI.Windows
{
    public abstract class CustomerHeader : Header, IWindow
    {
        /// <inheritdoc />
        public virtual void Show()
        {
            AnsiConsole.Profile.Width = 80;
            AnsiConsole.Profile.Height = 40;
            AnsiConsole.Clear();
#if DEVMODE
            {
                showWindowStack();
                AnsiConsole.WriteLine($"Level {Level}");
            }
#endif
            int width = AnsiConsole.Profile.Width;
            var image = new CanvasImage($"Assets{Path.AltDirectorySeparatorChar}bluebunny2xW.png");
            image.MaxWidth(80);
            image.PixelWidth(1);
            image.Mutate(ctx => ctx.Contrast(1.5f));
            AnsiConsole.Write(image);

            AnsiConsole.Write(
                new Rule("[bold orangered1]Rabbit-Eye Bank[/]")
                    .LeftAligned()
                    .RuleStyle(Style.Parse("orangered1"))
            );
        }
    }
}
