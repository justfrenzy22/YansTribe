using System.ComponentModel.DataAnnotations;
using core.responses;

namespace server.responses
{
    public class AdminLoginRes : BaseRes
    {

        // [Display(Name = "check")]
        // [Required]
        // public required bool check { get; set; } // 'required' is kept

        [Display(Name = "user_id")]
        public int? user_id { get; set; }
    }
}