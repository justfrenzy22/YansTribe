using core.entities;
using core.mapper;
using dal.dto;

namespace dal.mapper
{
    public class UserMapper : Mapper<UserDTO, UserCredentials>
    {
        public override UserCredentials MapTo(UserDTO entity) => new UserCredentials(
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

        public UserAccount MapTo(EssentialsUserDTO entity) => new UserAccount(
            user_id: entity.user_id,
            username: entity.username,
            pfp_src: entity.pfp_src,
            is_private: entity.is_private
        );


        public UserProfile MapTo(ProfileUserDTO entity) => new UserProfile(
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
            friends_num: entity.friends_num,
            relationship_info: entity.relationship_info
        );

        public UserDetails MapTo(UserDetailsDTO entity) => new UserDetails(
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