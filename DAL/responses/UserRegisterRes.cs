using System.ComponentModel.DataAnnotations;
using core.responses;

namespace dal.responses
{
    public class UserRegisterRes : BaseRes
    {
        [Display(Name = "user_id")]
        public int? user_id { get; set; }
    }
}