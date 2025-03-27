using dal.interfaces;
using dal.requests;
using core.entities;
using core.enums;
using dal.responses;

namespace dal.admin
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo repo;
        private readonly IUserRepo user_repo;

        public AdminService(IAdminRepo repo, IUserRepo user_repo)
        {
            this.repo = repo;
            this.user_repo = user_repo;
        }

        public async Task<AdminLoginRes> ValidateLogin(AdminLoginReq model) => await repo.ValidateLogin(model);



        public async Task<List<User>> GetUsersAsync(int admin_id)
        {
            Role? role = await this.user_repo.GetRoleById(admin_id);

            if (role == null)
            {
                throw new Exception("User not found");
            }

            switch (role)
            {
                case Role.User:
                    throw new Exception("User is not an admin");
                case Role.Admin:
                    return await this.repo.GetStandardUsersAsync();
                case Role.SuperAdmin:
                    return await this.repo.GetAllUsersAsync();
                default:
                    throw new Exception("User is not an admin");
            }
        }
    }
}