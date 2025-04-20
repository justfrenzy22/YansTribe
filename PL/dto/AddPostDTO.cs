using System.ComponentModel.DataAnnotations;

namespace pl.dto
{
    public class AddPostDTO
    {
        public required string content { get; set; }
        public List<IFormFile>? files { get; set; }
    }
}