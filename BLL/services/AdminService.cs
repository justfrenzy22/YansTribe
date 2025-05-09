using System.Reflection.Metadata.Ecma335;
using bll.dto;
using bll.interfaces;
using core.entities;
using core.enums;
using dal.dto;
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

        public async Task<string?> ValidateLogin(string email, string password)
        {
            User? user = await this.user_repo.ValidateUserByEmail(email);

            if (user == null)
            {
                return null;
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

        public async Task<VerifySuperAdminDTO> AuthSuperAdmin(string token)
        {
            VerifyTokenRes res = this.auth_service.VerifyTokenAsync(token, isAdmin: true);

            if (!res.check)
            {
                return new VerifySuperAdminDTO
                {
                    check = res.check,
                    exception = res.exception,
                };
            }

            UserDTO? admin = await this.user_repo.GetUserById(res.user_id ?? 0);

            if (admin != null && admin.role == Role.Admin)
            {
                return new VerifySuperAdminDTO
                {
                    check = false,
                    exception = "Unauthorized permissions"
                };
            }
            else if (admin != null && admin.role == Role.SuperAdmin)
            {
                return new VerifySuperAdminDTO
                {
                    check = true
                };
            }

            return new VerifySuperAdminDTO
            {
                check = false,
                exception = "User not found or invalid role"
            };
        }


        public async Task<List<UserDTO>?> GetUsersAsync(int admin_id)
        {
            List<UserDTO>? users = await this.repo.GetAllUsersAsync(admin_id);
            return users;
        }

        public async Task<string> ChangeRole(string user_id, string role)
        {
            int conv_user_id = Convert.ToInt32(user_id);

            bool check = await this.user_repo.ChangeRole(conv_user_id, role);

            return check ? "User role updated successfully." : "Failed to update user role.";
        }
    }
}