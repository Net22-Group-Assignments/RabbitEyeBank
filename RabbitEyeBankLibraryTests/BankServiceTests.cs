using RabbitEyeBankLibrary.Services;

namespace RabbitEyeBankLibraryTests
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
