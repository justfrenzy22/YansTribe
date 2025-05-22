using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace YansTribe.Tests.BLL.HashService
{
    [TestClass]
    public class HashServiceTest : HashServiceTestBase
    {
        [TestMethod]
        public void Hash_ValidInput_ReturnsHashedString()
        {
            string input = "password";
            string result = this.service.hash(input);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(input, result);
        }

        [TestMethod]
        public void Hash_SameInput_ReturnsSameHash()
        {
            string input = "password";
            string result1 = this.service.hash(input);
            string result2 = this.service.hash(input);

            Assert.AreEqual(result1, result2);
        }
    }
}