using bll.dto;

namespace YansTribe.Tests.BLL.AuthService
{
    [TestClass]
    public class VerifyTokenTest : AuthServiceTestBase
    {
        [TestMethod]
        public void ValidTokenAdmin_ReturnsTrue()
        {
            string? token = this._service.GenerateJwtToken(user_id: this.TestUserId.ToString() ?? "", isAdmin: false);
            VerifyTokenRes result = this._service.VerifyTokenAsync(token: token, isAdmin: false);

            Assert.IsTrue(result.check);
            Assert.AreEqual(result.user_id, this.TestUserId);
        }

        [TestMethod]
        public void InvalidTokenAdmin_ReturnsFalse()
        {
            string? token = "invalid_token";
            VerifyTokenRes result = this._service.VerifyTokenAsync(token: token, isAdmin: false);

            Assert.IsFalse(result.check);
            Assert.IsNull(result.user_id);
        }

        [TestMethod]
        public void EmptyToken_ReturnsFalse()
        {
            string? token = string.Empty;
            VerifyTokenRes result = this._service.VerifyTokenAsync(token: token, isAdmin: false);

            Assert.IsFalse(result.check);
        }
        [TestMethod]
        public void NullToken_ReturnsFalse()
        {
            string? token = null;
#pragma warning disable CS8604 // Possible null reference argument.
            VerifyTokenRes result = this._service.VerifyTokenAsync(token: token, isAdmin: false);
#pragma warning restore CS8604 // Possible null reference argument.

            Assert.IsFalse(result.check);
        }

        [TestMethod]
        public void InvalidTokenUser_ReturnsFalse()
        {
            // string? token = this._service.GenerateJwtToken(user_id: this.TestUserId.ToString(), isAdmin: false);
            VerifyTokenRes result = this._service.VerifyTokenAsync(token: "asdsadad", isAdmin: true);

            Assert.IsFalse(result.check);
        }
    }
}