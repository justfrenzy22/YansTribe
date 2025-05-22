using bll.dto;
using bll.interfaces;
using core.entities;
using dal.exceptions;
using dal.interfaces.repo;

namespace bll.services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _user_repo;
        private readonly IPostRepo _post_repo;
        private readonly IHashService _hash_service;
        private readonly IAuthService _auth_service;
        public UserService(IUserRepo repo, IPostRepo post_repo, IHashService hash_service, IAuthService auth_service)
        {

            this._user_repo = repo;
            this._post_repo = post_repo;
            this._hash_service = hash_service;
            this._auth_service = auth_service;
        }
        public async Task<string> ValidateUser(string email, string password)
        {
            FullUser? user = await this._user_repo.ValidateUserByEmail(email);

            if (user == null)
            {
                throw new DataAccessException("User or password is incorrect.");
            }

            string hash_password = this._hash_service.hash(password);

            if (hash_password != user.password)
            {
                throw new DataAccessException("User or password is incorrect.");
            }

            string token = this._auth_service.GenerateJwtToken(user.user_id.ToString() ?? "", isAdmin: false);

            return token;
        }

        public async Task<Guid?> RegisterUser(FullUser user)
        {

            FullUser? userEmail = await this._user_repo.GetUserByEmail(user.email);

            if (userEmail != null)
            {
                throw new DataAccessException("User with this email already exists.");
            }

            FullUser? userUsername = await this._user_repo.GetUserByUsername(user.username);

            if (userUsername != null)
            {
                throw new DataAccessException("User with this username already exists.");
            }

            string hash_password = this._hash_service.hash(user.password);

            user.HashPassword(hash_password);

            Guid? user_id = await this._user_repo.RegisterUser(user);

            if (Guid.Empty == user_id || user_id == null)
            {
                throw new DataAccessException("User with this email or username already exists.");
            }

            return user_id;
        }

        public async Task<ProfileUser?> FetchUserProfile(string username, Guid req_user_id)
        {
            Guid? profile_user_id = await this._user_repo.GetUserIdByUsername(username);

            if (profile_user_id == null)
            {
                return null;
            }

            ProfileUser? user = await this._user_repo.GetUserProfileById(req_user_id: req_user_id, profile_user_id: profile_user_id.Value);

            if (user == null)
            {
                return null;
            }

            if (user.is_private)
            {
                return user;
            }
            else
            {
                List<Post> posts = await this._post_repo.GetProfileInitPostsById(req_user_id, profile_user_id ?? Guid.Empty);

                user.AddPosts(posts);

                return user;
            }
        }

        public async Task<SafeUser?> GetUserById(Guid user_id) => await this._user_repo.GetUserById(user_id);

        public async Task<BaseUser?> GetUserEssentials(Guid user_id) => await this._user_repo.GetUserEssentials(user_id);

        public VerifyTokenRes AuthUser(string token) => this._auth_service.VerifyTokenAsync(token, isAdmin: false);
    }
}