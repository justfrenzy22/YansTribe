using core.entities;
using dal.exceptions;
using dal.interfaces.db;
using dal.interfaces.repo;
using dal.queries;
using Microsoft.Data.SqlClient;

/* TODO: continue implementing the post repo
*/

namespace dal.repo
{
    public class PostRepo : BaseRepo, IPostRepo
    {
        private readonly PostQuery postQuery;
        public PostRepo(IDBRepo db_repo, PostQuery postQuery) : base(db_repo) => this.postQuery = postQuery;

        public async Task<int> CreatePost(Post post)
        {
            try
            {
                var result = await this.db_repo.scalar(postQuery.add_post(), new Dictionary<string, object> {
                    { "@post_id", post.post_id },
                    { "@user_id", post.user_id },
                    { "@content", post.content },
                    { "@created_at", post.created_at }
                });

                if (result == null || result == DBNull.Value)
                {
                    throw new DatabaseOperationException("Post creation query executed but did not return a valid post_id");
                }

                Guid post_id = (Guid)result;

                if (
                    post.media == null || post.media.Count == 0
                )
                {
                    return 1;
                }


                foreach (PostMedia media in post.media)
                {
                    await this.db_repo.nonQuery(postQuery.add_post_media(), new Dictionary<string, object> {
                        { "@media_id", media.media_id },
                        { "@post_id", post_id },
                        { "@media_type", media.media_type.ToString() },
                        { "@media_src", media.media_src }
                    });
                }

                return 1;
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