using core.entities;
using dal.exceptions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace YansTribe.Tests.BLL.FileService
{
    [TestClass]
    public class FileServiceTest : FileServiceTestBase
    {
        [TestMethod]
        public async Task Upload_ValidFile_ReturnsPostMedia()
        {
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns("test.jpg");
            fileMock.Setup(f => f.ContentType).Returns("image/jpeg");

            Guid post_id = Guid.NewGuid();
            PostMedia result = await this.service.Upload(post_id, fileMock.Object);

            Assert.IsNotNull(result);
            Assert.AreEqual(post_id, result.post_id);
            Assert.AreEqual(core.enums.MediaType.image, result.media_type);
        }
    }
}