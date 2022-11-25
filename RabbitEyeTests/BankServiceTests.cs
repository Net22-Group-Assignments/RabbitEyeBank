using RabbitEyeBank.Services;

namespace RabbitEyeTests
{
    public class BankServiceTests
    {
        //BankService bankService;

        //public BankServiceTests()
        //{
        //    bankService = BankService.Instance;
        //}

        [Fact]
        public void LogOutWhenCurrentCustomerIsNull_ThrowsException()
        {
            var bankService = BankService.Instance;
            Assert.Throws<InvalidOperationException>(bankService.LogOut);
        }
    }
}
