using System.ComponentModel.DataAnnotations;

public class FileDTO
{
    public string FileName { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public byte[] Content { get; set; } = Array.Empty<byte>();
}

public class CreatePostDTO
{
    [Required]
    public required string content { get; set; }

    public List<IFormFile>? files { get; set; }
}