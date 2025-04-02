using System.ComponentModel.DataAnnotations;

namespace server.requests
{
    public class UserRegisterReq
    {
        [Required]
        [Display(Name = "username")]
        public required string username { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "email")]
        public required string email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public required string password { get; set; }

        [Required]
        [Display(Name = "full_name")]
        public required string full_name { get; set; }

        [Required]
        [Display(Name = "bio")]
        public required string bio { get; set; }

        [Required]
        [Display(Name = "pfp_src")]
        public required string pfp_src { get; set; }

        [Required]
        [Display(Name = "location")]
        public required string location { get; set; }

        [Required]
        [Display(Name = "website")]
        public required string website { get; set; }
    }
}