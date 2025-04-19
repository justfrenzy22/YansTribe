using Microsoft.Extensions.Configuration;

namespace YansTribe.Tests.BLL.AuthService
{
    [TestClass]
    public class AuthServiceTestBase
    {
        public IConfiguration _conf = null!;
        protected bll.services.AuthService _service = null!;

        protected int? TestUserId = 123;

        [TestInitialize]
        public void Init()
        {

            var configPath = Path.Combine(
                Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName,
                "..\\..\\PL",
                "appsettings.json"
            );

            this._conf = new ConfigurationBuilder()
                .AddJsonFile(configPath, optional: false, reloadOnChange: true)
                .Build();

            this._service = new bll.services.AuthService(this._conf);
        }
    }
}