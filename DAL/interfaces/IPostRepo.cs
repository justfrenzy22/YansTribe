using core.entities;

namespace dal.interfaces.repo
{
    public interface IPostRepo
    {
        Task<int> CreatePost(PostCreateEntity post);
        Task<List<Post>> GetProfileInitPostsById(Guid req_user_id, Guid user_id);
        Task LikePost(Guid post_id, Guid user_id);
        Task DislikePost(Guid post_id, Guid user_id);
        Task<List<Post>?> GetHomeInitPosts(Guid user_id);
    }
}