using bll.dto;
using core.entities;
using core.enums;
using dal.interfaces.repo;
using Moq;

namespace YansTribe.Tests.BLL.AdminService
{
    [TestClass]
    public class AuthSuperAdminTest : AdminServiceTestBase
    {
        [TestMethod]
        public async Task InvalidToken_ReturnsCheckFalseWithException()
        {
            this.authServiceMock.Setup(authService => authService.VerifyTokenAsync(this.test_token, true))
                .Returns(new VerifyTokenRes { check = false, exception = "Invalid token" });

            VerifySuperAdminDTO result = await this.service.AuthSuperAdmin(this.test_token);

            Assert.IsFalse(result.check);
            Assert.AreEqual("Invalid token", result.exception);
        }

        [TestMethod]
        public async Task UserNotFoundOrInvalidRole_ReturnsCheckFalseWithException()
        {
            this.authServiceMock.Setup(authService => authService.VerifyTokenAsync(this.test_token, true))
                .Returns(new VerifyTokenRes { check = true, user_id = this.test_user_id });

            this.userRepoMock.Setup(repo => repo.GetUserById(this.test_user_id)).ReturnsAsync((User?)null);

            VerifySuperAdminDTO result = await this.service.AuthSuperAdmin(this.test_token);

            Assert.IsFalse(result.check);
            Assert.AreEqual("User not found or invalid role", result.exception);
        }

        [TestMethod]
        public async Task AdminRole_ReturnsCheckFalseWithUnauthorizedPermissions()
        {
            this.authServiceMock.Setup(authService => authService.VerifyTokenAsync(this.test_token, true))
                .Returns(new VerifyTokenRes { check = true, user_id = this.test_user_id });

            var admin = new User(user_id: this.test_user_id, role: Role.Admin);

            this.userRepoMock.Setup(repo => repo.GetUserById(this.test_user_id)).ReturnsAsync(admin);

            VerifySuperAdminDTO result = await this.service.AuthSuperAdmin(this.test_token);

            Assert.IsFalse(result.check);
            Assert.AreEqual("Unauthorized permissions", result.exception);
        }

        [TestMethod]
        public async Task SuperAdminRole_ReturnsCheckTrue()
        {
            this.authServiceMock.Setup(authService => authService.VerifyTokenAsync(this.test_token, true))
                .Returns(new VerifyTokenRes { check = true, user_id = this.test_user_id });

            var superAdmin = new User(
                user_id: this.test_user_id,
                role: Role.SuperAdmin
            );

            this.userRepoMock.Setup(repo => repo.GetUserById(this.test_user_id)).ReturnsAsync(() => superAdmin);

            VerifySuperAdminDTO result = await this.service.AuthSuperAdmin(this.test_token);

            Assert.IsTrue(result.check);
            Assert.IsNull(result.exception);
        }
    }
}