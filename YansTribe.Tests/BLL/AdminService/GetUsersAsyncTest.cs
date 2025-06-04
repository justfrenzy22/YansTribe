using System.Collections.Generic;
using System.Threading.Tasks;
using core.entities;
using Moq;

namespace YansTribe.Tests.BLL.AdminService
{
    [TestClass]
    public class GetUsersAsyncTest : AdminServiceTestBase
    {
        [TestMethod]
        public async Task GetUsers_ReturnsUserList()
        {
            var users = new List<UserCredentials>
            {
                new UserCredentials
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
                ),
                new UserCredentials
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
                )
            };

            this.adminRepoMock.Setup(repo => repo.GetAllUsersAsync(this.test_user_id)).ReturnsAsync(users);

            var result = await this.service.GetUsersAsync(this.test_user_id.ToString());

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
    }
}