using dal.requests;
using dal.responses;

namespace dal.interfaces.service
{
    public interface IUserService
    {
        Task<UserRegisterRes> AddUser(UserRegisterReq model);
        Task<UserGetRoleRes> GetRoleById(UserGetRoleReq model);
        // Task<User> GetByEmail(string email);
        Task<UserGetUserRes> GetUserById(UserGetUserReq model);

        Task<UserLoginRes> ValidateUser(UserLoginReq model);
    }
}