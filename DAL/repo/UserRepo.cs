using System.Data;
using System.Text.Json;
using core.entities;
using core.enums;
using dal.dto;
using dal.exceptions;
using dal.interfaces.db;
using dal.interfaces.repo;
using dal.mapper;
using dal.queries;
using Microsoft.Data.SqlClient;

namespace dal.repo
{

    public class UserRepo : BaseUserRepo, IUserRepo
    {
        private readonly UserQuery _user_query;
        private readonly UserMapper _mapper;

        public UserRepo(IDBRepo db_repo, UserQuery user_query, UserMapper mapper) : base(db_repo)
        {
            this._user_query = user_query;
            this._mapper = mapper;
        }

        public async Task<Guid> RegisterUser(UserCredentials user)
        {
            try
            {
                object? result = await this._db_repo.scalar(this._user_query.add_user(), new Dictionary<string, object> {
                    { "@username", user.username },
                    { "@email", user.email },
                    { "@password_hash", user.password },
                    { "@full_name", user.full_name },
                    { "@bio", user.bio },
                    { "@location", user.location },
                    { "@website", user.website },
                    { "@role", user.role.ToString() },
                    { "@is_private", user.is_private },
                    { "@created_at", user.created_at.ToString() },
                });

                if (result != null && result != DBNull.Value)
                {
                    return Guid.Parse(result.ToString() ?? "");
                }
                else
                {
                    throw new DatabaseOperationException("User creation query executed but did not return the executed user_id.");
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                {
                    string message = sqlEx.Message.ToLowerInvariant();

                    string field = "Unknown";
                    if (message.Contains("username"))
                    {
                        field = "username";
                    }

                    else if (message.Contains("email"))
                    {
                        field = "email";
                    }

                    throw new DuplicateUserException(
                        $"A user with the same {field.ToLower()} already exists.",
                        field,
                        sqlEx
                    );
                }
                else
                {
                    throw new DatabaseOperationException($"Database error during user creation: {sqlEx.Message}", sqlEx);
                }
            }

            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during user creation: {ex.Message}", ex);
            }
        }

        public async Task<UserDetails?> GetUserById(Guid user_id)
        {
            try
            {
                DataTable? res = await this._db_repo.reader(this._user_query.get_user_by_id(), new Dictionary<string, object> {
                { "@user_id", Guid.Parse(user_id.ToString() ?? "") }
                });

                if (res.Rows.Count == 0)
                {
                    return null;
                }
                var row = res.Rows[0];
                return this._mapper.MapTo(new UserDetailsDTO
                {
                    user_id = Guid.Parse(row["user_id"].ToString() ?? ""),
                    username = row["username"]?.ToString() ?? string.Empty,
                    email = row["email"]?.ToString() ?? string.Empty,
                    full_name = row["full_name"]?.ToString() ?? string.Empty,
                    bio = row["bio"]?.ToString() ?? string.Empty,
                    pfp_src = row["pfp_src"]?.ToString() ?? string.Empty,
                    location = row["location"]?.ToString() ?? string.Empty,
                    website = row["website"]?.ToString() ?? string.Empty,
                    is_private = Convert.ToBoolean(row["is_private"]),
                    created_at = Convert.ToDateTime(row["created_at"]),
                    role = ParseRole(row["role"].ToString() ?? ""),
                });
            }

            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during user retrieval: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during user retrieval: {ex.Message}", ex);
            }
        }

