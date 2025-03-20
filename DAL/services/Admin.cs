using dal.interfaces;
using core.requests;
using core.entities;
using core.responses;

namespace dal.admin
{
    public class AdminService : IAdminDal
    {


        private readonly IAdminRepo repo;
        
        public AdminService(IAdminRepo repo) => this.repo = repo;


        public async Task<AdminLoginRes> ValidateLogin(AdminLoginReq model) => await repo.ValidateLogin(model);

        public async Task<List<User>> GetUsers() => await repo.GetUsers();

    }

}