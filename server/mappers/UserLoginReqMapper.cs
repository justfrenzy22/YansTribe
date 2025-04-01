using core.mapper;

namespace server.mapper
{
    public class UserLoginReqMapper : Mapper<requests.UserLoginReq, dal.requests.UserLoginReq>
    {
        public override dal.requests.UserLoginReq MapTo(requests.UserLoginReq from)
        {
            return new dal.requests.UserLoginReq
            {
                email = from.email,
                password = from.password
            };
        }
    }
}