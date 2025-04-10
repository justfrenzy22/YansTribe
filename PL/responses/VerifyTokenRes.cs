using System.ComponentModel.DataAnnotations;
using core.responses;

namespace server.responses {
    public class VerifyTokenRes : BaseRes {
        [Display(Name = "user_id")]
        public int? user_id { get; set; }

        // [Display(Name = "check")]
        // [Required]
        // public required bool check { get; set; }
    }
}