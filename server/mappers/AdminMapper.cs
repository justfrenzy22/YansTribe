using core.mapper;

namespace server.mapper
{
    public class AdminMapper
    {
        // Map AdminLoginRes from DAL to Response
        public responses.AdminLoginRes mapLoginRes(dal.responses.AdminLoginRes from)
        {
            return new responses.AdminLoginRes
            {
                check = from.check,
                user_id = from.user_id
            };
        }

        // Map AdminLoginReq from Request to DAL
        public dal.requests.AdminLoginReq mapLoginReq(requests.AdminLoginReq from)
        {
            return new dal.requests.AdminLoginReq
            {
                email = from.email,
                password = from.password
            };
        }

        // Override MapTo for generic mapping (if needed)
        // public override object MapTo(object from)
        // {
        //     // Implement logic for generic mapping if required
        //     throw new NotImplementedException("Generic mapping is not implemented.");
        // }
    }
}