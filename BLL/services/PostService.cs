using bll.interfaces;
using core.entities;
using dal.interfaces.repo;

namespace bll.services
{
    public class PostService : IPostService
    {
        private readonly IPostRepo post_repo;
        private readonly IAuthService auth_service;

        public PostService(IPostRepo post_repo, IAuthService auth_service)
        {
            this.post_repo = post_repo;
            this.auth_service = auth_service;
        }

        public async Task<int?> AddPost(Post post)
        {
            return await this.post_repo.AddPost(post);
        }
    }
}