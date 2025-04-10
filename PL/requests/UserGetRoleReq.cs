using System.ComponentModel.DataAnnotations;

namespace server.requests {
    public class UserGetRoleReq {
        [Required]
        [Display(Name = "user_id")]
        public required int user_id { get; set; }
    }
}