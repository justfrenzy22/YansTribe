using System.Data;
using System.Text.Json;
using core.entities;
using core.enums;
using dal.dto;
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

        public async Task<int> CreatePost(PostCreateEntity post)
        {
            try
            {
                var result = await this.db_repo.scalar(this.postQuery.add_post(), new Dictionary<string, object> {
                    { "@post_id", post.post_id },
                    { "@user_id", post.user_id },
                    { "@content", post.content },
                    { "@created_at", post.created_at },
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

        public async Task<List<Post>?> GetHomeInitPosts(Guid user_id)
        {
            try
            {
                var result = await this.db_repo.reader(this.postQuery.get_home_init(), new Dictionary<string, object> {
                    { "@user_id", user_id }
                });

                if (result.Rows.Count == 0)
                {
                    return null;
                }

                List<Post> posts = new List<Post>();

                foreach (DataRow row in result.Rows)
                {
                    Post post = new Post(
                        post_id: Guid.Parse(row["post_id"].ToString() ?? string.Empty),
                        content: row["content"]?.ToString() ?? string.Empty,
                        edited: Convert.ToBoolean(row["edited"]),
                        edited_at: row["edited_at"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["edited_at"]),
                        created_at: Convert.ToDateTime(row["created_at"]),
                        like_count: Convert.ToInt32(row["like_count"]),
                        comment_count: Convert.ToInt32(row["comment_count"]),
                        is_liked_requester: row["is_liked_requester"] != DBNull.Value && Convert.ToBoolean(row["is_liked_requester"]),
                        user: new BaseUser(
                            user_id: Guid.Parse(row["user_id"].ToString() ?? string.Empty),
                            username: row["username"]?.ToString() ?? string.Empty,
                            pfp_src: row["pfp_src"]?.ToString() ?? string.Empty,
                            is_private: Convert.ToBoolean(row["is_private"])
                        )
                    );

                    if (row["media_json"] != DBNull.Value)
                    {
                        string mediaJson = row["media_json"].ToString() ?? "[]";
                        List<MediaJsonDTO> mediaList = JsonSerializer.Deserialize<List<MediaJsonDTO>>(mediaJson) ?? new List<MediaJsonDTO>();

                        if (mediaList.Count > 0)
                        {
                            foreach (var media in mediaList)
                            {
                                if (Enum.TryParse<MediaType>(media.media_type.ToString(), true, out var parsedMediaType))
                                {
                                    var mediaObj = new PostMedia(
                                        post_id: post.post_id,
                                        media_id: media.media_id,
                                        media_type: parsedMediaType,
                                        media_src: media.media_src
                                    );

                                    post.AddMedia(mediaObj);
                                }
                            }
                        }
                    }

                    posts.Add(post);
                }

                return posts;
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during posts retrieval: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during posts retrieval: {ex.Message}", ex);
            }
        }

        public async Task<List<Post>> GetProfileInitPostsById(Guid req_user_id, Guid user_id)
        {
            try
            {
                var result = await this.db_repo.reader(this.postQuery.get_first_10_posts_by_user_id(), new Dictionary<string, object> {
                    { "@user_id", user_id },
                    { "@req_user_id", req_user_id }
                });

                if (result.Rows.Count == 0)
                {
                    return new List<Post>();
                }

                List<Post> posts = new List<Post>();

                foreach (DataRow row in result.Rows)
                {
                    Post post = new Post(
                        post_id: Guid.Parse(row["post_id"].ToString() ?? string.Empty),
                        content: row["content"]?.ToString() ?? string.Empty,
                        edited: Convert.ToBoolean(row["edited"]),
                        edited_at: row["edited_at"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["edited_at"]),
                        created_at: Convert.ToDateTime(row["created_at"]),
                        like_count: Convert.ToInt32(row["like_count"]),
                        comment_count: Convert.ToInt32(row["comment_count"]),
                        is_liked_requester: row["is_liked_requester"] != DBNull.Value && Convert.ToBoolean(row["is_liked_requester"]),
                        user: new BaseUser(
                            user_id: Guid.Parse(row["user_id"].ToString() ?? string.Empty),
                            username: row["username"]?.ToString() ?? string.Empty,
                            pfp_src: row["pfp_src"]?.ToString() ?? string.Empty,
                            is_private: Convert.ToBoolean(row["is_private"])
                        )
                    );

                    if (row["media_json"] != DBNull.Value)
                    {
                        string mediaJson = row["media_json"].ToString() ?? "[]";
                        List<MediaJsonDTO> mediaList = JsonSerializer.Deserialize<List<MediaJsonDTO>>(mediaJson) ?? new List<MediaJsonDTO>();

                        if (mediaList.Count > 0)
                        {
                            foreach (var media in mediaList)
                            {
                                if (Enum.TryParse<MediaType>(media.media_type.ToString(), true, out var parsedMediaType))
                                {
                                    var mediaObj = new PostMedia(
                                        post_id: post.post_id,
                                        media_id: media.media_id,
                                        media_type: parsedMediaType,
                                        media_src: media.media_src
                                    );

                                    post.AddMedia(mediaObj);
                                }
                            }
                        }
                    }

                    posts.Add(post);
                }

                return posts;
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during posts retrieval: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during posts retrieval: {ex.Message}", ex);
            }
        }

        public async Task LikePost(Guid post_id, Guid user_id)
        {
            try
            {
                await this.db_repo.nonQuery(this.postQuery.like_post(), new Dictionary<string, object> {
                    { "@post_id", post_id },
                    { "@user_id", user_id },
                    { "@created_at", DateTime.UtcNow }
                });
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during post liking: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during post liking: {ex.Message}", ex);
            }
        }

        public async Task DislikePost(Guid post_id, Guid user_id)
        {
            try
            {
                await this.db_repo.nonQuery(this.postQuery.dislike_post(), new Dictionary<string, object> {
                    { "@post_id", post_id },
                    { "@user_id", user_id }
                });
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during post disliking: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during post disliking: {ex.Message}", ex);
            }
        }
    }
}