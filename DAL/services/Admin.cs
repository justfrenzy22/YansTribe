using dal.interfaces;
using core.requests;
using core.entities;
using core.responses;

namespace dal.admin
{
    public class AdminService : IAdminDal
    {


        private readonly IAdminRepo IAdminRepo;


        public AdminService(IAdminRepo IAdminRepo)
        {
            this.IAdminRepo = IAdminRepo;
        }

        public async Task<AdminLoginRes> ValidateLogin(AdminLoginReq model)
        {
            return await IAdminRepo.ValidateLogin(model);
        }

        public async Task<List<User>> GetUsers()
        {
            return await IAdminRepo.GetUsers();
        }
    }

}