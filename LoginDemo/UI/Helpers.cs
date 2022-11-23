namespace LoginDemo.UI;

public class Helpers
{
    public static Func<TInput, string> SelectionConverter<TInput>(TInput[] inputs, string[] outputs)
        where TInput : notnull
    {
        Dictionary<TInput, string> selectionStrings;
        var inputOutput = inputs.Zip(
            outputs,
            (input, output) => new KeyValuePair<TInput, string>(input, output)
        );
        selectionStrings = new Dictionary<TInput, string>(inputOutput);

        return (inputType => selectionStrings[inputType]);
    }
}
