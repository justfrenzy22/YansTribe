namespace YansTribe.Tests.BLL.FileService
{
    [TestClass]
    public class FileServiceTestBase
    {
        public bll.services.FileService service = null!;

        [TestInitialize]
        public void Init()
        {
            this.service = new bll.services.FileService();
        }
    }
}