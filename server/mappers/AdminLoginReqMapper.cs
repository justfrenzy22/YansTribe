using core.mapper;

namespace server.mapper
{
    public class AdminLoginReqMapper : Mapper<requests.AdminLoginReq, dal.requests.AdminLoginReq>
    {
        public override dal.requests.AdminLoginReq MapTo(requests.AdminLoginReq from)
        {
            return new dal.requests.AdminLoginReq
            {
                email = from.email,
                password = from.password
            };
        }
    }
}