using System.ComponentModel.DataAnnotations;

namespace server.requests
{
    public class UserLoginReq
    {
        [Required]
        [Display(Name = "email")]
        public required string email { get; set; }

        [Required]
        [Display(Name = "password")]
        public required string password { get; set; }
    }
}