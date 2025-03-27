using core.entities;
using core.enums;
using dal.interfaces;

namespace dal.user
{
    public class UserService : IUserService
    {
        private readonly IUserRepo repo;

        public UserService(IUserRepo repo) => this.repo = repo;

        public async Task<int> AddUser(User user) => await repo.AddUser(user);

        public async Task<User?> GetUserById(int user_id) => await repo.GetUserById(user_id);

        public async Task<Role?> GetRoleById(int user_id) => await repo.GetRoleById(user_id);
    }
}