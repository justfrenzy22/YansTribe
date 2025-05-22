using bll.interfaces;

namespace bll.services
{
    public class CDNService : ICDNService
    {
        private readonly string _cdnRootPath = Path.Combine(Directory.GetCurrentDirectory());

        public CDNService() { }

        public byte[]? GetFileBytes(string fileName)
        {
            string filePath = Path.Combine(_cdnRootPath, fileName);

            if (!File.Exists(filePath))
                return null;

            return File.ReadAllBytes(filePath);
        }

        public string? GetContentType(string fileName)
        {
            var ext = Path.GetExtension(fileName)?.ToLowerInvariant();
            return ext switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".mp4" => "video/mp4",
                ".webm" => "video/webm",
                _ => null
            };
        }
    }


}