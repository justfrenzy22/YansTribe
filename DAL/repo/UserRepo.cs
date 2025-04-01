using System.Data;
using core.entities;
using core.enums;
using dal.interfaces;
using dal.queries;

public class UserRepo : BaseUserRepo, IUserRepo
{
    private readonly IDBRepo dBRepo;
    private readonly UserQuery userQuery;

    public UserRepo(IDBRepo dbRepo, UserQuery userQuery) : base(dbRepo)
    {
        this.dBRepo = dbRepo;
        this.userQuery = userQuery;
    }

    public async Task<int> AddUser(User user)
    {
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
        return await dBRepo.nonQuery(userQuery.add_user(), parameters);
    }

    public async Task<Role?> GetRoleById(int user_id)
    {
        var parameters = new Dictionary<string, object> {
            { "@user_id", user_id }
        };

        DataTable? res = await dBRepo.reader(userQuery.get_role_by_id(), parameters);

        if (res.Rows.Count == 0)
        {
            return null;
        }

        var row = res.Rows[0];

        return ParseRole<Role>(row["role"].ToString() ?? "");
    }

    public async Task<User?> GetUserById(int user_id)
    {
        var parameters = new Dictionary<string, object> {
            { "@user_id", user_id }
        };

        DataTable? res = await dBRepo.reader(userQuery.get_user_by_id(), parameters);

        if (res.Rows.Count == 0)
        {
            return null;
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

        // return res.AsEnumerable().Select(row => new User(
        //         user_id: Convert.ToInt32(row["user_id"]),
        //         username: row["username"]?.ToString() ?? string.Empty,
        //         email: row["email"]?.ToString() ?? string.Empty,
        //         password_hash: row["password_hash"]?.ToString() ?? string.Empty,
        //         full_name: row["full_name"]?.ToString() ?? string.Empty,
        //         bio: row["bio"]?.ToString() ?? string.Empty,
        //         pfp_src: row["pfp_src"]?.ToString() ?? string.Empty,
        //         location: row["location"]?.ToString() ?? string.Empty,
        //         website: row["website"]?.ToString() ?? string.Empty,
        //         is_private: Convert.ToBoolean(row["is_private"]),
        //         created_at: Convert.ToDateTime(row["created_at"]),
        //         role: ParseRole(row["role"].ToString() ?? "")
        //     )).ToList();
    }

    public async Task<int?> ValidateUser(string email, string password)
    {
        var parameters = new Dictionary<string, object> {
            { "@email", email },
            { "@password_hash", password }
        };

        DataTable? res = await dBRepo.reader(userQuery.get_user_by_email_and_password(), parameters);

        if (res.Rows.Count == 0)
        {
            return null;
        }

        var row = res.Rows[0];

        return Convert.ToInt32(row["user_id"]);
    }
}