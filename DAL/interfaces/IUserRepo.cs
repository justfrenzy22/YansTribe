using core.entities;
using core.enums;
using dal.requests;

namespace dal.interfaces.repo
{
    public interface IUserRepo
    {
        Task<int> AddUser(UserRegisterReq user);
        Task<Role?> GetRoleById(UserGetRoleReq user_id);
        Task<int> ValidateUser(UserLoginReq model);
        Task<User?> GetUserById(UserGetUserReq user_id);
    }
}