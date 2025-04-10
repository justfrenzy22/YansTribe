using bll.interfaces;
using core.entities;
using dal.exceptions;
using dal.interfaces.repo;

namespace bll.services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo repo;
        private readonly IHashService hash_service;
        private readonly IAuthService auth_service;
        public UserService(IUserRepo repo, IHashService hash_service, IAuthService auth_service)
        {

            this.repo = repo;
            this.hash_service = hash_service;
            this.auth_service = auth_service;
        }
        public async Task<string> ValidateUser(string email, string password)
        {
            User? user = await this.repo.GetUserByEmail(email);

            if (user == null)
            {
                throw new DataAccessException("User or password is incorrect.");
            }

            string hash_password = this.hash_service.hash(password);

            if (hash_password != user.password)
            {
                throw new DataAccessException("User or password is incorrect.");
            }

            string token = this.auth_service.GenerateJwtToken(user.user_id.ToString() ?? "", isAdmin: false);

            return token;
        }

        public async Task<int?> RegisterUser(User user)
        {
            User? userEmail = await this.repo.GetUserByEmail(user.email);

            if (userEmail != null)
            {
                throw new DataAccessException("User with this email already exists.");
            }

            User? userUsername = await this.repo.GetUserByUsername(user.username);

            if (userUsername != null)
            {
                throw new DataAccessException("User with this username already exists.");
            }

            string hash_password = this.hash_service.hash(user.password);

            user.HashPassword(hash_password);

            int? user_id = await this.repo.RegisterUser(user);

            if (user_id == null || user_id == -1)
            {
                throw new DataAccessException("User with this email or username already exists.");
            }

            return user_id;
        }

        public async Task<User?> GetUserById(int user_id) => await this.repo.GetUserById(user_id);
    }
}