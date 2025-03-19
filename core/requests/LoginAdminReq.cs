using System.ComponentModel.DataAnnotations;

namespace core.requests
{
    public class AdminLoginReq
    {
        [Required]
        [Display(Name = "email")]
        public required string email { get; set; }

        [Required]
        [Display(Name = "email")]
        public required string password { get; set; }
    }
}