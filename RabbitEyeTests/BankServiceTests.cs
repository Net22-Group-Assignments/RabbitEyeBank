using RabbitEyeBank.Services;

namespace RabbitEyeTests
{
    public class BankServiceTests
    {
        readonly BankService bankService;

        public BankServiceTests()
        {
            bankService = new BankService();
        }

        [Fact]
        public void LogOutWhenCurrentCustomerIsNull_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(bankService.LogOut);
        }
    }
}
