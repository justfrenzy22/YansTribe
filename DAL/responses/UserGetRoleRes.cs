using System.ComponentModel.DataAnnotations;
using core.enums;
using core.responses;

namespace dal.responses
{
    public class UserGetRoleRes : BaseRes
    {
        [Required]
        [Display(Name = "role")]
         public Role role { get; set; }
    }
}