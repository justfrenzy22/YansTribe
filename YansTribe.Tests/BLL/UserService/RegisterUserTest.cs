using core.entities;
using dal.exceptions;
using Moq;

namespace YansTribe.Tests.BLL.UserService
{
    [TestClass]
    public class RegisterUserTest : UserServiceTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(DataAccessException))]
        public async Task AlreadyExistingUserEmail_ReturnsException()
        {
            User user = new User(
                username: this.test_username,
                email: this.test_email,
                password: this.test_password,
                full_name: this.test_full_name,
                bio: this.test_bio,
                location: this.test_location,
                website: this.test_website,
                role: this.test_role,
                is_private: this.test_is_private,
                created_at: this.test_created_at
            );

            this.userRepoMock.Setup(repo => repo.ValidateUserByEmail(user.email)).ReturnsAsync(user);

            await this.service.RegisterUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(DataAccessException))]
        public async Task AlreadyExistingUserUsername_ReturnsException()
        {
            User user = new User(
                username: this.test_username,
                email: this.test_email,
                password: this.test_password,
                full_name: this.test_full_name,
                bio: this.test_bio,
                location: this.test_location,
                website: this.test_website,
                role: this.test_role,
                is_private: this.test_is_private,
                created_at: this.test_created_at
            );

            this.userRepoMock.Setup(repo => repo.ValidateUserByEmail(user.email)).ReturnsAsync(user);

            this.userRepoMock.Setup(repo => repo.GetUserByUsername(user.username)).ReturnsAsync(user);

            await this.service.RegisterUser(user);
        }

        [TestMethod]
        public async Task ValidUser_ReturnsUserId()
        {
            User user = new User(
                user_id: this.test_user_id,
                username: this.test_username,
                email: this.test_email,
                password: this.test_password,
                full_name: this.test_full_name,
                bio: this.test_bio,
                pfp_src: this.test_pfp_src,
                location: this.test_location,
                website: this.test_website,
                is_private: this.test_is_private,
                created_at: this.test_created_at,
                role: this.test_role
            );

            this.userRepoMock.Setup(repo => repo.ValidateUserByEmail(user.email)).ReturnsAsync((User?)null);
            this.userRepoMock.Setup(repo => repo.GetUserByUsername(user.username)).ReturnsAsync((User?)null);
            this.userRepoMock.Setup(repo => repo.RegisterUser(user)).ReturnsAsync(user.user_id);
            this.hashServiceMock.Setup(service => service.hash(user.password)).Returns(user.password);

            Guid? result = await this.service.RegisterUser(user);

            Assert.AreEqual(user.user_id, result);
        }
    }
}