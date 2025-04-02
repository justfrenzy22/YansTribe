using System.ComponentModel.DataAnnotations;
using core.responses;

namespace dal.responses {
    public class UserLoginRes : BaseRes {
        // [Required]
        // [Display(Name = "check")]
        // public required bool check { get; set; }

        [Display(Name = "user_id")]
        public int? user_id { get; set; }
    }
}