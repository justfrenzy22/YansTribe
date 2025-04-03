using System.ComponentModel.DataAnnotations;

namespace server.requests
{
    public class UserGetUserReq
    {
        [Required]
        [Display(Name = "user_id")]
        public required string user_id { get; set; }
    }
}