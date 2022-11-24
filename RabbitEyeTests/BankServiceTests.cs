using RabbitEyeBank.Services;

namespace RabbitEyeTests
{
    public class BankServiceTests
    {
        [Fact]
        public void LogOutWhenCurrentCustomerIsNull_ThrowsException()
        {
            Assert.Throws<InvalidOperationException>(BankServices.LogOut);
        }
    }
}
