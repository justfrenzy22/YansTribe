using bll.interfaces;
using core.entities;
using dal.exceptions;
using Microsoft.AspNetCore.Http;

namespace bll.services
{
    public class FileService : IFileService
    {
        private string uploadsFolder;

        public FileService()
        {
            this.uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        }

        public async Task<PostMedia> Upload(Guid post_id, IFormFile file)
        {
            Guid guid = Guid.NewGuid();

            this.CheckOrCreateFolder();

            string fileName = guid + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(this.uploadsFolder, fileName);

            await this.SaveFile(file, filePath);

            return new PostMedia(
                post_id: post_id,
                media_id: guid,
                media_type: file.ContentType.StartsWith("image/")
                        ? core.enums.MediaType.image
                        : core.enums.MediaType.video,
                media_src: fileName
            );
        }

        private async Task SaveFile(IFormFile file, string filePath)
        {
            try
            {

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    await File.WriteAllBytesAsync(filePath, stream.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new BaseException("Error saving file", 500, ex);
            }
        }

        private void CheckOrCreateFolder()
        {
            try
            {

                if (!Directory.Exists(this.uploadsFolder))
                {
                    Directory.CreateDirectory(this.uploadsFolder);
                }
            }
            catch (Exception ex)
            {
                throw new BaseException("Error creating folder", 500, ex);
            }
        }
    }
}