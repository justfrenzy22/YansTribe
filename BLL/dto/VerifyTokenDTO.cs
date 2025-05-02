using System.ComponentModel.DataAnnotations;
using core.responses;

namespace bll.dto
{
    public class VerifyTokenRes : BaseRes
    {
        [Display(Name = "user_id")]
        [Required]
        public int? user_id { get; set; }
    }
}