// public class LoginModel
// {
//     [Required]
//     [Display(Name = "Email")]
//     public string Email { get; set; }

//     [Required]
//     [DataType(DataType.Password)]
//     [Display(Name = "Password")]
//     public string Password { get; set; }
// }

using System.ComponentModel.DataAnnotations;

namespace server.models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "email")]
        public required string email { get; set; }

        [Required]
        [Display(Name = "password")]
        public required string password { get; set; }
    }

}