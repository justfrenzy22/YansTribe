using core.entities;

namespace bll.interfaces
{
    public interface IPostService
    {
        Task<int?> CreatePost(Post post);
    }
}