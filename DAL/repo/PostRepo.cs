using System.Data;
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
                var result = await this.db_repo.scalar(this.postQuery.add_post(), new Dictionary<string, object> {
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
        // public async Task<List<Post>?> GetInitPostsById(Guid user_id)
        // {
        //     try
        //     {
        //         var result = await this.db_repo.reader(this.postQuery.get_first_10_posts_by_user_id(), new Dictionary<string, object> {
        //             { "@user_id", user_id }
        //         });

        //         if (result.Rows.Count == 0)
        //         {
        //             return null;
        //         }

        //         List<Post> posts = new List<Post>();

        //         foreach (DataRow row in result.Rows) {
        //             Post post = new Post(
        //                 post_id: Guid.Parse(row["post_id"].ToString() ?? string.Empty),
        //                 user_id: Guid.Parse(row["user_id"].ToString() ?? string.Empty),
        //                 content: row["content"]?.ToString() ?? string.Empty,
        //                 created_at: Convert.ToDateTime(row["created_at"]),
        //                 edited: Convert.ToBoolean(row["edited"]),
        //                 edited_at: Convert.ToDateTime(row["edited_at"])
        //             );


        //         }
        //     }
        //     catch (SqlException sqlEx)
        //     {
        //         throw new DatabaseOperationException($"Database error during post retrieval: {sqlEx.Message}", sqlEx);
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new DataAccessException($"An unexpected error occurred during post retrieval: {ex.Message}", ex);
        //     }
        // }
    }
}