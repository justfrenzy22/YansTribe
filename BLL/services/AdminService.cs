
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
        private readonly IUserRepo _user_repo;
        private readonly IAdminRepo _admin_repo;
        private readonly IHashService _hash_service;
        private readonly IAuthService _auth_service;

        public AdminService(IUserRepo user_repo, IAdminRepo admin_repo, IHashService hash_service, IAuthService auth_service)
        {
            this._user_repo = user_repo;
            this._admin_repo = admin_repo;
            this._hash_service = hash_service;
            this._auth_service = auth_service;
        }

        public async Task<string?> ValidateLogin(string email, string password)
        {
            UserCredentials? user = await this._user_repo.ValidateUserByEmail(email);

            if (user == null)
            {
                return null;
            }

            if (user.role != Role.Admin && user.role != Role.SuperAdmin)
            {
                return null;
            }

            string hash_password = this._hash_service.hash(password);

            if (hash_password != user.password)
            {
                throw new DataAccessException("User or password is incorrect.");
            }

            string token = this._auth_service.GenerateJwtToken(user.user_id.ToString() ?? "", isAdmin: true);

            return token;

        }
        public VerifyTokenRes AuthAdmin(string token) => this._auth_service.VerifyTokenAsync(token, isAdmin: true);

        public async Task<VerifySuperAdminDTO> AuthSuperAdmin(string token)
        {
            VerifyTokenRes res = this._auth_service.VerifyTokenAsync(token, isAdmin: true);

            if (!res.check)
            {
                return new VerifySuperAdminDTO
                {
                    check = res.check,
                    exception = res.exception,
                };
            }

            Guid user_id = res.user_id ?? Guid.Empty;

            if (user_id == Guid.Empty)
            {
                return new VerifySuperAdminDTO
                {
                    check = false,
                    exception = "User not found"
                };
            }

            UserDetails? admin = await this._user_repo.GetUserById(user_id);

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


        public async Task<List<UserCredentials>?> GetUsersAsync(string admin_id)
        {
            List<UserCredentials>? users = await this._admin_repo.GetAllUsersAsync(Guid.Parse(admin_id));
            return users;
        }

        public async Task<string> ChangeRole(string user_id, string role)
        {
            UserDetails? admin = await this._user_repo.GetUserById(Guid.Parse(user_id));

            if (admin == null)
            {
                throw new DataAccessException("User not found.");
            }

            bool check = await this._user_repo.ChangeRole(Guid.Parse(user_id), role);
            return check ? "User role updated successfully." : "Failed to update user role.";
        }
    }
}