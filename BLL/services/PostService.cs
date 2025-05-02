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

        public async Task<int?> CreatePost(Post post, List<IFormFile>? files)
        {
            try
            {
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        PostMedia media = await this.file_service.Upload(post.post_id, file);
                        post.AddMedia(media: media);
                    }
                }
                return await this.post_repo.CreatePost(post);
            }
            catch (Exception ex)
            {
                throw new BaseException($"Error uploading file: {ex.Message}", 500, ex);
            }
        }
    }
}