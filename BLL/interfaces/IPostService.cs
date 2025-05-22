using core.entities;
using Microsoft.AspNetCore.Http;

namespace bll.interfaces
{
    public interface IPostService
    {
        Task<int?> CreatePost(PostCreateEntity post, List<IFormFile>? files);
        Task LikePost(Guid post_id, Guid user_id);
        Task DislikePost(Guid post_id, Guid user_id);
        Task<List<Post>?> GetHomePosts(Guid user_id);
    }
}