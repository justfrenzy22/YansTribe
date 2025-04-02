using System.ComponentModel.DataAnnotations;

namespace core.responses
{
    public class BaseRes
    {
        [Display(Name = "exception")]
        public string? exception { get; set; }

        [Required]
        [Display(Name = "check")]
        public required bool check { get; set; }
    }
}