using core.entities;
using dal.exceptions;
using dal.interfaces.db;
using dal.queries;
using Microsoft.Data.SqlClient;

/* TODO: start implementing the post repo
*/

namespace dal.repo
{
    public class PostRepo : BaseRepo
    {
        private readonly PostQuery postQuery;
        public PostRepo(IDBRepo db_repo, PostQuery postQuery) : base(db_repo) => this.postQuery = postQuery;

        public async Task<int> AddPost(Post post)
        {
            try
            {
                object? result = await this.db_repo.scalar(postQuery.add_post(), new Dictionary<string, object>{
                    { "@title", post.title },
                    { "@has_img", post.has_img},
                    { "@media_src", post.media_src},
                    { "@content", post.content },
                    { "@crated_at", post.created_at }
                });

                if (result != null && result != DBNull.Value)
                {
                    if (int.TryParse(result.ToString(), out int user_id))
                    {
                        return user_id;
                    }

                    return -1;
                }
                else
                {
                    throw new DatabaseOperationException("Post creation query executed but did not return the executed post_id");
                }
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during post creation: {sqlEx.Message}", sqlEx);
            }
            catch (Exception)
            {
                throw new DataAccessException($"An unexpected error occurred during post creation");
            }
        }
    }
}