        public async Task<UserProfile?> GetUserProfileById(Guid req_user_id, Guid profile_user_id)
        {
            try
            {
                DataTable? res = await this._db_repo.reader(this._user_query.get_user_profile_by_id(), new Dictionary<string, object> {
                    { "@profile_user_id", Guid.Parse(profile_user_id.ToString() ?? "") },
                    { "@req_user_id", Guid.Parse(req_user_id.ToString() ?? "") }
                });

                if (res.Rows.Count == 0)
                {
                    return null;
                }
                var row = res.Rows[0];

                bool is_private = Convert.ToBoolean(row["is_private"]);

                if (is_private)
                {
                    return (UserProfile)this._mapper.MapTo(new EssentialsUserDTO
                    {
                        user_id = Guid.Parse(row["user_id"].ToString() ?? ""),
                        username = row["username"]?.ToString() ?? string.Empty,
                        pfp_src = row["pfp_src"]?.ToString() ?? string.Empty,
                        is_private = is_private
                    });
                }
                else
                {
                    UserProfile user = this._mapper.MapTo(new ProfileUserDTO
                    {
                        user_id = Guid.Parse(row["user_id"].ToString() ?? ""),
                        username = row["username"]?.ToString() ?? string.Empty,
                        pfp_src = row["pfp_src"]?.ToString() ?? string.Empty,
                        email = row["email"]?.ToString() ?? string.Empty,
                        full_name = row["full_name"]?.ToString() ?? string.Empty,
                        bio = row["bio"]?.ToString() ?? string.Empty,
                        location = row["location"]?.ToString() ?? string.Empty,
                        website = row["website"]?.ToString() ?? string.Empty,
                        is_private = Convert.ToBoolean(row["is_private"]),
                        created_at = Convert.ToDateTime(row["created_at"]),
                        role = ParseRole(row["role"].ToString() ?? ""),
                        friends_num = Convert.ToInt32(row["friends_num"]),
                        relationship_info = new RelationShipInfo(
                            is_self: Convert.ToBoolean(row["is_self"]),
                            is_friend: Convert.ToBoolean(row["is_friend"]),
                            friendship_status: row["friendship_status"] != DBNull.Value ? ParseFriendStatus(row["friendship_status"].ToString() ?? "") : null,
                            request_direction: row["request_direction"]?.ToString() ?? string.Empty
                        )
                    });

                    if (row["friends_json"] != DBNull.Value)
                    {
                        string rawJson = row["friends_json"].ToString() ?? "[]";

                        List<FriendUserDTO> friendsJson = JsonSerializer.Deserialize<List<FriendUserDTO>>(rawJson) ?? new List<FriendUserDTO>();

                        foreach (var friendDto in friendsJson)
                        {
                            if (!Enum.TryParse(friendDto.status, true, out FriendShipStatus status))
                                continue;
                            Friendship friendUser = new Friendship(
                                status,
                                friendDto.user_id,
                                friendDto.username,
                                friendDto.pfp_src,
                                friendDto.is_private
                            );
                            user.AddFriend(friendUser);
                        }
                    }
                    return user;
                }
            }

            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during user retrieval: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during user retrieval: {ex.Message}", ex);
            }
        }

        public async Task<List<FriendNotification>?> GetFriendNotifications(Guid user_id)
        {
            try
            {
                DataTable? res = await this._db_repo.reader(this._user_query.get_friend_notifications(), new Dictionary<string, object>
                {
                    { "@user_id", Guid.Parse(user_id.ToString() ?? "") }
                });

                if (res.Rows.Count == 0)
                {
                    return null;
                }

                List<FriendNotification> frNot = new List<FriendNotification>();

                foreach (DataRow row in res.Rows)
                {
                    frNot.Add(new FriendNotification(
                        sender: new UserAccount(
                            user_id: Guid.Parse(row["sender_id"].ToString() ?? string.Empty),
                            username: row["username"].ToString() ?? string.Empty,
                            pfp_src: row["pfp_src"].ToString() ?? string.Empty,
                            is_private: Convert.ToBoolean(row["is_private"])
                        ),
                        request_sent_at: Convert.ToDateTime(row["request_sent_at"])
                    ));
                }

                return frNot;
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during user retrieval: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during user retrieval: {ex.Message}", ex);
            }
        }

        public async Task<UserAccount?> GetUserEssentials(Guid user_id)
        {
            try
            {
                DataTable? res = await this._db_repo.reader(this._user_query.get_user_essentials_by_id(), new Dictionary<string, object> {
                    { "@user_id", Guid.Parse(user_id.ToString() ?? "") }
                });

                if (res.Rows.Count == 0)
                {
                    return null;
                }

                var row = res.Rows[0];

                return this._mapper.MapTo(new EssentialsUserDTO
                {
                    user_id = Guid.Parse(row["user_id"]?.ToString() ?? ""),
                    username = row["username"]?.ToString() ?? string.Empty,
                    pfp_src = row["pfp_src"]?.ToString() ?? string.Empty,
                    is_private = row["is_private"] == DBNull.Value ? false : Convert.ToBoolean(row["is_private"])
                });
            }
            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during user retrieval: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during user retrieval: {ex.Message}", ex);
            }
        }

        public async Task<UserCredentials?> GetUserByEmail(string email)
        {
            try
            {
                DataTable? res = await this._db_repo.reader(this._user_query.get_user_by_email(), new Dictionary<string, object> {
                { "@email", email }
            });

                if (res.Rows.Count == 0)
                {
                    return null;
                }

                var row = res.Rows[0];

                return this._mapper.MapTo(new UserDTO
                {
                    user_id = Guid.Parse(row["user_id"]?.ToString() ?? ""),
                    username = row["username"]?.ToString() ?? string.Empty,
                    email = row["email"]?.ToString() ?? string.Empty,
                    password = row["password_hash"]?.ToString() ?? string.Empty,
                    full_name = row["full_name"]?.ToString() ?? string.Empty,
                    bio = row["bio"]?.ToString() ?? string.Empty,
                    pfp_src = row["pfp_src"]?.ToString() ?? string.Empty,
                    location = row["location"]?.ToString() ?? string.Empty,
                    website = row["website"]?.ToString() ?? string.Empty,
                    is_private = Convert.ToBoolean(row["is_private"]),
                    created_at = Convert.ToDateTime(row["created_at"]),
                    role = ParseRole(row["role"].ToString() ?? "")
                });
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during user retrieval by email: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during user retrieval by email: {ex.Message}", ex);
            }
        }

        public async Task<Guid?> GetUserIdByUsername(string username)
        {
            try
            {
                DataTable? res = await this._db_repo.reader(this._user_query.check_user_by_username(), new Dictionary<string, object> {
                { "@username", username }
            });

                if (res.Rows.Count == 0)
                {
                    return null;
                }

                var row = res.Rows[0];

                return Guid.Parse(row["user_id"]?.ToString() ?? "");
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during user retrieval by email: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during user retrieval by email: {ex.Message}", ex);
            }
        }

        public async Task<UserCredentials?> ValidateUserByEmail(string email)
        {
            try
            {

                DataTable? res = await this._db_repo.reader(this._user_query.get_user_by_email(), new Dictionary<string, object> {
                { "@email", email }
            });

                if (res.Rows.Count == 0)
                {
                    return null;
                }

                var row = res.Rows[0];

                return this._mapper.MapTo(new UserDTO
                {
                    user_id = Guid.Parse(row["user_id"]?.ToString() ?? ""),
                    username = row["username"]?.ToString() ?? string.Empty,
                    email = row["email"]?.ToString() ?? string.Empty,
                    password = row["password_hash"]?.ToString() ?? string.Empty,
                    full_name = row["full_name"]?.ToString() ?? string.Empty,
                    bio = row["bio"]?.ToString() ?? string.Empty,
                    pfp_src = row["pfp_src"]?.ToString() ?? string.Empty,
                    location = row["location"]?.ToString() ?? string.Empty,
                    website = row["website"]?.ToString() ?? string.Empty,
                    is_private = Convert.ToBoolean(row["is_private"]),
                    created_at = Convert.ToDateTime(row["created_at"]),
                    role = ParseRole(row["role"].ToString() ?? "")
                });
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during user retrieval by email: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during user retrieval by email: {ex.Message}", ex);
            }
        }

        public async Task<UserCredentials?> GetUserByUsername(string username)
        {
            try
            {

                DataTable? res = await this._db_repo.reader(this._user_query.get_user_by_username(), new Dictionary<string, object> {
                    { "@username", username }
                });

                if (res.Rows.Count == 0)
                {
                    return null;
                }

                var row = res.Rows[0];

                return this._mapper.MapTo(
                    new UserDTO
                    {
                        user_id = Guid.Parse(row["user_id"]?.ToString() ?? ""),
                        username = row["username"]?.ToString() ?? string.Empty,
                        password = row["password_hash"]?.ToString() ?? string.Empty,
                        email = row["email"]?.ToString() ?? string.Empty,
                        full_name = row["full_name"]?.ToString() ?? string.Empty,
                        bio = row["bio"]?.ToString() ?? string.Empty,
                        pfp_src = row["pfp_src"]?.ToString() ?? string.Empty,
                        location = row["location"]?.ToString() ?? string.Empty,
                        website = row["website"]?.ToString() ?? string.Empty,
                        is_private = Convert.ToBoolean(row["is_private"]),
                        created_at = Convert.ToDateTime(row["created_at"]),
                        role = ParseRole(row["role"].ToString() ?? "")
                    }
                );
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during user retrieval by email: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during user retrieval by email: {ex.Message}", ex);
            }
        }

        public async Task<bool> ChangeRole(Guid user_id, string role)
        {
            int result = await this._db_repo.nonQuery(this._user_query.update_user_role(), new Dictionary<string, object> {
                { "@user_id", Guid.Parse(user_id.ToString() ?? "") },
                { "@role", role }
            });

            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}