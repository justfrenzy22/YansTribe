using System.Data;
using System.Threading.Tasks;
using core.entities;
using core.enums;
using core.requests;
using core.responses;
using dal.interfaces;
using dal.queries;

namespace dal.repo
{
    public class AdminRepo : IAdminRepo
    {
        // private readonly string connString;
        private readonly IDBRepo dbRepo;
        private readonly AdminQuery adminQuery;
        private readonly UserQuery userQuery;

        public AdminRepo(IDBRepo dbRepo, AdminQuery adminQuery, UserQuery userQuery)
        {
            this.dbRepo = dbRepo;
            // this.connString = connString;
            this.adminQuery = adminQuery;
            this.userQuery = userQuery;
        }

        public async Task<List<User>> GetUsers()
        {

            DataTable? res = await dbRepo.reader(userQuery.get_users());

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
                role: ParseRole(row["role"].ToString() ?? "")
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
                {"@password", model.password}
            };

            DataTable? res = await dbRepo.reader(adminQuery.get_admin_login(), parameters);

            if (res.Rows.Count == 0)
            {
                return new AdminLoginRes { check = false };
            }

            // !TODO Add JWT Token integration
            string token = "token";

            return new AdminLoginRes { check = true, token = token };
        }



        private Role ParseRole(string value)
        {
            switch (value)
            {
                case "user":
                    return Role.User;
                case "admin":
                    return Role.Admin;
                case "superadmin":
                    return Role.SuperAdmin;
                default:
                    return Role.User;
            }
        }
    }
}