using core.entities;

namespace dal.interfaces.repo
{
    public interface ICommentRepo
    {
        Task AddComment(Guid post_id, Guid user_id, string content, DateTime created_at);
        Task<List<Comment>> GetComments(Guid post_id, Guid user_id);
        // Task<List<core.entities.Comment>> GetCommentsByPostIdAsync(Guid postId);
        // Task<core.entities.Comment?> GetCommentByIdAsync(Guid commentId);
        // Task UpdateCommentAsync(core.entities.Comment comment);
        // Task DeleteCommentAsync(Guid commentId);
    }
}