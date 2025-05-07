using bll.interfaces;
using dal.interfaces.repo;
using Moq;

namespace YansTribe.Tests.BLL.PostService
{
    [TestClass]
    public class PostServiceTestBase
    {
        protected Mock<IPostRepo> postRepoMock = null!;
        protected Mock<IFileService> fileServiceMock = null!;
        public bll.services.PostService service = null!;

        [TestInitialize]
        public void Init()
        {
            this.postRepoMock = new Mock<IPostRepo>();
            this.fileServiceMock = new Mock<IFileService>();

            this.service = new bll.services.PostService(
                this.postRepoMock.Object,
                this.fileServiceMock.Object
            );
        }

        protected string test_content = "test content";
    }
}