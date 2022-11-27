namespace RabbitEyeBank.Services;

public static class ServiceContainer
{
    public static UserService UserService = new();
    public static AccountService accountService = new();
    public static MoneyTransferService MoneyTransferService = new(UserService, accountService);
    public static CurrencyService CurrencyService = new();
}
