using bll.interfaces;
using core.entities;
using core.enums;
using dal.exceptions;
using Microsoft.AspNetCore.Http;

namespace bll.services
{
    public class FileService : IFileService
    {
        private readonly string _baseUploadFolder;
        private readonly Dictionary<FileCategory, string> _categoryFolderPaths;

        public FileService()
        {
            _baseUploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "cdn");

            _categoryFolderPaths = new Dictionary<FileCategory, string>
            {
                { FileCategory.PostImage, Path.Combine(_baseUploadFolder, "images", "posts") },
                { FileCategory.ProfileImage, Path.Combine(_baseUploadFolder, "images", "profile_pics") },
                { FileCategory.StoryImage, Path.Combine(_baseUploadFolder, "images", "stories") },
                { FileCategory.PostVideo, Path.Combine(_baseUploadFolder, "videos", "posts") },
                { FileCategory.StoryVideo, Path.Combine(_baseUploadFolder, "videos", "stories") }
            };

            EnsureFoldersExist();
        }

        public async Task<PostMedia> UploadPost(Guid entityId, IFormFile file, FileCategory category)
        {
            if (!_categoryFolderPaths.TryGetValue(category, out var targetFolder))
                throw new BaseException($"No path defined for category '{category}'.", 500);

            var mediaId = Guid.NewGuid();
            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = $"{mediaId}{fileExtension}";
            var filePath = Path.Combine(targetFolder, fileName);

            await SaveFileAsync(file, filePath);

            var mediaType = DetermineMediaType(file, category);
            var (typeSegment, categorySegment) = GetPathSegments(category);

            return new PostMedia(
                post_id: entityId,
                media_id: mediaId,
                media_type: mediaType,
                media_src: $"/cdn/{typeSegment}/{categorySegment}/{fileName}"
            );
        }

        // public async Task UploadStory()
        // {

        // }

        private async Task SaveFileAsync(IFormFile file, string path)
        {
            try
            {
                using var stream = new FileStream(path, FileMode.Create);
                await file.CopyToAsync(stream);
            }
            catch (Exception ex)
            {
                throw new BaseException("Error saving file", 500, ex);
            }
        }

        private static MediaType DetermineMediaType(IFormFile file, FileCategory category)
        {
            if (category == FileCategory.PostVideo || category == FileCategory.StoryVideo)
                return MediaType.video;

            if (file.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
                return MediaType.image;

            if (file.ContentType.StartsWith("video/", StringComparison.OrdinalIgnoreCase))
                return MediaType.video;

            throw new BaseException($"Unsupported file ContentType: {file.ContentType}", 400);
        }

        private static (string typeSegment, string categorySegment) GetPathSegments(FileCategory category) => category switch
        {
            FileCategory.PostImage => ("images", "posts"),
            FileCategory.ProfileImage => ("images", "profile_pics"),
            FileCategory.StoryImage => ("images", "stories"),
            FileCategory.PostVideo => ("videos", "posts"),
            FileCategory.StoryVideo => ("videos", "stories"),
            _ => throw new BaseException($"Unsupported category: {category}", 400)
        };

        private void EnsureFoldersExist()
        {
            try
            {
                foreach (var folder in _categoryFolderPaths.Values)
                {
                    Directory.CreateDirectory(folder);
                }
            }
            catch (Exception ex)
            {
                throw new BaseException("Error creating folder", 500, ex);
            }
        }
    }
}
