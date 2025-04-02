namespace server.mapper
{
    public class UserMapper
    {
        // requests

        public dal.requests.UserLoginReq mapLoginReq(requests.UserLoginReq from)
        {
            return new dal.requests.UserLoginReq
            {
                email = from.email,
                password = from.password
            };
        }

        public dal.requests.UserRegisterReq mapRegisterReq(requests.UserRegisterReq from)
        {
            return new dal.requests.UserRegisterReq
            {
                email = from.email,
                username = from.username,
                password = from.password,
                full_name = from.full_name,
                bio = from.bio,
                pfp_src = from.pfp_src,
                location = from.location,
                website = from.website
            };
        }

        public dal.requests.UserGetUserReq mapGetUserReq(requests.UserGetUserReq from)
        {
            return new dal.requests.UserGetUserReq
            {
                user_id = from.user_id
            };
        }

        public dal.requests.UserGetRoleReq mapGetRoleReq(requests.UserGetRoleReq from)
        {
            return new dal.requests.UserGetRoleReq
            {
                user_id = from.user_id
            };
        }

        // responses

        public responses.UserLoginRes mapLoginRes(dal.responses.UserLoginRes from)
        {
            return new responses.UserLoginRes
            {
                check = from.check,
                user_id = from.user_id,
                exception = from.exception
            };
        }

        public responses.UserRegisterRes mapRegisterRes(dal.responses.UserRegisterRes from)
        {
            return new responses.UserRegisterRes
            {
                check = from.check,
                user_id = from.user_id,
                exception = from.exception
            };
        }

        public responses.UserGetUserRes mapGetUserRes(dal.responses.UserGetUserRes from)
        {
            return new responses.UserGetUserRes
            {
                check = from.check,
                user = from.user,
                exception = from.exception
            };
        }

        public responses.UserGetRoleRes mapGetRoleRes(dal.responses.UserGetRoleRes from)
        {
            return new responses.UserGetRoleRes
            {
                check = from.check,
                role = from.role,
                exception = from.exception
            };
        }

    }
}