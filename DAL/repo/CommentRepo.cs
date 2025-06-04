using System.Data;
using core.entities;
using dal.exceptions;
using dal.interfaces.db;
using dal.interfaces.repo;
using dal.queries;
using Microsoft.Data.SqlClient;

namespace dal.repo
{
    public class CommentRepo : BaseUserRepo, ICommentRepo
    {
        private readonly CommentQuery _comment_query;

        public CommentRepo(IDBRepo db_repo, CommentQuery commentQuery) : base(db_repo)
        {
            this._comment_query = commentQuery;
        }

        public async Task AddComment(Guid post_id, Guid user_id, string content, DateTime created_at)
        {
            try
            {
                await this._db_repo.nonQuery(this._comment_query.add_comment(), new Dictionary<string, object>
                {
                    { "@post_id", post_id },
                    { "@user_id", user_id },
                    { "@content", content },
                    { "@created_at", created_at }
                });
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during comment addition", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An error occurred while adding a comment", ex);
            }
        }

        public async Task<List<Comment>> GetComments(Guid post_id, Guid user_id)
        {
            try
            {
                DataTable rows = await this._db_repo.reader(this._comment_query.get_init_comments(), new Dictionary<string, object>
                {
                    { "@post_id", post_id },
                    { "@user_id", user_id }
                });

                List<Comment> comments = new List<Comment>();

                foreach (DataRow row in rows.Rows)
                {
                    UserAccount user = new UserAccount(
                        user_id: Guid.Parse(row["user_id"].ToString()!),
                        username: row["username"].ToString()!,
                        pfp_src: row["pfp_src"].ToString()!,
                        is_private: row["is_private"] == DBNull.Value ? false : Convert.ToBoolean(row["is_private"])
                    );

                    comments.Add(new Comment(
                        comment_id: Guid.Parse(row["comment_id"].ToString()!),
                        post_id: Guid.Parse(row["post_id"].ToString()!),
                        user: user,
                        parent_id: row["parent_id"] == DBNull.Value ? Guid.Empty : Guid.Parse(row["parent_id"].ToString()!),
                        reply_count: row["reply_count"] == DBNull.Value ? 0 : Convert.ToInt32(row["reply_count"]),
                        like_count: row["like_count"] == DBNull.Value ? 0 : Convert.ToInt32(row["like_count"]),
                        is_liked_requester: row["is_liked_requester"] == DBNull.Value ? false : Convert.ToBoolean(row["is_liked_requester"]),
                        content: row["content"].ToString()!,
                        created_at: DateTime.Parse(row["created_at"].ToString()!)
                    ));
                }

                return comments;
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during comment retrieval", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An error occurred while retrieving comments", ex);
            }
        }
    }
}