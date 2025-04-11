using core.entities;

namespace dal.interfaces.repo
{
    public interface IPostRepo
    {
        Task<int> AddPost(Post post);
    }
}