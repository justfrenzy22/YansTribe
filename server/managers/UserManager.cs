using System.Data;
using core.entities;
using core.enums;
using Microsoft.Data.SqlClient;


namespace server.managers
{

    public class UserManager : DatabaseManager
    {

        private UserQuery query;

        public UserManager(string db_conn) : base(db_conn)
        {
            this.query = new UserQuery();
        }

        public int create_user(string username, string email, string password_hash, string full_name, string bio, string pfp_src, string location, string website)
        {
            try
            {

                if (get_user_by_username(username) != null || get_user_by_email(email) != null)
                {
                    throw new Exception("User with the same email or username already exists");
                }

                Dictionary<string, object> parameters = new Dictionary<string, object> {
                    {"@username", username},
                    {"@email", email},
                    {"@password_hash", password_hash},
                    {"@full_name", full_name},
                    {"@bio", bio},
                    {"@pfp_src", pfp_src},
                    {"@location", location},
                    {"@website", website},
                };

                // Execute the insert query and get the inserted ID
                var insertResult = scalar(query.add_user(), parameters);
                if (insertResult == null || insertResult == DBNull.Value)
                {
                    throw new Exception("Failed to insert user into the database.");
                }

                return Convert.ToInt32(insertResult);
            }
            catch (SqlException ex)
            {
                throw new Exception($"Database error in create_user: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error in create_user: {ex.Message}", ex);
            }
        }

        public int get_last_id()
        {
            try
            {
                var lastIDResult = scalar(query.get_last_id());

                if (lastIDResult == null || lastIDResult == DBNull.Value)
                {
                    throw new Exception($"Failed to retrieve the last inserted user id");
                }

                return Convert.ToInt32(lastIDResult);
            }
            catch (SqlException e)
            {
                throw new Exception($"Database error in get_last_id: {e.Message}", e);
            }

            catch (Exception e)
            {
                throw new Exception($"Unexpected error in get_last_id: {e.Message}", e);
            }
        }

        public User? get_user_by_id(int user_id)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"@user_id", user_id},
            };

                DataTable res = reader(query.get_user_by_id(), parameters);

                if (res.Rows.Count == 0)
                {
                    return null;
                }

                return new User(
                    user_id: Convert.ToInt32(res.Rows[0]["user_id"]),
                    username: res.Rows[0]["username"]?.ToString() ?? string.Empty,
                    email: res.Rows[0]["email"]?.ToString() ?? string.Empty,
                    password_hash: res.Rows[0]["password_hash"]?.ToString() ?? string.Empty,
                    full_name: res.Rows[0]["full_name"]?.ToString() ?? string.Empty,
                    bio: res.Rows[0]["bio"]?.ToString() ?? string.Empty,
                    pfp_src: res.Rows[0]["pfp_src"]?.ToString() ?? string.Empty,
                    location: res.Rows[0]["location"]?.ToString() ?? string.Empty,
                    website: res.Rows[0]["website"]?.ToString() ?? string.Empty,
                    is_private: Convert.ToBoolean(res.Rows[0]["is_private"]),
                    created_at: Convert.ToDateTime(res.Rows[0]["created_at"]),
                    role: (Role)Convert.ToInt32(res.Rows[0]["role"])
                );
            }
            catch (Exception e)
            {
                throw new Exception($"Error getting user by id: {user_id}: {e.Message}");
            }
        }

        public User? get_user_by_username(string username)
        {
            try
            {

                Dictionary<string, object> parameters = new Dictionary<string, object> {
                    {"@username", username},
                };

                DataTable res = reader(query.get_user_by_username(), parameters);

                if (res.Rows.Count == 0)
                {
                    return null;
                }

                DataRow row = res.Rows[0];

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
                    role: ParseRole(row["role"].ToString() ?? "")
                );


            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting user by username: {username}: {ex.Message}");
            }
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

        public User? get_user_by_email(string email)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object> {
            {"@email", email},
        };

                DataTable res = reader(query.get_user_by_email(), parameters);

                if (res.Rows.Count == 0)
                {
                    return null;
                }

                DataRow row = res.Rows[0];

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
                    role: ParseRole(row["role"].ToString() ?? "")
                );
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting user by email: {email}: {ex.Message}");
            }
        }

        public void delete_user(int user_id)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object> {
                {"@user_id", user_id},
            };
                nonQuery(query.delete_user(), parameters);
                // return scalar(query.delete_user(), parameters) != null ? Convert.ToInt32(scalar(query.get_last_insert_id())) : 0;
            }
            catch
            {
                throw new Exception("Failed to delete user");
            }
        }
    }
}