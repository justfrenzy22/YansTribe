
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace YansTribe.Tests.BLL.AuthService
{
    [TestClass]
    public class GenerateTokenTest : AuthServiceTestBase
    {


        [TestMethod]
        public void AdminUser_CreateValidToken()
        {
            string? token = this._service.GenerateJwtToken(user_id: this.TestUserId.ToString(), isAdmin: true);
            Assert.IsFalse(string.IsNullOrEmpty(token));
        }

        [TestMethod]
        public void NormalUser_CreateValidToken()
        {
            string? token = this._service.GenerateJwtToken(user_id: this.TestUserId.ToString(), isAdmin: false);
            Assert.IsFalse(string.IsNullOrEmpty(token));
        }

        [TestMethod]
        public void User_ValidCredentials()
        {
            string? token = this._service.GenerateJwtToken(user_id: this.TestUserId.ToString(), isAdmin: false);

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken decoded = handler.ReadJwtToken(token);
            Assert.AreEqual(decoded.Issuer, this._conf["Jwt:Issuer"]);
            Assert.AreEqual(decoded.Audiences.FirstOrDefault(), this._conf["Jwt:Audience"]);
        }


        [TestMethod]
        public void User_ValidClaims()
        {

            string? token = this._service.GenerateJwtToken(user_id: this.TestUserId.ToString(), isAdmin: false);

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken decoded = handler.ReadJwtToken(token);

            var claim = decoded.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name || c.Type == "unique_name");

            Assert.IsTrue(Convert.ToInt32(claim?.Value) == this.TestUserId);
        }

    }
}