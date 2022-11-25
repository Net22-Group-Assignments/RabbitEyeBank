namespace RabbitEyeBank.Services;

public static class ServiceContainer
{
    public static BankService bankService = new();
    public static AccountService accountService = new();
    public static MoneyTransferService MoneyTransferService = new(accountService);
}
