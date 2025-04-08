using System.Data;
using core.entities;
using core.enums;
using dal.exceptions;
using dal.interfaces;
using dal.interfaces.db;
using dal.interfaces.repo;
using dal.queries;
using dal.requests;
using dal.responses;
using Microsoft.Data.SqlClient;

public class UserRepo : BaseUserRepo, IUserRepo
{
    private readonly IDBRepo dBRepo;
    private readonly UserQuery userQuery;

    public UserRepo(IDBRepo dbRepo, UserQuery userQuery) : base(dbRepo)
    {
        this.dBRepo = dbRepo;
        this.userQuery = userQuery;
    }

    public async Task<int> RegisterUser(User user)
    {
        try
        {

            if (user == null)
            {
                throw new EmptyRequestDataException("User is required.");
            }

            var parameters = new Dictionary<string, object> {
            { "@username", user.username },
            { "@email", user.email },
            { "@password_hash", user.password_hash },
            { "@full_name", user.full_name },
            { "@bio", user.bio },
            { "@pfp_src", user.pfp_src },
            { "@location", user.location },
            { "@website", user.website }
        };

            object? result = await dBRepo.scalar(userQuery.add_user(), parameters);

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
        catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
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

    // public async Task<Role?> GetRoleById(UserGetRoleReq user_id)
    // {
    //     try
    //     {
    //         if (user_id == null)
    //         {
    //             throw new EmptyRequestDataException("User id is required.");
    //         }

    //         var parameters = new Dictionary<string, object> {
    //             { "@user_id", user_id }
    //         };

    //         DataTable? res = await dBRepo.reader(userQuery.get_role_by_id(), parameters);

    //         if (res.Rows.Count == 0)
    //         {
    //             throw new NotFoundException("User with this id was not found.");
    //         }

    //         var row = res.Rows[0];

    //         return ParseRole<Role>(row["role"].ToString() ?? "");
    //     }
    //     catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
    //     {
    //         throw new DatabaseOperationException($"Database error during role retrieval: {sqlEx.Message}", sqlEx);
    //     }
    //     catch (Exception ex)
    //     {
    //         throw new DataAccessException($"An unexpected error occurred during role retrieval: {ex.Message}", ex);
    //     }
    // }


    public async Task<User?> GetUserById(int user_id)
    {
        try
        {
            var parameters = new Dictionary<string, object> {
                { "@user_id", Convert.ToInt32(user_id) }
            };

            DataTable? res = await dBRepo.reader(userQuery.get_user_by_id(), parameters);

            if (res.Rows.Count == 0)
            {
                throw new NotFoundException("User with this id was not found.");
            }

            var row = res.Rows[0];

            return new User(
                user_id: Convert.ToInt32(row["user_id"]),
                username: row["username"]?.ToString() ?? string.Empty,
                email: row["email"]?.ToString() ?? string.Empty,
                password_hash: row["password_hash"]?.ToString() ?? string.Empty,
                full_name: row["full_name"]?.ToString() ?? string.Empty,
                bio: row["bio"]?.ToString() ?? string.Empty,
                pfp_src: row["pfp_src"]?.ToString() ?? string.Empty,
                location: row["location"]?.ToString() ?? string.Empty,
                website: row["website"]?.ToString() ?? string.Empty,
                is_private: Convert.ToBoolean(row["is_private"]),
                created_at: Convert.ToDateTime(row["created_at"]),
                role: ParseRole<Role>(row["role"].ToString() ?? "")
            );

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

    public async Task<int> ValidateUser(string email, string password)
    {
        try
        {
            var parameters = new Dictionary<string, object> {
                { "@email", email },
                { "@password_hash", password }
            };

            DataTable? res = await dBRepo.reader(userQuery.get_user_by_email_and_password(), parameters);

            if (res.Rows.Count == 0)
            {
                throw new DataAccessException("User or password is incorrect.");
            }

            var row = res.Rows[0];

            return Convert.ToInt32(row["user_id"]);
        }
        catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
        {
            throw new DatabaseOperationException($"Database error during user validation: {sqlEx.Message}", sqlEx);
        }
        catch (Exception)
        {
            throw new DataAccessException("User or password is incorrect.");
        }
    }


}