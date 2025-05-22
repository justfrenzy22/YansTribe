using System.ComponentModel.DataAnnotations;

namespace pl.viewModel
{

    public class FileViewModel
    {
        public string FileName { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public byte[] Content { get; set; } = Array.Empty<byte>();
    }

    public class CreatePostViewModel
    {
        [Required]
        public required string content { get; set; }

        public List<IFormFile>? files { get; set; }
    }
}