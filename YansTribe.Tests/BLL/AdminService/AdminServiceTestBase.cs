using bll.interfaces;
using core.enums;
using dal.interfaces.repo;
using Moq;

namespace YansTribe.Tests.BLL.AdminService
{
    [TestClass]
    public class AdminServiceTestBase
    {

        protected Mock<IUserRepo> userRepoMock = null!;
        protected Mock<IAdminRepo> adminRepoMock = null!;
        protected Mock<IHashService> hashServiceMock = null!;
        protected Mock<IAuthService> authServiceMock = null!;
        public bll.services.AdminService service = null!;

        [TestInitialize]
        public void Init()
        {
            this.userRepoMock = new Mock<IUserRepo>();
            this.adminRepoMock = new Mock<IAdminRepo>(); // Initialize adminRepoMock
            this.hashServiceMock = new Mock<IHashService>();
            this.authServiceMock = new Mock<IAuthService>();

            this.service = new bll.services.AdminService(
                this.userRepoMock.Object,
                this.adminRepoMock.Object,
                this.hashServiceMock.Object,
                this.authServiceMock.Object
            );
        }

        protected string test_email = "test@example.com";
        protected string test_password = "password";
        protected string test_hashed_password = "hashedPassword";
        protected string test_token = "validToken";
        protected Guid test_user_id = Guid.NewGuid();
        protected Role test_role = Role.Admin;

    }
}