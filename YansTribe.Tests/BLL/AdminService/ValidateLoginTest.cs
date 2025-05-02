using core.entities;
using Moq;

namespace YansTribe.Tests.BLL.AdminService
{
    [TestClass]
    public class ValidateLoginTest : AdminServiceTestBase
    {
        [TestMethod]
        public void InvalidLogin_ReturnsNull()
        {
            string email = "a@b.com";
            string password = "password";

            string? token = this._service.ValidateLogin(email, password).Result;

            Assert.IsNull(token);
        }
        /* TODO : Add more test cases and make them work with Mock data */

        [TestMethod]
        public void ValidLogin_ReturnsToken()
        {
            string email = "just.frenzy22@gmail.com";
            string password = "asdasd12";
            string hashedPassword = "hashedPassword";
            string token = "validToken";

            var user = new User
            (
                user_id: Guid.NewGuid(),
                email: email,
                password: hashedPassword,
                role: core.enums.Role.Admin
            );

            this._userRepoMock
                .Setup(repo => repo.ValidateUserByEmail(email))
                .ReturnsAsync(user);

            this._hashServiceMock
                .Setup(hashService => hashService.hash(password))
                .Returns(hashedPassword);

            this._authServiceMock
                .Setup(authService => authService.GenerateJwtToken(user.user_id.ToString(), true))
                .Returns(token);

            string? result = this._service.ValidateLogin(email, password).Result;

            Assert.IsNotNull(result); // Ensure the result is not null
            Assert.AreEqual(token, result); // Ensure the result matches the expected token
        }
    }
}