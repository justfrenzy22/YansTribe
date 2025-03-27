using core.mapper;

namespace server.mapper
{
    public class AdminLoginResMapper : Mapper<dal.responses.AdminLoginRes, responses.AdminLoginRes>
    {
        public override responses.AdminLoginRes MapTo(dal.responses.AdminLoginRes from)
        {
            return new responses.AdminLoginRes
            {
                check = from.check,
                user_id = from.user_id
            };
        }
    }
}