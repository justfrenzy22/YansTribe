using System.ComponentModel.DataAnnotations;

namespace server.responses {
    public class VerifyTokenRes {
        [Display(Name = "user_id")]
        public int? user_id { get; set; }

        [Display(Name = "check")]
        [Required]
        public required bool check { get; set; }
    }
}