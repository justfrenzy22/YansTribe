using bll.interfaces;
using core.entities;
using dal.exceptions;
using dal.interfaces.repo;
using Microsoft.AspNetCore.Http;

namespace bll.services
{
    public class PostService : IPostService
    {
        private readonly IPostRepo post_repo;
        private readonly IFileService file_service;

        public PostService(IPostRepo post_repo, IFileService file_service)
        {
            this.post_repo = post_repo;
            this.file_service = file_service;
        }

        public async Task<List<Post>?> GetHomePosts(Guid user_id)
            => await this.post_repo.GetHomeInitPosts(user_id);


        public async Task<int?> CreatePost(PostCreateEntity post, List<IFormFile>? files)
        {
            try
            {
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {

                        if (file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                        {
                            PostMedia media = await this.file_service.Upload(post.post_id, file, core.enums.FileCategory.PostImage);
                            post.AddMedia(media: media);
                        }
                        else
                        {
                            PostMedia media = await this.file_service.Upload(post.post_id, file, core.enums.FileCategory.PostVideo);
                            post.AddMedia(media: media);
                        }
                    }
                }
                return await this.post_repo.CreatePost(post);
            }
            catch (Exception ex)
            {
                throw new BaseException($"Error uploading file: {ex.Message}", 500, ex);
            }
        }

        public async Task LikePost(Guid post_id, Guid user_id) =>
            await this.post_repo.LikePost(post_id, user_id);

        public async Task DislikePost(Guid post_id, Guid user_id) =>
            await this.post_repo.DislikePost(post_id, user_id);
    }
}