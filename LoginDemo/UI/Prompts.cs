using RabbitEyeBank.Money;
using Spectre.Console;

namespace LoginDemo.UI;

public class Prompts
{
    /// <summary>
    /// Connects the choices with output-strings of your liking.
    /// The items must be lined up with the strings in the correct order.
    /// </summary>
    /// <typeparam name="TInput">The type of choices in the selection prompt.</typeparam>
    /// <param name="inputs">The choices</param>
    /// <param name="outputs">The strings output in the menu.</param>
    /// <returns></returns>
    public static Func<TInput, string> SelectionConverter<TInput>(
        IEnumerable<TInput> inputs,
        IEnumerable<string?> outputs
    ) where TInput : notnull
    {
        var inputOutput = inputs.Zip(
            outputs,
            (input, output) => new KeyValuePair<TInput, string?>(input, output)
        );
        var selectionStrings = new Dictionary<TInput, string?>(inputOutput);

        return (inputType => selectionStrings[inputType]);
    }

    /// <summary>
    /// Creates a selection prompt with bank-accounts
    /// </summary>
    /// <param name="Title">Prompt title</param>
    /// <param name="bankAccounts">Bank accounts to choose from.</param>
    /// <param name="disabledAccount">A bank-account hidden from choosing.</param>
    /// <returns></returns>
    public static SelectionPrompt<BankAccount> BankAccountSelector(
        string Title,
        IEnumerable<BankAccount> bankAccounts,
        BankAccount? disabledAccount = null
    )
    {
        var bankAccountList = bankAccounts.ToList();
        if (disabledAccount is not null)
        {
            bankAccountList.Remove(disabledAccount);
        }

        List<string?> accountNumbers = new();
        foreach (var bankAccount in bankAccountList)
        {
            accountNumbers.Add(bankAccount.AccountNumber);
        }
        var accountSelection = new SelectionPrompt<BankAccount>()
            .Title(Title)
            .AddChoices(bankAccountList);
        accountSelection.Converter = SelectionConverter(bankAccountList, accountNumbers);

        return accountSelection;
    }
}
