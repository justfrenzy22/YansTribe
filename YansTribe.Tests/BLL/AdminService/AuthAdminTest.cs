using System.Threading.Tasks;
using bll.dto;
using core.entities;
using core.enums;
using dal.exceptions;
using Moq;

namespace YansTribe.Tests.BLL.AdminService
{
    [TestClass]
    public class AuthAdminTest : AdminServiceTestBase
    {
        [TestMethod]
        public async Task AuthAdmin_ReturnsToken()
        {
            this.userRepoMock.Setup(x => x.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(new User
            (
                user_id: this.test_user_id,
                role: Role.Admin));

            this.authServiceMock.Setup(authService => authService.VerifyTokenAsync(this.test_token, true))
                .Returns(new VerifyTokenRes { check = true, user_id = this.test_user_id });

            VerifyTokenRes result = await Task.Run(() => this.service.AuthAdmin(this.test_token));

            Assert.IsNotNull(result);
            Assert.AreEqual(this.test_user_id, result.user_id);
        }
    }
}