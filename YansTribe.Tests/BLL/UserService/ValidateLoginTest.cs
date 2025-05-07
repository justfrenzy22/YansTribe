using System.Threading.Tasks;
using core.entities;
using dal.exceptions;
using Moq;

namespace YansTribe.Tests.BLL.UserService
{
    [TestClass]
    public class ValidateLoginTest : UserServiceTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(DataAccessException))]
        public async Task NonExistentUser_ReturnsException()
        {
            this.userRepoMock.Setup(repo => repo.ValidateUserByEmail(this.test_email)).ReturnsAsync((User?)null);


            await this.service.ValidateUser(this.test_email, this.test_password);
        }

        [TestMethod]
        [ExpectedException(typeof(DataAccessException))]
        public async Task InvalidPassword_ReturnsException()
        {
            var user = new User
            (
                user_id: Guid.NewGuid(),
                email: this.test_email,
                password: this.test_password,
                role: this.test_role
            );

            this.userRepoMock.Setup(repo => repo.ValidateUserByEmail(this.test_email)).ReturnsAsync(user);
            this.hashServiceMock.Setup(hashService => hashService.hash(this.test_password)).Returns("differentHash");

            await this.service.ValidateUser(this.test_email, this.test_password);
        }

        [TestMethod]
        public async Task ValidLogin_ReturnsToken()
        {
            var user = new User
            (
                user_id: Guid.NewGuid(),
                email: this.test_email,
                password: this.test_password,
                role: core.enums.Role.User
            );

            this.userRepoMock.Setup(repo => repo.ValidateUserByEmail(this.test_email)).ReturnsAsync(user);
            this.hashServiceMock.Setup(hashService => hashService.hash(this.test_password)).Returns(this.test_password);
            this.authServiceMock.Setup(authService => authService.GenerateJwtToken(user.user_id.ToString(), false)).Returns(this.test_token);

            string? result = await this.service.ValidateUser(this.test_email, this.test_password);
            Assert.AreEqual(this.test_token, result);
        }
    }
}