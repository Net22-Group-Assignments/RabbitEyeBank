using RabbitEyeBank;
using Xunit.Sdk;

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
