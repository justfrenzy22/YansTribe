namespace YansTribe.Tests.BLL.HashService
{
    [TestClass]
    public class HashServiceTestBase
    {
        public bll.services.HashService service = null!;

        [TestInitialize]
        public void Init()
        {
            this.service = new bll.services.HashService();
        }
    }
}