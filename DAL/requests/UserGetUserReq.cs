using System.ComponentModel.DataAnnotations;

namespace dal.requests {
    public class UserGetUserReq {
        [Required]
        [Display(Name = "user_id")]
        public required int user_id { get; set; }
    }
}