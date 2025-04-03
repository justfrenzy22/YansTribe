using System.Data;
using core.entities;
using core.enums;
using dal.requests;
using dal.queries;
using Microsoft.Data.SqlClient;
using dal.exceptions;
using dal.interfaces.repo;
using dal.interfaces.db;

namespace dal.repo
{
    public class AdminRepo : BaseUserRepo, IAdminRepo
    {
        // private readonly string connString;
        private readonly IDBRepo dbRepo;
        private readonly AdminQuery adminQuery;
        private readonly UserQuery userQuery;

        public AdminRepo(IDBRepo dbRepo, AdminQuery adminQuery, UserQuery userQuery) : base(dbRepo)
        {
            this.dbRepo = dbRepo;
            // this.connString = connString;
            this.adminQuery = adminQuery;
            this.userQuery = userQuery;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {

            try
            {
                DataTable? res = await this.dbRepo.reader(this.userQuery.get_users());

                if (res.Rows.Count == 0)
                {
                    // return new List<User>();
                    throw new NotFoundException("No users found.");
                }

                return res.AsEnumerable().Select(row => new User(
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
                )).ToList();
            }
            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during all users retrieval: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Database error during all users retrieval: {ex.Message}", ex);
            }
        }

        public async Task<List<User>> GetStandardUsersAsync()
        {
            try
            {
                DataTable? res = await this.dbRepo.reader(this.userQuery.get_standard_users());

                if (res.Rows.Count == 0)
                {
                    // return new List<User>();
                    throw new NotFoundException("No standard users found.");
                }

                return res.AsEnumerable().Select(row => new User(
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
                )).ToList();
            }
            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during standard users retrieval: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Database error during standard users retrieval: {ex.Message}", ex);
            }
        }


        public async Task<int> ValidateLogin(AdminLoginReq model)
        {
            if (model.email == null || model.password == null)
            {
                // return new AdminLoginRes { check = false };
                throw new EmptyRequestDataException("Email and password are required.");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"@email", model.email},
                {"@password_hash", model.password}
            };

            try
            {
                DataTable? res = await dbRepo.reader(adminQuery.get_admin_login(), parameters);

                if (res.Rows.Count == 0)
                {
                    // return new AdminLoginRes { check = false };
                    throw new NotFoundException("Admin with this email was not found.");
                }

                var row = res.Rows[0];

                // int user_id = Convert.ToInt32(row["user_id"]);

                // return new AdminLoginRes { check = true, user_id = Convert.ToInt32(row["user_id"]) };
                return Convert.ToInt32(row["user_id"]);
            }

            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during admin validation: {sqlEx.Message}", sqlEx);
            }

            catch (Exception ex)
            {
                throw new DataAccessException($"Database error during admin validation: {ex.Message}", ex);
            }

        }



        // private Role ParseRole(string value)
        // {
        //     switch (value)
        //     {
        //         case "user":
        //             return Role.User;
        //         case "admin":
        //             return Role.Admin;
        //         case "superadmin":
        //             return Role.SuperAdmin;
        //         default:
        //             return Role.User;
        //     }
        // }
    }
}
