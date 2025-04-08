using System.ComponentModel.DataAnnotations;

namespace server.dto
{
    public class UserLoginDTO
    {
        [Required]
        [Display(Name = "email")]
        public required string email { get; set; }

        [Required]
        [Display(Name = "password")]
        public required string password { get; set; }
    }
}