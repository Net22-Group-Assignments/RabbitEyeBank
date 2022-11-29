namespace RabbitEyeBankLibrary.Services;

public static class ServiceContainer
{
    public static UserService UserService = new();
    public static AccountService AccountService = new();
    public static CurrencyService CurrencyService = new();
    public static MoneyTransferService MoneyTransferService =
        new(UserService, AccountService, CurrencyService);
}
