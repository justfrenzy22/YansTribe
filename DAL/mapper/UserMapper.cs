using core.entities;
using core.mapper;
using dal.dto;

namespace dal.mapper
{
    public class UserMapper : Mapper<UserDTO, FullUser>
    {
        public override FullUser MapTo(UserDTO entity) => new FullUser(
            user_id: entity.user_id,
            username: entity.username,
            email: entity.email,
            password: entity.password,
            full_name: entity.full_name,
            bio: entity.bio,
            pfp_src: entity.pfp_src,
            location: entity.location,
            website: entity.website,
            is_private: entity.is_private,
            created_at: entity.created_at,
            role: entity.role
        );

        public BaseUser MapTo(EssentialsUserDTO entity) => new BaseUser(
            user_id: entity.user_id,
            username: entity.username,
            pfp_src: entity.pfp_src,
            is_private: entity.is_private
        );

        public ProfileUser MapTo(ProfileUserDTO entity) => new ProfileUser(
            user_id: entity.user_id,
            username: entity.username,
            email: entity.email,
            full_name: entity.full_name,
            bio: entity.bio,
            pfp_src: entity.pfp_src,
            location: entity.location,
            website: entity.website,
            is_private: entity.is_private,
            created_at: entity.created_at,
            role: entity.role,
            is_self: entity.is_self,
            is_friend: entity.is_friend,
            friends_num: entity.friends_num,
            friendship_status: entity.friendship_status,
            request_direction: entity.request_direction
        );

        public SafeUser MapTo(SafeUserDTO entity) => new SafeUser(
            user_id: entity.user_id,
            username: entity.username,
            pfp_src: entity.pfp_src,
            email: entity.email,
            full_name: entity.full_name,
            bio: entity.bio,
            location: entity.location,
            website: entity.website,
            is_private: entity.is_private,
            created_at: entity.created_at,
            role: entity.role
        );
    }
}