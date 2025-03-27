using System.ComponentModel.DataAnnotations;

namespace server.requests
{
    public class AuthRequestReq
    {
        [Required]
        [Display(Name = "token")]
        public required string token { get; set; }
    }
}