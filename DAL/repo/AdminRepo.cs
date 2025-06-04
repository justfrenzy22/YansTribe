using System.Data;
using core.entities;
using dal.queries;
using Microsoft.Data.SqlClient;
using dal.exceptions;
using dal.interfaces.repo;
using dal.interfaces.db;
using dal.dto;
using dal.mapper;

namespace dal.repo
{
    public class AdminRepo : BaseUserRepo, IAdminRepo
    {
        private readonly AdminQuery _admin_query;
        private readonly UserQuery _user_query;
        private readonly UserMapper _mapper;

        public AdminRepo(IDBRepo db_repo, AdminQuery admin_query, UserQuery user_query, UserMapper mapper) : base(db_repo)
        {
            this._admin_query = admin_query;
            this._user_query = user_query;
            this._mapper = mapper;
        }

        public async Task<List<UserCredentials>?> GetAllUsersAsync(Guid admin_id)
        {
            try
            {
                DataTable? res = await this._db_repo.reader(
                    this._admin_query.get_all_users(),
                    new Dictionary<string, object> {
                        {"@admin_id", admin_id}
                    }
                );
                return res.AsEnumerable()
                    .Select(row => this._mapper.MapTo(new UserDTO
                    {
                        user_id = Guid.Parse(row["user_id"]?.ToString() ?? string.Empty),
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
                    }))
                    .ToList();
            }
            catch (SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during all users retrieval: {sqlEx.Message}");
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"Database error during all users retrieval: {ex.Message}", ex);
            }
        }
    }
}
