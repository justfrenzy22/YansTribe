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
            var users = new List<User>
            {
                new User ( user_id : Guid.NewGuid(), role : core.enums.Role.User ),
                new User ( user_id : Guid.NewGuid(), role : core.enums.Role.Admin )
            };

            this.adminRepoMock.Setup(repo => repo.GetAllUsersAsync(this.test_user_id)).ReturnsAsync(users);

            var result = await this.service.GetUsersAsync(this.test_user_id.ToString());

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
        }
    }
}