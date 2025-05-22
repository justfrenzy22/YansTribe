using core.entities;
using dal.exceptions;
using Moq;

namespace YansTribe.Tests.BLL.AdminService
{
    [TestClass]
    public class ValidateLoginTest : AdminServiceTestBase
    {
        [TestMethod]
        public void InvalidUser_ReturnsNull()
        {
            string? token = this.service.ValidateLogin(this.test_email, this.test_password).Result;

            Assert.IsNull(token);
        }

        [TestMethod]
        public async Task NonExistentUser_ReturnsNull()
        {
            this.userRepoMock.Setup(repo => repo.ValidateUserByEmail(this.test_email)).ReturnsAsync((FullUser?)null);

            string? result = await this.service.ValidateLogin(this.test_email, this.test_password);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task InvalidRole_ReturnsNull()
        {
            var user = new FullUser
            (
                user_id: this.test_user_id,
                username: this.test_username,
                pfp_src: "",
                email: this.test_email,
                full_name: this.test_full_name,
                bio: this.test_bio,
                location: this.test_location,
                website: this.test_website,
                is_private: false,
                created_at: DateTime.Now,
                role: core.enums.Role.User,
                password: this.test_hashed_password
            );

            this.userRepoMock.Setup(repo => repo.ValidateUserByEmail(this.test_email)).ReturnsAsync(user);

            string? result = await this.service.ValidateLogin(this.test_email, this.test_password);

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(DataAccessException))]
        public async Task InvalidPassword_ThrowsException()
        {
            var user = new FullUser
            (
                user_id: this.test_user_id,
                username: this.test_username,
                pfp_src: "",
                email: this.test_email,
                full_name: this.test_full_name,
                bio: this.test_bio,
                location: this.test_location,
                website: this.test_website,
                is_private: false,
                created_at: DateTime.Now,
                role: core.enums.Role.User,
                password: this.test_hashed_password
            );

            this.userRepoMock.Setup(repo => repo.ValidateUserByEmail(this.test_email)).ReturnsAsync(user);
            this.hashServiceMock.Setup(hashService => hashService.hash(this.test_password)).Returns("differentHash");

            await this.service.ValidateLogin(this.test_email, this.test_password);
        }

        [TestMethod]
        public async Task ValidLogin_ReturnsToken()
        {
            var user = new FullUser
            (
                user_id: this.test_user_id,
                username: this.test_username,
                pfp_src: "",
                email: this.test_email,
                full_name: this.test_full_name,
                bio: this.test_bio,
                location: this.test_location,
                website: this.test_website,
                is_private: false,
                created_at: DateTime.Now,
                role: core.enums.Role.User,
                password: this.test_hashed_password
            );

            this.userRepoMock.Setup(repo => repo.ValidateUserByEmail(this.test_email)).ReturnsAsync(user);
            this.hashServiceMock.Setup(hashService => hashService.hash(this.test_password)).Returns(this.test_hashed_password);
            this.authServiceMock.Setup(authService => authService.GenerateJwtToken(user.user_id.ToString(), true)).Returns(this.test_token);

            string? result = await this.service.ValidateLogin(this.test_email, this.test_password);

            Assert.IsNotNull(result);
            Assert.AreEqual(this.test_token, result);
        }
    }
}