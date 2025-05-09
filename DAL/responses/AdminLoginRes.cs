using System.ComponentModel.DataAnnotations;
using core.responses;
// using core.responses;

namespace dal.responses
{
    public class AdminLoginRes : BaseRes
    {
        // [Required]
        // [Display(Name = "check")]
        // public required bool check { get; set; } // 'required' is kept

        [Display(Name = "user_id")]
        public int? user_id { get; set; }
    }
}