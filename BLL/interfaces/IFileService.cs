using core.entities;
using core.enums;
using Microsoft.AspNetCore.Http;

namespace bll.interfaces
{
    public interface IFileService
    {
        Task<PostMedia> UploadPost(Guid entityId, IFormFile file, FileCategory category);
    }
}
// git commit