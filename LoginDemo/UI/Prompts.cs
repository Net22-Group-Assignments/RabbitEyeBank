using RabbitEyeBank.Money;
using Spectre.Console;

namespace LoginDemo.UI;

public class Prompts
{
    /// <summary>
    /// This empty bankaccount is used to have a "null" option in the account
    /// selector menu. Not self explanatory and probably error-prone. This would
    /// be removed in an actual release.
    /// </summary>
    public static BankAccount NullBankAccount = new("0", "", 0, new Currency(), null);

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
            accountNumbers.Add(
                $"{bankAccount.AccountNumber} {bankAccount.Balance} {bankAccount.Currency}"
            );
        }

        // This is a ugly hack to get a cancel option in the bankaccount list.
        bankAccountList.Add(NullBankAccount);
        accountNumbers.Add("Cancel");
        var accountSelection = new SelectionPrompt<BankAccount>()
            .Title(Title)
            .AddChoices(bankAccountList);
        accountSelection.Converter = SelectionConverter(bankAccountList, accountNumbers);

        return accountSelection;
    }

    public static TextPrompt<decimal> AmountPrompt(BankAccount bankAccount)
    {
        return new TextPrompt<decimal>("Transfer amount? (Leave blank to cancel)")
            .AllowEmpty()
            .DefaultValue(0)
            .HideDefaultValue()
            .ValidationErrorMessage("Insufficient Funds")
            .Validate(
                amount =>
                    amount > bankAccount.Balance || amount < 0
                        ? ValidationResult.Error()
                        : ValidationResult.Success()
            );
    }
}
