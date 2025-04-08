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

        public async Task<int> AddUser(User user) => await this.repo.RegisterUser(user);


        public async Task<User?> GetUserById(int user_id) => await this.repo.GetUserById(user_id);
            // await this.executeSecure(async () =>
            //     if (model is UserGetUserReq { user_id: string strModel })
            //     {
            //         Convert.ToInt32(strModel);
            //     }

            //     User? user = await this.repo.GetUserById(model);

            //     return new UserGetUserRes { check = true, user = user };
            // });

        public async Task<int> ValidateUser(string email, string password) => await this.repo.ValidateUser(email, password);

        // return new UserLoginRes { check = true, user_id = user_id };
        // });
    }
}