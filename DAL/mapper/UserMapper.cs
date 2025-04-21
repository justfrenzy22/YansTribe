using core.entities;
using core.mapper;
using dal.dto;

namespace dal.mapper
{
    public class UserMapper : Mapper<UserDTO, User>
    {
        public override User MapTo(UserDTO entity) => new User(
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
            role: entity.role
        );

        public User MapTo (EssentialsUserDTO entity) => new User(
            user_id: entity.user_id,
            username: entity.username,
            pfp_src: entity.pfp_src
        );

        // public override User MapTo

    
    }
}