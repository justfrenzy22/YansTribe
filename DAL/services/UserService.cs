using core.entities;
using core.enums;
using dal.interfaces;
using dal.interfaces.repo;
using dal.interfaces.service;
using dal.requests;

// using dal.requests;
using dal.responses;

namespace dal.services.user
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepo repo;

        public UserService(IUserRepo repo) => this.repo = repo;

        public async Task<UserRegisterRes> AddUser(UserRegisterReq user) =>
            await this.executeSecure(async () =>
            {
                int user_id = await this.repo.AddUser(user);

                return new UserRegisterRes { check = true, user_id = user_id };
            });

        public async Task<UserGetUserRes> GetUserById(UserGetUserReq model) =>
            await this.executeSecure(async () =>
            {
                User? user = await this.repo.GetUserById(model);

                return new UserGetUserRes { check = true, user = user };
            });

        public async Task<UserGetRoleRes> GetRoleById(UserGetRoleReq model) =>
            await this.executeSecure(async () =>
            {
                Role? role = await this.repo.GetRoleById(model);

                return new UserGetRoleRes { check = true, role = (Role)role };
            });

        public async Task<UserLoginRes> ValidateUser(UserLoginReq model) =>
            await this.executeSecure(async () =>
            {
                int user_id = await this.repo.ValidateUser(model);

                return new UserLoginRes { check = true, user_id = user_id };
            });
    }
}