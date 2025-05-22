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
            this.userRepoMock.Setup(repo => repo.ValidateUserByEmail(this.test_email)).ReturnsAsync((FullUser?)null);


            await this.service.ValidateUser(this.test_email, this.test_password);
        }

        [TestMethod]
        [ExpectedException(typeof(DataAccessException))]
        public async Task InvalidPassword_ReturnsException()
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

            await this.service.ValidateUser(this.test_email, this.test_password);
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
            this.hashServiceMock.Setup(hashService => hashService.hash(this.test_password)).Returns(this.test_password);
            this.authServiceMock.Setup(authService => authService.GenerateJwtToken(user.user_id.ToString(), false)).Returns(this.test_token);

            string? result = await this.service.ValidateUser(this.test_email, this.test_password);
            Assert.AreEqual(this.test_token, result);
        }
    }
}