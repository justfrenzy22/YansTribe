using bll.interfaces;
using dal.interfaces.repo;
using Moq;

namespace YansTribe.Tests.BLL.AdminService
{
    [TestClass]
    public class AdminServiceTestBase
    {

        protected Mock<IUserRepo> _userRepoMock = null!;
        protected Mock<IAdminRepo> _adminRepoMock = null!;
        protected Mock<IHashService> _hashServiceMock = null!;
        protected Mock<IAuthService> _authServiceMock = null!;
        public bll.services.AdminService _service = null!;

        [TestInitialize]
        public void Init()
        {
            _userRepoMock = new Mock<IUserRepo>();
            _hashServiceMock = new Mock<IHashService>();
            _authServiceMock = new Mock<IAuthService>();

            _service = new bll.services.AdminService(
                Mock.Of<IUserRepo>(),
                Mock.Of<IAdminRepo>(),
                _hashServiceMock.Object,
                _authServiceMock.Object
            );
        }
    }
}