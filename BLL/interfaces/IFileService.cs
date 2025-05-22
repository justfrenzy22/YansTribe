using core.entities;
using core.enums;
using Microsoft.AspNetCore.Http;

namespace bll.interfaces
{



    public interface IFileService
    {
        Task<PostMedia> Upload(Guid entityId, IFormFile file, FileCategory category);
    }
}