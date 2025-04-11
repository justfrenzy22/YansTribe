using System.Data;
using core.entities;
using core.enums;
using dal.dto;
using dal.exceptions;
using dal.interfaces;
using dal.interfaces.db;
using dal.interfaces.repo;
using dal.queries;
using Microsoft.Data.SqlClient;

namespace dal.repo
{


    public class UserRepo : BaseUserRepo, IUserRepo
    {
        // private readonly IDBRepo db_repo;
        private readonly UserQuery userQuery;

        public UserRepo(IDBRepo db_repo, UserQuery userQuery) : base(db_repo)
        {
            // this.db_repo = db_repo;
            this.userQuery = userQuery;
        }

        public async Task<int> RegisterUser(User user)
        {
            try
            {
                //     var parameters = new Dictionary<string, object> {
                //     { "@username", user.username },
                //     { "@email", user.email },
                //     { "@password_hash", user.password },
                //     { "@full_name", user.full_name },
                //     { "@bio", user.bio },
                //     { "@location", user.location },
                //     { "@website", user.website },
                //     { "@role", user.role.ToString() },
                //     { "@is_private", user.is_private },
                //     { "@created_at", user.created_at.ToString() },
                // };

                object? result = await this.db_repo.scalar(userQuery.add_user(), new Dictionary<string, object> {
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
                    if (int.TryParse(result.ToString(), out int user_id))
                    {
                        return user_id;
                    }
                    return -1;
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

        public async Task<UserDTO?> GetUserById(int user_id)
        {
            try
            {
                // var parameters = new Dictionary<string, object> {
                //     { "@user_id", Convert.ToInt32(user_id) }
                // };

                DataTable? res = await this.db_repo.reader(userQuery.get_user_by_id(), new Dictionary<string, object> {
                { "@user_id", Convert.ToInt32(user_id) }
            });

                if (res.Rows.Count == 0)
                {
                    throw new NotFoundException("User with this id was not found.");
                }

                var row = res.Rows[0];

                return new UserDTO
                {
                    user_id = Convert.ToInt32(row["user_id"]),
                    username = row["username"]?.ToString() ?? string.Empty,
                    email = row["email"]?.ToString() ?? string.Empty,
                    full_name = row["full_name"]?.ToString() ?? string.Empty,
                    bio = row["bio"]?.ToString() ?? string.Empty,
                    pfp_src = row["pfp_src"]?.ToString() ?? string.Empty,
                    location = row["location"]?.ToString() ?? string.Empty,
                    website = row["website"]?.ToString() ?? string.Empty,
                    is_private = Convert.ToBoolean(row["is_private"]),
                    created_at = Convert.ToDateTime(row["created_at"]),
                    role = ParseRole(row["role"].ToString() ?? "")
                };

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

        public async Task<UserDTO?> GetUserByEmail(string email)
        {
            try
            {
                // var parameters = new Dictionary<string, object> {
                //     { "@email", email }
                // };

                DataTable? res = await this.db_repo.reader(userQuery.get_user_by_email(), new Dictionary<string, object> {
                { "@email", email }
            });

                if (res.Rows.Count == 0)
                {
                    throw new NotFoundException("User with this email was not found.");
                }

                var row = res.Rows[0];

                return new UserDTO
                {
                    user_id = Convert.ToInt32(row["user_id"]),
                    username = row["username"]?.ToString() ?? string.Empty,
                    email = row["email"]?.ToString() ?? string.Empty,
                    full_name = row["full_name"]?.ToString() ?? string.Empty,
                    bio = row["bio"]?.ToString() ?? string.Empty,
                    pfp_src = row["pfp_src"]?.ToString() ?? string.Empty,
                    location = row["location"]?.ToString() ?? string.Empty,
                    website = row["website"]?.ToString() ?? string.Empty,
                    is_private = Convert.ToBoolean(row["is_private"]),
                    created_at = Convert.ToDateTime(row["created_at"]),
                    role = ParseRole(row["role"].ToString() ?? "")
                };
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

        public async Task<User?> ValidateUserByEmail(string email)
        {
            try
            {
                // var parameters = new Dictionary<string, object> {
                //     { "@email", email }
                // };

                DataTable? res = await this.db_repo.reader(userQuery.get_user_by_email(), new Dictionary<string, object> {
                { "@email", email }
            });

                if (res.Rows.Count == 0)
                {
                    return null;
                }

                var row = res.Rows[0];

                return new User(
                    user_id: Convert.ToInt32(row["user_id"]),
                    username: row["username"]?.ToString() ?? string.Empty,
                    email: row["email"]?.ToString() ?? string.Empty,
                    password: row["password_hash"]?.ToString() ?? string.Empty,
                    full_name: row["full_name"]?.ToString() ?? string.Empty,
                    bio: row["bio"]?.ToString() ?? string.Empty,
                    pfp_src: row["pfp_src"]?.ToString() ?? string.Empty,
                    location: row["location"]?.ToString() ?? string.Empty,
                    website: row["website"]?.ToString() ?? string.Empty,
                    is_private: Convert.ToBoolean(row["is_private"]),
                    created_at: Convert.ToDateTime(row["created_at"]),
                    role: ParseRole(row["role"].ToString() ?? "")
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

        public async Task<UserDTO?> GetUserByUsername(string username)
        {
            try
            {
                // var parameters = new Dictionary<string, object> {
                //     { "@username", username }
                // };

                DataTable? res = await this.db_repo.reader(userQuery.get_user_by_username(), new Dictionary<string, object> {
                    { "@username", username }
                });

                if (res.Rows.Count == 0)
                {
                    throw new NotFoundException("User with this email was not found.");
                }

                var row = res.Rows[0];

                return new UserDTO
                {
                    user_id = Convert.ToInt32(row["user_id"]),
                    username = row["username"]?.ToString() ?? string.Empty,
                    email = row["email"]?.ToString() ?? string.Empty,
                    full_name = row["full_name"]?.ToString() ?? string.Empty,
                    bio = row["bio"]?.ToString() ?? string.Empty,
                    pfp_src = row["pfp_src"]?.ToString() ?? string.Empty,
                    location = row["location"]?.ToString() ?? string.Empty,
                    website = row["website"]?.ToString() ?? string.Empty,
                    is_private = Convert.ToBoolean(row["is_private"]),
                    created_at = Convert.ToDateTime(row["created_at"]),
                    role = ParseRole(row["role"].ToString() ?? "")
                };
                // return new User(
                //     user_id: Convert.ToInt32(row["user_id"]),
                //     username: row["username"]?.ToString() ?? string.Empty,
                //     email: row["email"]?.ToString() ?? string.Empty,
                //     password: row["password_hash"]?.ToString() ?? string.Empty,
                //     full_name: row["full_name"]?.ToString() ?? string.Empty,
                //     bio: row["bio"]?.ToString() ?? string.Empty,
                //     pfp_src: row["pfp_src"]?.ToString() ?? string.Empty,
                //     location: row["location"]?.ToString() ?? string.Empty,
                //     website: row["website"]?.ToString() ?? string.Empty,
                //     is_private: Convert.ToBoolean(row["is_private"]),
                //     created_at: Convert.ToDateTime(row["created_at"]),
                //     role: ParseRole(row["role"].ToString() ?? "")
                // );
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



        public async Task<bool> ChangeRole(int user_id, string role)
        {


            // string roleStr = ParseStringRole(role);

            int result = await db_repo.nonQuery(userQuery.update_user_role(), new Dictionary<string, object> {
                { "@user_id", user_id.ToString() },
                { "@role", role }
            });

            return result > 0;
        }

        // public async Task<int> ValidateUser(string email, string password)
        // {
        //     try
        //     {
        //         // var parameters = new Dictionary<string, object> {
        //         //     { "@email", email },
        //         //     { "@password_hash", password }
        //         // };

        //         DataTable? res = await dBRepo.reader(userQuery.get_user_by_email_and_password(), new Dictionary<string, object> {
        //             { "@email", email },
        //         });

        //         if (res.Rows.Count == 0)
        //         {
        //             throw new DataAccessException("User or password is incorrect.");
        //         }

        //         var row = res.Rows[0];

        //         return Convert.ToInt32(row["user_id"]);
        //     }
        //     catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
        //     {
        //         throw new DatabaseOperationException($"Database error during user validation: {sqlEx.Message}", sqlEx);
        //     }
        //     catch (Exception)
        //     {
        //         throw new DataAccessException("User or password is incorrect.");
        //     }
        // }


    }

}