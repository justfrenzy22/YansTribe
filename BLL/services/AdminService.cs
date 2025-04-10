using bll.dto;
using bll.interfaces;
using core.entities;
using core.enums;
using dal.exceptions;
using dal.interfaces.repo;

namespace bll.services
{



    public class AdminService : IAdminService
    {
        private readonly IUserRepo user_repo;
        private readonly IAdminRepo repo;
        private readonly IHashService hash_service;
        private readonly IAuthService auth_service;

        public AdminService(IUserRepo user_repo, IAdminRepo repo, IHashService hash_service, IAuthService auth_service)
        {
            this.user_repo = user_repo;
            this.repo = repo;
            this.hash_service = hash_service;
            this.auth_service = auth_service;
        }

        public async Task<string> ValidateLogin(string email, string password)
        {
            User? user = await this.user_repo.GetUserByEmail(email);

            if (user == null)
            {
                throw new DataAccessException("User or password is incorrect.");
            }

            if (user.role != Role.Admin && user.role != Role.SuperAdmin)
            {
                // throw new DataAccessException("User or password is incorrect.");
                throw new DataAccessException("User role is incorrect.");
            }

            string hash_password = this.hash_service.hash(password);

            if (hash_password != user.password)
            {
                throw new DataAccessException("User or password is incorrect.");
            }

            string token = this.auth_service.GenerateJwtToken(user.user_id.ToString() ?? "", isAdmin: true);

            return token;

        }
        public VerifyTokenRes AuthAdmin(string token) => this.auth_service.VerifyTokenAsync(token, isAdmin: true);

        public async Task<List<User>?> GetUsersAsync(int admin_id)
        {
            List<User>? users = await this.repo.GetAllUsersAsync(admin_id);
            return users;
        }
    }
}