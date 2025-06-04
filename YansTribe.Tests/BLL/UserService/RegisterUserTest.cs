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
            UserCredentials user = new UserCredentials
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

            this.userRepoMock.Setup(repo => repo.ValidateUserByEmail(user.email)).ReturnsAsync(user);

            await this.service.RegisterUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(DataAccessException))]
        public async Task AlreadyExistingUserUsername_ReturnsException()
        {
            UserCredentials user = new UserCredentials
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

            this.userRepoMock.Setup(repo => repo.ValidateUserByEmail(user.email)).ReturnsAsync(user);

            this.userRepoMock.Setup(repo => repo.GetUserByUsername(user.username)).ReturnsAsync(user);

            await this.service.RegisterUser(user);
        }

        [TestMethod]
        public async Task ValidUser_ReturnsUserId()
        {
            UserCredentials user = new UserCredentials
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

            this.userRepoMock.Setup(repo => repo.ValidateUserByEmail(user.email)).ReturnsAsync((UserCredentials?)null);
            this.userRepoMock.Setup(repo => repo.GetUserByUsername(user.username)).ReturnsAsync((UserCredentials?)null);
            this.userRepoMock.Setup(repo => repo.RegisterUser(user)).ReturnsAsync(user.user_id);
            this.hashServiceMock.Setup(service => service.hash(user.password)).Returns(user.password);

            Guid? result = await this.service.RegisterUser(user);

            Assert.AreEqual(user.user_id, result);
        }
    }
}