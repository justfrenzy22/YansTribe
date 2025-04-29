using core.entities;

namespace dal.interfaces.repo
{
    public interface IPostRepo
    {
        Task<int> CreatePost(Post post);
    }
}