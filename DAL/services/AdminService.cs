using dal.interfaces;
using dal.requests;
using core.entities;
using core.enums;
using dal.responses;
using dal.interfaces.service;
using dal.interfaces.repo;

namespace dal.services.admin
{
    public class AdminService : BaseService, IAdminService
    {
        private readonly IAdminRepo repo;
        private readonly IUserRepo user_repo;

        public AdminService(IAdminRepo repo, IUserRepo user_repo)
        {
            this.repo = repo;
            this.user_repo = user_repo;
        }

        public async Task<AdminLoginRes> ValidateLogin(AdminLoginReq model) =>

            await this.executeSecure(async () =>
            {
                int user_id = await this.repo.ValidateLogin(model);
                return new AdminLoginRes { check = true, user_id = user_id };
            });
        // try
        // {
        //     int user_id = await this.repo.ValidateLogin(model);

        //     return new AdminLoginRes { check = true, user_id = user_id };
        // }
        // catch (EmptyRequestDataException ex)
        // {
        //     return new AdminLoginRes { check = false, exception = ex.Message };
        // }
        // catch (NotFoundException ex)
        // {
        //     return new AdminLoginRes { check = false, exception = ex.Message };
        // }
        // catch (DatabaseOperationException ex)
        // {
        //     return new AdminLoginRes { check = false, exception = ex.Message };
        // }
        // catch (DataAccessException ex)
        // {
        //     return new AdminLoginRes { check = false, exception = ex.Message };
        // }



        public async Task<AdminGetUsersRes> GetUsersAsync(UserGetRoleReq admin_id) =>

            await this.executeSecure(async () =>
            {
                Role? role = await this.user_repo.GetRoleById(admin_id);
                List<User> users = new List<User>();

                if (role == Role.Admin)
                {
                    users = await this.repo.GetAllUsersAsync();
                }
                else
                {
                    users = await this.repo.GetStandardUsersAsync();
                }

                if (users.Count == 0)
                {
                    return new AdminGetUsersRes { check = false, exception = "No users found." };
                }

                return new AdminGetUsersRes { check = true, users = users };
            });

        // {
        // Role? role = await this.user_repo.GetRoleById(admin_id);

        // if (role == null)
        // {
        //     throw new Exception("User not found");
        // }

        // switch (role)
        // {
        //     case Role.User:
        //         throw new Exception("User is not an admin");
        //     case Role.Admin:
        //         return await this.repo.GetStandardUsersAsync();
        //     case Role.SuperAdmin:
        //         return await this.repo.GetAllUsersAsync();
        //     default:
        //         throw new Exception("User is not an admin");
        // }
        // }
    }
}