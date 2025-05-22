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

            this.userRepoMock.Setup(repo => repo.GetUserById(this.test_user_id)).ReturnsAsync((SafeUser?)null);

            VerifySuperAdminDTO result = await this.service.AuthSuperAdmin(this.test_token);

            Assert.IsFalse(result.check);
            Assert.AreEqual("User not found or invalid role", result.exception);
        }

        [TestMethod]
        public async Task AdminRole_ReturnsCheckFalseWithUnauthorizedPermissions()
        {
            this.authServiceMock.Setup(authService => authService.VerifyTokenAsync(this.test_token, true))
                .Returns(new VerifyTokenRes { check = true, user_id = this.test_user_id });

            var admin = new SafeUser
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

            var superAdmin = new SafeUser
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

            this.userRepoMock.Setup(repo => repo.GetUserById(this.test_user_id)).ReturnsAsync(() => superAdmin);

            VerifySuperAdminDTO result = await this.service.AuthSuperAdmin(this.test_token);

            Assert.IsTrue(result.check);
            Assert.IsNull(result.exception);
        }
    }
}