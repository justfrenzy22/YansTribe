using core.entities;
using Microsoft.AspNetCore.Http;

namespace bll.interfaces
{
    public interface IPostService
    {
        Task<int?> CreatePost(Post post, List<IFormFile>? files);
    }
}