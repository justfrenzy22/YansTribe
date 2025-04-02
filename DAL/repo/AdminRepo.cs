using System.Data;
using core.entities;
using core.enums;
using dal.requests;
using dal.responses;
using dal.interfaces;
using dal.queries;
using Microsoft.Data.SqlClient;

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

            DataTable? res = await this.dbRepo.reader(this.userQuery.get_users());

            if (res.Rows.Count == 0)
            {
                return new List<User>();
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

        public async Task<List<User>> GetStandardUsersAsync()
        {

            DataTable? res = await this.dbRepo.reader(this.userQuery.get_standard_users());

            if (res.Rows.Count == 0)
            {
                return new List<User>();
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


        public async Task<AdminLoginRes> ValidateLogin(AdminLoginReq model)
        {
            if (model.email == null || model.password == null)
            {
                return new AdminLoginRes { check = false };
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
                    return new AdminLoginRes { check = false };
                }

                var row = res.Rows[0];

                // int user_id = Convert.ToInt32(row["user_id"]);

                return new AdminLoginRes { check = true, user_id = Convert.ToInt32(row["user_id"]) };
            }

            catch (SqlException)
            {
                return new AdminLoginRes { check = false };
            }

            catch (Exception)
            {
                return new AdminLoginRes { check = false };
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
