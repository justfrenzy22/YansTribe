using System.Threading.Tasks;
using core.entities;
using dal.exceptions;
using Moq;

namespace YansTribe.Tests.BLL.AdminService
{
    [TestClass]
    public class ChangeRoleTest : AdminServiceTestBase
    {
        [TestMethod]
        public async Task ChangeRole_ValidUser_UpdatesRole()
        {
            var user = new UserDetails
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
                role: core.enums.Role.User
            );

            var newRole = core.enums.Role.Admin;

            this.userRepoMock.Setup(repo => repo.GetUserById(this.test_user_id)).ReturnsAsync(user);
            this.userRepoMock.Setup(repo => repo.ChangeRole(user.user_id, newRole.ToString())).ReturnsAsync(true);

            string check = await this.service.ChangeRole(this.test_user_id.ToString(), newRole.ToString());

            Assert.AreEqual("User role updated successfully.", check);
        }

        [TestMethod]
        [ExpectedException(typeof(DataAccessException))]
        public async Task ChangeRole_InvalidUser_ThrowsException()
        {
            this.userRepoMock.Setup(repo => repo.GetUserById(this.test_user_id)).ReturnsAsync((UserDetails?)null);

            string check = await this.service.ChangeRole(this.test_user_id.ToString(), core.enums.Role.Admin.ToString());
            Assert.AreEqual("User not found or invalid role", check);
        }
    }
}