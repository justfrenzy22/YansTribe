using core.entities;
using core.enums;
using dal.exceptions;
using dal.interfaces;
using dal.requests;

// using dal.requests;
using dal.responses;

namespace dal.user
{
    public class UserService : IUserService
    {
        private readonly IUserRepo repo;

        public UserService(IUserRepo repo) => this.repo = repo;

        public async Task<UserRegisterRes> AddUser(UserRegisterReq user)
        {
            try
            {
                int user_id = await this.repo.AddUser(user);

                return new UserRegisterRes { check = true, user_id = user_id };
            }
            catch (EmptyRequestDataException ex)
            {
                return new UserRegisterRes { check = false, exception = ex.Message };
            }
            catch (DuplicateUserException ex)
            {
                return new UserRegisterRes { check = false, exception = ex.Message };
            }
            catch (DataAccessException ex)
            {
                return new UserRegisterRes { check = false, exception = ex.Message };
            }
        }

        public async Task<UserGetUserRes> GetUserById(UserGetUserReq model)
        {
            try
            {
                User? user = await this.repo.GetUserById(model);

                return new UserGetUserRes { check = true, user = user };
            }
            catch (EmptyRequestDataException ex)
            {
                return new UserGetUserRes { check = false, exception = ex.Message };
            }
            catch (NotFoundException ex)
            {
                return new UserGetUserRes { check = false, exception = ex.Message };
            }
            catch (DatabaseOperationException ex)
            {
                return new UserGetUserRes { check = false, exception = ex.Message };
            }
            catch (DataAccessException ex)
            {
                return new UserGetUserRes { check = false, exception = ex.Message };
            }

            // await repo.GetUserById(user_id)
        }

        public async Task<UserGetRoleRes> GetRoleById(UserGetRoleReq model)
        {
            try
            {
                Role? role = await this.repo.GetRoleById(model);

                return new UserGetRoleRes { check = true, role = (Role)role };
            }
            catch (EmptyRequestDataException ex)
            {
                return new UserGetRoleRes { check = false, exception = ex.Message };
            }
            catch (NotFoundException ex)
            {
                return new UserGetRoleRes { check = false, exception = ex.Message };
            }
            catch (DatabaseOperationException ex)
            {
                return new UserGetRoleRes { check = false, exception = ex.Message };
            }
            catch (DataAccessException ex)
            {
                return new UserGetRoleRes { check = false, exception = ex.Message };
            }
            // await repo.GetRoleById(user_id)
        }

        public async Task<UserLoginRes> ValidateUser(UserLoginReq model)
        {
            try
            {
                int user_id = await this.repo.ValidateUser(model);

                return new UserLoginRes { check = true, user_id = user_id };
            }
            catch (NotFoundException ex)
            {
                return new UserLoginRes { check = false, exception = ex.Message };
            }
            catch (DataAccessException ex)
            {
                return new UserLoginRes { check = false, exception = ex.Message };
            }
        }
    }
}