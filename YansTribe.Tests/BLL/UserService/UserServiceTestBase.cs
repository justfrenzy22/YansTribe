using bll.interfaces;
using core.enums;
using dal.interfaces.repo;
using Moq;

namespace YansTribe.Tests.BLL.UserService
{
    [TestClass]
    public class UserServiceTestBase
    {
        protected Mock<IUserRepo> userRepoMock = null!;
        protected Mock<IHashService> hashServiceMock = null!;
        protected Mock<IAuthService> authServiceMock = null!;
        public bll.services.UserService service = null!;

        [TestInitialize]
        public void Init()
        {
            this.userRepoMock = new Mock<IUserRepo>();
            this.hashServiceMock = new Mock<IHashService>();
            this.authServiceMock = new Mock<IAuthService>();

            this.service = new bll.services.UserService(
                this.userRepoMock.Object,
                this.hashServiceMock.Object,
                this.authServiceMock.Object
            );
        }

        protected Guid test_user_id = Guid.NewGuid();
        protected string test_username = "User";
        protected string test_email = "@example.com";
        protected string test_password = "Password";
        protected string test_full_name = "User";
        protected string test_bio = "This is a user for testing purposes.";
        protected string test_pfp_src = "https://example.com";
        protected string test_location = "Test Location";
        protected string test_website = "https://example.com";
        protected Role test_role = Role.User;
        protected bool test_is_private = false;
        protected DateTime test_created_at = DateTime.Now;
        protected string test_token = "Test Token";
    }
}