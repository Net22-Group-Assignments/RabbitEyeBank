using RabbitEyeBank.Services;

namespace RabbitEyeTests
{
    public class BankServiceTests
    {
        readonly UserService userService;

        public BankServiceTests()
        {
            userService = new UserService();
        }

        [Fact]
        public void LogOutWhenCurrentCustomerIsNull_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(userService.LogOut);
        }
    }
}
