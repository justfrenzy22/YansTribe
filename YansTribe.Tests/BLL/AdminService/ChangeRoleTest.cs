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
            var user = new User
            (
                user_id: this.test_user_id,
                role: this.test_role
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
            this.userRepoMock.Setup(repo => repo.GetUserById(this.test_user_id)).ReturnsAsync((User?)null);

            string check = await this.service.ChangeRole(this.test_user_id.ToString(), core.enums.Role.Admin.ToString());
            Assert.AreEqual("User not found or invalid role", check);
        }
    }
}