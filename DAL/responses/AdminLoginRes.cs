using System.ComponentModel.DataAnnotations;

namespace dal.responses
{
    public class AdminLoginRes
    {

        [Display(Name = "check")]
        [Required]
        public required bool check { get; set; } // 'required' is kept

        [Display(Name = "user_id")]
        public int? user_id { get; set; }

    }
}