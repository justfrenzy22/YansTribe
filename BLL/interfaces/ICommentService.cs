using core.entities;

namespace bll.interfaces
{
    public interface ICommentService
    {
        Task<string> AddComment(Guid post_id, Guid user_id, string content, DateTime created_at);
        Task<List<Comment>> GetComments(Guid post_id, Guid user_id);
    }
}