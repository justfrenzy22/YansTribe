using System.ComponentModel.DataAnnotations;

namespace core.responses
{
    public class AdminLoginRes
    {

        [Display(Name = "check")]
        [Required]
        public required bool check { get; set; } // 'required' is kept

        [Display(Name = "token")]
        public string? token { get; set; }

    }
}