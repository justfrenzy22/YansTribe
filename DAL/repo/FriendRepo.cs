using System.Data;
using core.entities;
using dal.exceptions;
using dal.interfaces.db;
using dal.interfaces.repo;
using dal.mapper;
using dal.queries;
using Microsoft.Data.SqlClient;

namespace dal.repo
{

    public class FriendRepo : BaseUserRepo, IFriendRepo
    {

        private readonly FriendQuery _friend_query;

        public FriendRepo(IDBRepo db_repo, FriendQuery friend_query) : base(db_repo)
        {
            this._friend_query = friend_query;
        }

        public async Task AddFriend(Guid req_user_id, Guid user2_id)
        {
            try
            {
                await this._db_repo.nonQuery(this._friend_query.add_friend(), new Dictionary<string, object> {
                    { "@req_user_id", Guid.Parse(req_user_id.ToString() ?? "") },
                    { "@user2", Guid.Parse(user2_id.ToString() ?? "") }
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

        public async Task<string?> RemoveFriend(Guid req_user_id, Guid user2_id)
        {
            try
            {
                await this._db_repo.nonQuery(this._friend_query.remove_friend(), new Dictionary<string, object> {
                    { "@req_user_id", Guid.Parse(req_user_id.ToString() ?? "") },
                    { "@user2", Guid.Parse(user2_id.ToString() ?? "") }
                });

                return "Friendship removed.";
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

        public async Task<string?> CancelFriend(Guid req_user_id, Guid user2_id)
        {
            try
            {
                await this._db_repo.nonQuery(this._friend_query.cancel_friend(), new Dictionary<string, object> {
                    { "@req_user_id", Guid.Parse(req_user_id.ToString() ?? "") },
                    { "@user2", Guid.Parse(user2_id.ToString() ?? "") }
                });

                return "Friendship cancelled.";
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

        public async Task<Friend?> GetFriendshipStatus(Guid req_user_id, Guid friend_id)
        {
            try
            {
                DataTable? res = await this._db_repo.reader(this._friend_query.get_friendship_status(), new Dictionary<string, object> {
                    { "@req_user_id", Guid.Parse(req_user_id.ToString() ?? "") },
                    { "@friend_id", Guid.Parse(friend_id.ToString() ?? "") }
                });

                if (res.Rows.Count == 0)
                {
                    return null;
                }

                var row = res.Rows[0];

                return new Friend(
                    friendship_id: Guid.Parse(row["friendship_id"].ToString() ?? ""),
                    user_1_id: Guid.Parse(row["user_1_id"].ToString() ?? ""),
                    user_2_id: Guid.Parse(row["user_2_id"].ToString() ?? ""),
                    status: ParseFriendStatus(row["status"].ToString() ?? ""),
                    created_at: Convert.ToDateTime(row["created_at"])
                );
            }
            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during friendship status check: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during friendship status check: {ex.Message}", ex);
            }
        }

        public async Task AcceptFriend(Guid friendship_id)
        {
            try
            {
                await this._db_repo.nonQuery(this._friend_query.accept_friend(), new Dictionary<string, object> {
                    { "@friendship_id", Guid.Parse(friendship_id.ToString() ?? "") }
                });
            }
            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during friendship acceptance: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during friendship acceptance: {ex.Message}", ex);
            }
        }

        public async Task RejectFriend(Guid friendship_id)
        {
            try
            {
                await this._db_repo.nonQuery(this._friend_query.reject_friend(), new Dictionary<string, object> {
                    { "@friendship_id", Guid.Parse(friendship_id.ToString() ?? "") }
                });
            }
            catch (Exception ex) when (ex.InnerException is SqlException sqlEx)
            {
                throw new DatabaseOperationException($"Database error during friendship rejection: {sqlEx.Message}", sqlEx);
            }
            catch (Exception ex)
            {
                throw new DataAccessException($"An unexpected error occurred during friendship rejection: {ex.Message}", ex);
            }
        }
    }
}