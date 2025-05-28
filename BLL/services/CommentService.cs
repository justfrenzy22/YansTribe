using bll.interfaces;
using core.entities;
using dal.interfaces.repo;

namespace bll.services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepo _comment_repo;

        public CommentService(ICommentRepo comment_repo)
        {
            this._comment_repo = comment_repo;
        }

        // public async Task<List<Comment>> GetInitialCommentsByPostId(Guid post_id)
        // {
        //     return await this._comment_repo.GetInitialCommentsByPostId(post_id);
        // }

        public async Task<string> AddComment(Guid post_id, Guid user_id, string content, DateTime created_at)
        {

            await this._comment_repo.AddComment(post_id, user_id, content, created_at);

            return "Comment added successfully";
        }

        public async Task<List<Comment>> GetComments(Guid post_id, Guid user_id)
        {
            List<Comment>? comments = await this._comment_repo.GetComments(post_id: post_id, user_id: user_id);

            if (comments == null)
            {
                return new List<Comment>();
            }

            return comments;
        }

        // public async Task AddCommentLike(Guid comment_id, Guid user_id)
        // {
        //     await this._comment_repo.AddCommentLike(comment_id, user_id);
        // }

        // public async Task DeleteCommentLike(Guid comment_id, Guid user_id)
        // {
        //     await this._comment_repo.DeleteCommentLike(comment_id, user_id);
        // }

        // public async Task DeleteCommentById(Guid comment_id)
        // {
        //     await this._comment_repo.DeleteCommentById(comment_id);
        // }
    }
}