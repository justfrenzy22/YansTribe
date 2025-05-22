using core.entities;
using dal.exceptions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace YansTribe.Tests.BLL.PostService
{
    public class CreateTest : PostServiceTestBase
    {
        [TestMethod]
        public async Task ValidPostWithContentOnly_ReturnsPostId()
        {
            PostCreateEntity post = new PostCreateEntity(post_id: Guid.NewGuid(), user_id: Guid.NewGuid(), content: this.test_content, created_at: DateTime.Now);
            this.postRepoMock.Setup(repo => repo.CreatePost(post)).ReturnsAsync(1);

            int? result = await this.service.CreatePost(post, null);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        [ExpectedException(typeof(BaseException))]
        public async Task InvalidPostWithFiles_ReturnsPostId()
        {
            PostCreateEntity post = new PostCreateEntity(post_id: Guid.NewGuid(), user_id: Guid.NewGuid(), content: this.test_content, created_at: DateTime.Now);
            List<IFormFile> files = new List<IFormFile> { Mock.Of<IFormFile>() };
            this.fileServiceMock.Setup(service => service.Upload(post.post_id, It.IsAny<IFormFile>(), core.enums.FileCategory.PostImage)).ThrowsAsync(new BaseException("Error uploading file", 500));

            await this.service.CreatePost(post, files);
        }

        [TestMethod]
        public async Task ValidPostWithFiles_ReturnsPostId()
        {
            PostCreateEntity post = new PostCreateEntity(post_id: Guid.NewGuid(), user_id: Guid.NewGuid(), content: this.test_content, created_at: DateTime.Now);
            List<IFormFile> files = new List<IFormFile> { Mock.Of<IFormFile>() };
            PostMedia media = new PostMedia(post.post_id, Guid.NewGuid(), core.enums.MediaType.image, "media_src");
            this.fileServiceMock.Setup(service => service.Upload(post.post_id, It.IsAny<IFormFile>(), core.enums.FileCategory.PostVideo)).ReturnsAsync(media);
            this.postRepoMock.Setup(repo => repo.CreatePost(post)).ReturnsAsync(1);

            int? result = await this.service.CreatePost(post, files);

            Assert.AreEqual(1, result);
        }
    }
}