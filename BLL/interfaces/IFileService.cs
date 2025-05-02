using core.entities;
using Microsoft.AspNetCore.Http;

namespace bll.interfaces
{



    public interface IFileService
    {
        Task<PostMedia> Upload(Guid post_id, IFormFile file);
    }
}