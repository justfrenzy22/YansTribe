using System.ComponentModel.DataAnnotations;

namespace pl.viewModel
{
    public class AdminLoginViewModel
    {
        [Required]
        [Display(Name = "email")]
        public required string email { get; set; }

        [Required]
        [Display(Name = "password")]
        public required string password { get; set; }
    }
}